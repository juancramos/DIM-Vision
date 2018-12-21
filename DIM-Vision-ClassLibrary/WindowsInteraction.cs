using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;

namespace DIM_Vision_ClassLibrary
{
    public static class WindowsInteraction
    {
        public static void GetScreenShot()
        {
            ScreenCapture sc = new ScreenCapture();
            CloudVision.VisionUseGoogle(sc.CaptureScreen());
            CloudVision.VisionUserCognitive(sc.CaptureScreen());
        }

        public static string GetRunningApps()
        {
           return string.Join(". ", Process.GetProcesses().Where(x => !string.IsNullOrWhiteSpace(x.MainWindowTitle))
                .Select(x => x.MainWindowTitle));
        }

        public static bool WindowsActions(string windowsTittle)
        {
            try
            {
                IntPtr w = Auxiliar.FindWindowByCaption(IntPtr.Zero, windowsTittle);
                Auxiliar.ShowWindow(w, Auxiliar.SW_SHOWNORMAL);
                return Auxiliar.ShowWindow(w, Auxiliar.SW_MAXIMIZE);
            }
            catch 
            {
                return false;
            }
        }

        private static class Auxiliar
        {
            public const int SW_SHOWNORMAL = 1;
            public const int SW_MAXIMIZE = 3;
            public const int SW_MINIMIZE = 6;
            public const int SW_FORCEMINIMIZE = 11;
            [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool DestroyWindow(IntPtr hwnd);

            [DllImport("user32.dll", EntryPoint = "FindWindow")]
            public static extern IntPtr FindWindowByCaption(IntPtr ZeroOnly, string lpWindowName);

            [DllImport("user32.dll")]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        }
    }
}
