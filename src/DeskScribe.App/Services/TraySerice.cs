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

            var iconStream = Application.GetResourceStream(new Uri("pack://application:,,,/Resources/app.ico")).Stream;
            _trayIcon.Icon = new System.Drawing.Icon(iconStream);

            _trayIcon.TrayMouseDoubleClick += (_, _) => ShowMainWindow();
        }

        public void ShowMainWindow()
        {
            var win = Application.Current.MainWindow as MainWindow;
            if (win == null) return;

            win.Show();
            win.Activate();
            win.ReactivateCanvas();
        }

        public void HideMainWindow()
        {
            Application.Current.MainWindow?.Hide();
        }

        public void ExitApplication()
        {
            Application.Current.Shutdown();
        }

        public void SaveCanvas()
        {
            var win = Application.Current.MainWindow as MainWindow;
            win?.SaveCanvasPublic();
        }

        public void LoadCanvas()
        {
            var win = Application.Current.MainWindow as MainWindow;
            win?.LoadCanvasPublic();
        }
    }
}