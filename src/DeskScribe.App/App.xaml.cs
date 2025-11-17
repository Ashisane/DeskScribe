using System.Linq;
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

            bool startupLaunch = e.Args.Contains("--startup");

            // ensure autostart registry is present (keeps --startup flag)
            StartupService.EnableAutoStart();

            // Initialize tray service
            _tray = new TrayService();

            // Create window; drawing mode ON for manual launch, OFF for startup
            MainWindow = new MainWindow(startInDrawMode: !startupLaunch);

            // OFF-SCREEN initialization to force WPF to create HWND/input pipeline
            MainWindow.Left = -5000;
            MainWindow.Top = -5000;
            MainWindow.Show();
            MainWindow.Activate();
            MainWindow.UpdateLayout();
            MainWindow.Hide();
            MainWindow.Left = 0;
            MainWindow.Top = 0;

            // Manual launch: show overlay immediately
            if (!startupLaunch)
            {
                MainWindow.Show();
                MainWindow.Activate();
                (MainWindow as MainWindow)?.ReactivateCanvas();
            }

            // Register global hotkey (Ctrl+D)
            _hotkeys = new HotkeyService(MainWindow);
            _hotkeys.HotkeyPressed += (_, _) =>
            {
                MainWindow.Show();
                MainWindow.Activate();
                (MainWindow as MainWindow)?.ReactivateCanvas();
            };
        }

        private void ShowOverlay_Click(object sender, RoutedEventArgs e) => _tray?.ShowMainWindow();
        private void HideOverlay_Click(object sender, RoutedEventArgs e) => _tray?.HideMainWindow();
        private void Exit_Click(object sender, RoutedEventArgs e) => _tray?.ExitApplication();

        // Tray Save/Load menu handlers
        private void Save_Click(object sender, RoutedEventArgs e) => _tray?.SaveCanvas();
        private void Load_Click(object sender, RoutedEventArgs e) => _tray?.LoadCanvas();
    }
}
