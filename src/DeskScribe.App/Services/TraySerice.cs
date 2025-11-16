using System;
using System.Windows;
using Hardcodet.Wpf.TaskbarNotification;

namespace DeskScribe.App.Services
{
    public class TrayService
    {
        private readonly TaskbarIcon _trayIcon;

        public TrayService()
        {
            _trayIcon = (TaskbarIcon)Application.Current.FindResource("TrayIcon");

            // Load embedded icon
            var iconStream = Application.GetResourceStream(
                new Uri("pack://application:,,,/Resources/app.ico")).Stream;

            _trayIcon.Icon = new System.Drawing.Icon(iconStream);

            // Wire menu events
            _trayIcon.TrayMouseDoubleClick += (_, _) => ShowMainWindow();
        }

        public void ShowMainWindow()
        {
            var win = Application.Current.MainWindow;
            win?.Show();
            win?.Activate();
        }

        public void HideMainWindow()
        {
            Application.Current.MainWindow?.Hide();
        }

        public void ExitApplication()
        {
            Application.Current.Shutdown();
        }
    }
}