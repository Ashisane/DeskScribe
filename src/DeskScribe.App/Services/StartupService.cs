using Microsoft.Win32;
using System.Diagnostics;

namespace DeskScribe.App.Services
{
    public static class StartupService
    {
        private const string RUN_KEY = @"Software\Microsoft\Windows\CurrentVersion\Run";
        private const string APP_NAME = "DeskScribe";

        public static void EnableAutoStart()
        {
            try
            {
                using var key = Registry.CurrentUser.OpenSubKey(RUN_KEY, writable: true);
                if (key == null) return;

                string exePath = Process.GetCurrentProcess().MainModule!.FileName;
                key.SetValue(APP_NAME, $"\"{exePath}\" --startup");
            }
            catch { }
        }

        public static void DisableAutoStart()
        {
            try
            {
                using var key = Registry.CurrentUser.OpenSubKey(RUN_KEY, writable: true);
                if (key == null) return;
                if (key.GetValue(APP_NAME) != null) key.DeleteValue(APP_NAME);
            }
            catch { }
        }

        public static bool IsAutoStartEnabled()
        {
            using var key = Registry.CurrentUser.OpenSubKey(RUN_KEY, writable: false);
            if (key == null) return false;
            return key.GetValue(APP_NAME) != null;
        }
    }
}