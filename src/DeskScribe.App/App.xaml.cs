using System.Windows;
using DeskScribe.App.Services;

namespace DeskScribe.App
{
    public partial class App : Application
    {
        private TrayService? _tray;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Initialize tray service
            _tray = new TrayService();

            // Create and show MainWindow
            MainWindow = new MainWindow();
            MainWindow.Show();
        }

        private void ShowOverlay_Click(object sender, RoutedEventArgs e)
        {
            _tray?.ShowMainWindow();
        }

        private void HideOverlay_Click(object sender, RoutedEventArgs e)
        {
            _tray?.HideMainWindow();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            _tray?.ExitApplication();
        }
    }
}