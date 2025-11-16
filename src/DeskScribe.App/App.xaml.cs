using System.Windows;
using DeskScribe.App.Services;

namespace DeskScribe.App
{
    public partial class App : Application
    {
        private TrayService? _tray;
        private HotkeyService? _hotkeys;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Initialize system tray
            _tray = new TrayService();

            // Create main overlay window
            MainWindow = new MainWindow();
            MainWindow.Show();

            // Register global hotkeys
            _hotkeys = new HotkeyService(MainWindow);
            _hotkeys.HotkeyPressed += (_, _) =>
            {
                MainWindow.Show();
                MainWindow.Activate();
            };
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