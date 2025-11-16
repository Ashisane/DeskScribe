using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

namespace DeskScribe.App
{
    public class HotkeyService : IDisposable
    {
        private const int HOTKEY_ID = 9000;
        private const int WM_HOTKEY = 0x0312;

        private HwndSource? _source;

        public event EventHandler? HotkeyPressed;

        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        public HotkeyService(Window window)
        {
            // Create message source for the window
            var helper = new WindowInteropHelper(window);
            _source = HwndSource.FromHwnd(helper.Handle);

            if (_source == null)
                return;

            _source.AddHook(WndProc);

            RegisterCtrlDHotkey();
        }

        private void RegisterCtrlDHotkey()
        {
            const uint MOD_CONTROL = 0x0002;
            const uint VK_D = 0x44;

            RegisterHotKey(_source!.Handle, HOTKEY_ID, MOD_CONTROL, VK_D);
        }

        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (msg == WM_HOTKEY)
            {
                HotkeyPressed?.Invoke(this, EventArgs.Empty);
                handled = true;
            }

            return IntPtr.Zero;
        }

        public void Dispose()
        {
            if (_source != null)
            {
                UnregisterHotKey(_source.Handle, HOTKEY_ID);
                _source.RemoveHook(WndProc);
            }
        }
    }
}