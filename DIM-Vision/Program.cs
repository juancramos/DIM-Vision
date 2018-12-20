using DIM_Vision_ClassLibrary;

namespace DIM_Vision
{
    class Program
    {
        static void Main(string[] args)
        {
            ScreenCapture sc = new ScreenCapture();
            // CloudVision.VisionUseGoogle(sc.CaptureScreen());
            CloudVision.VisionUserCognitive(sc.CaptureScreen());
        }
    }
}


