using DIM_Vision_ClassLibrary;
using System;

namespace DIM_Vision
{
    class Program
    {
        static void Main(string[] args)
        {
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", "./key.json");
            ScreenCapture sc = new ScreenCapture();
            // CloudVision.VisionUseGoogle(sc.CaptureScreen());
            CloudVision.VisionUserCognitive(sc.CaptureScreen());
        }
    }
}


