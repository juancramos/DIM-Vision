using DIM_Vision_Cross;
using DIM_Vision_Data.DataAccess;
using DIM_Vision_Entities.Enums;
using System;
using System.Linq;
using System.Reflection;
using System.Speech.Recognition;
using System.Speech.Synthesis;

namespace DIM_Vision_ClassLibrary
{
    public class Vision
    {
        private static SpeechSynthesizer synth = new SpeechSynthesizer();
        private static SpeechRecognitionEngine _recognizer = new SpeechRecognitionEngine();
        public Vision()
        {
            _recognizer.SetInputToDefaultAudioDevice();
            _recognizer.UnloadAllGrammars();
            bool ready = CreateGrammarBuilder();
            if (!ready)
            {
                synth.Speak("No he podido iniciar.");
                return;
            }
            else
            {
                _recognizer.UpdateRecognizerSetting(VisionConstants.GrammasSettings[0], 60);
                _recognizer.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(_recognizerHandler);
                // _recognizer.SpeechRecognitionRejected += _recognizerHandlerRejected;
                _recognizer.RecognizeAsync(RecognizeMode.Multiple);
            }
            //synth.Speak("Ahora puedo ayudar.");
        }

        private bool CreateGrammarBuilder()
        {
            try
            {
                using (DatabaseContext context = new DatabaseContext())
                {
                    context.Database.EnsureCreated();
                    foreach (DIM_Vision_Entities.Entities.Grammar item in context.Grammars.OrderBy(x => x.Order))
                    {
                        if (item.Choices.Any())
                        {
                            GrammarBuilder bg = new GrammarBuilder(item.Name);
                            string[] c = item.Choices.Where(x => !x.EmbeddedChoices.Any() && x.CboiceType == ChoiceType.None).OrderBy(x => x.Order).Select(x => x.Name).ToArray();
                            if (c.Any()) bg.Append(new Choices(c));

                            foreach (IGrouping<int, DIM_Vision_Entities.Entities.Choice> e in item.Choices.Where(x => x.EmbeddedChoices.Any() && x.CboiceType == ChoiceType.None).GroupBy(x => x.Order))
                            {
                                bg.Append(new GrammarBuilder(new Choices(e.OrderBy(x => x.Order).Select(x => x.Name).ToArray())));
                                string tag = e.OrderBy(x => x.Order).Select(x => x.Value).FirstOrDefault();
                                var em = e.OrderBy(x => x.Order).SelectMany(x => x.EmbeddedChoices);

                                Choices lChoices = new Choices();
                                foreach (var ec in em)
                                {
                                    lChoices.Add(new GrammarBuilder(new SemanticResultValue(ec.Name, ec.Value)));
                                }
                                if (lChoices != null)
                                    bg.Append(new GrammarBuilder(new SemanticResultKey(tag, lChoices)));
                            }

                            _recognizer.LoadGrammar(new Grammar(bg) { Name = item.Name, Enabled = true, Priority = item.Order });
                        }
                        else _recognizer.LoadGrammar(new Grammar(new GrammarBuilder(item.Value)) { Name = item.Name, Enabled = true, Priority = item.Order });
                    }
                }
                return _recognizer.Grammars.Count > 0;
            }
            catch
            {
                return false;
            }
        }

        private static void _recognizerHandler(object sender, SpeechRecognizedEventArgs e)
        {
            using (DatabaseContext context = new DatabaseContext())
            {
                if (context.Grammars.Any(x => x.Name.Equals(e.Result.Text)))
                {
                    DIM_Vision_Entities.Entities.Grammar action = context.Grammars.FirstOrDefault(x => x.Name.Equals(e.Result.Text));
                    if (action != null)
                    {
                        MethodInfo met = typeof(WindowsInteraction).GetMethod(action.Value);
                        if (met != null)
                        {
                            var response = met.Invoke(null, null);
                            synth.Speak((string)response);
                        }
                    }
                }
                else if (context.Choices.Any(x => x.Name.Equals(e.Result.Text)))
                {
                    DIM_Vision_Entities.Entities.Choice action = context.Choices.FirstOrDefault(x => x.Name.Equals(e.Result.Text));
                    if (action != null)
                    {
                        MethodInfo met = typeof(WindowsInteraction).GetMethod(action.Value);
                        if (met != null) met.Invoke(null, null);
                    }
                }
                else
                {
                    SemanticValue semantics = e.Result.Semantics;
                    if (semantics.Any(y => context.Choices.Any(x => x.Value.Equals(y.Key))))
                    {
                        var sem = semantics.FirstOrDefault(y => context.Choices.Any(x => x.Value.Equals(y.Key)));
                        MethodInfo met = typeof(WindowsInteraction).GetMethod(sem.Value.Value.ToString());
                        if (met != null)
                        {
                            var response = met.Invoke(null, null);
                            synth.Speak((string)response);
                        }
                    }
                }
            }

            // synth.Speak(string.Join(",",listApps));           
        }

        private static void _recognizerHandlerRejected(object sender, SpeechRecognitionRejectedEventArgs e)
        {
            if (e.Result.Alternates.Count == 0)
            {
                synth.Speak("No te he entendido. Intentalo de nuevo.");
                return;
            }
            else if (e.Result.Alternates.Any(x => string.IsNullOrWhiteSpace(x.Text)))
            {
                synth.Speak("No te he entendido. Intentalo de nuevo.");
                return;
            }
            else
            {
                synth.Speak("No te he entendido. has querido decir:");
                foreach (RecognizedPhrase r in e.Result.Alternates)
                {
                    synth.Speak($"      {r.Text},");
                }
            }
        }
    }
}
