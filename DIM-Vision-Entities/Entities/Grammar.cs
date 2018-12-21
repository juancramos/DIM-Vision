using DIM_Vision_Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DIM_Vision_Entities.Entities
{
    public class Grammar
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
        public GrammarType GrammarType { get; set; }
        public string Value { get; set; }
        public virtual IList<Choice> Choices { get; set; }

        public Grammar()
        {
            Id = Guid.NewGuid();
            Choices = new List<Choice>();
            GrammarType = GrammarType.None;
        }
    }
}
