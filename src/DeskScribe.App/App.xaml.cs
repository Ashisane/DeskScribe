using System.Windows;

namespace DeskScribe.App
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Load embedded icon safely
            var tray = (Hardcodet.Wpf.TaskbarNotification.TaskbarIcon)FindResource("TrayIcon");
            tray.Icon = new System.Drawing.Icon(Application.GetResourceStream(
                new Uri("pack://application:,,,/Resources/app.ico")).Stream);

            // Create and show MainWindow manually
            MainWindow = new MainWindow();
            MainWindow.Show();
        }
        private void ShowOverlay_Click(object sender, RoutedEventArgs e)
        {
            if (Current.MainWindow != null)
            {
                Current.MainWindow.Show();
                Current.MainWindow.Activate();
            }
        }
        private void HideOverlay_Click(object sender, RoutedEventArgs e)
        {
            Current.MainWindow?.Hide();
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Shutdown();
        }
    }
}