using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Newtonsoft.Json;

namespace DeskScribe.App
{
    public partial class MainWindow : Window
    {
        private readonly Brush[] _brushes =
        {
            Brushes.Black,
            Brushes.Red,
            Brushes.Green,
            Brushes.Blue,
            Brushes.Yellow,
            Brushes.White
        };

        private int _currentBrushIndex = 0;
        private double _currentStrokeThickness = 2;

        private Brush _currentBrush;
        private bool _isDrawingEnabled;
        private bool _isMouseDown;
        private Polyline? _currentLine;

        private readonly string _saveDir =
            System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), "DeskScribe");

        private readonly string _configDir =
            System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "DeskScribe");

        private readonly string _configPath;

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);

        private const int SPI_SETDESKWALLPAPER = 20;
        private const int SPIF_UPDATEINIFILE = 0x01;
        private const int SPIF_SENDWININICHANGE = 0x02;

        public MainWindow(bool startInDrawMode)
        {
            InitializeComponent();

            Directory.CreateDirectory(_saveDir);
            Directory.CreateDirectory(_configDir);
            _configPath = System.IO.Path.Combine(_configDir, "config.json");

            _currentBrush = _brushes[_currentBrushIndex];
            _isDrawingEnabled = startInDrawMode;
            Cursor = startInDrawMode ? Cursors.Pen : Cursors.Arrow;

            RenderOptions.SetBitmapScalingMode(DrawCanvas, BitmapScalingMode.NearestNeighbor);
            RenderOptions.SetEdgeMode(DrawCanvas, EdgeMode.Aliased);

            MouseDown += OnMouseDown;
            MouseMove += OnMouseMove;
            MouseUp += OnMouseUp;
        }

        public void ReactivateCanvas()
        {
            _isMouseDown = false;
            _currentLine = null;

            // Ensure window and canvas are focused for keyboard/mouse
            this.Focus();
            DrawCanvas.Focus();
            Keyboard.Focus(DrawCanvas);

            // **Enable drawing mode** when reactivating from tray/hotkey
            _isDrawingEnabled = true;
            Cursor = Cursors.Pen;
            Title = "DeskScribe Overlay (Draw Mode)";
        }


        public string SaveCanvasPublic() => SaveCanvasToPng();

        public void LoadCanvasPublic()
        {
            if (!IsVisible)
            {
                Show();
                Activate();
                ReactivateCanvas();
            }
            LoadLastSavedImage();
        }

        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!_isDrawingEnabled) return;

            _isMouseDown = true;
            _currentLine = new Polyline
            {
                Stroke = _currentBrush,
                StrokeThickness = _currentStrokeThickness
            };

            _currentLine.Points.Add(e.GetPosition(DrawCanvas));
            DrawCanvas.Children.Add(_currentLine);
            DrawCanvas.CaptureMouse();
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (!_isMouseDown || _currentLine == null) return;
            _currentLine.Points.Add(e.GetPosition(DrawCanvas));
        }

        private void OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            DrawCanvas.ReleaseMouseCapture();
            _isMouseDown = false;
            _currentLine = null;
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.C && Keyboard.Modifiers == ModifierKeys.Control)
            {
                DrawCanvas.Children.Clear();
                BackgroundImage.Source = null;
                _currentLine = null;
                _isMouseDown = false;
                Title = "DeskScribe Overlay (Canvas Cleared)";
                e.Handled = true;
                return;
            }

            if (e.Key == Key.K && Keyboard.Modifiers == ModifierKeys.Control)
            {
                _currentBrushIndex = (_currentBrushIndex + 1) % _brushes.Length;
                _currentBrush = _brushes[_currentBrushIndex];
                Title = $"DeskScribe Overlay (Brush: {_currentBrush})";
                e.Handled = true;
                return;
            }

            if (e.Key == Key.OemPlus && Keyboard.Modifiers == ModifierKeys.Control)
            {
                _currentStrokeThickness = Math.Min(_currentStrokeThickness + 1, 20);
                Title = $"DeskScribe Overlay (Brush Size: {_currentStrokeThickness})";
                e.Handled = true;
                return;
            }

            if (e.Key == Key.OemMinus && Keyboard.Modifiers == ModifierKeys.Control)
            {
                _currentStrokeThickness = Math.Max(_currentStrokeThickness - 1, 1);
                Title = $"DeskScribe Overlay (Brush Size: {_currentStrokeThickness})";
                e.Handled = true;
                return;
            }

            if (e.Key == Key.Z && Keyboard.Modifiers == ModifierKeys.Control)
            {
                if (DrawCanvas.Children.Count > 0)
                    DrawCanvas.Children.RemoveAt(DrawCanvas.Children.Count - 1);
                e.Handled = true;
                return;
            }

            if (e.Key == Key.S && Keyboard.Modifiers == ModifierKeys.Control)
            {
                var saved = SaveCanvasToPng();
                Title = $"DeskScribe Overlay (Saved: {System.IO.Path.GetFileName(saved)})";
                e.Handled = true;
                return;
            }

            if (e.Key == Key.B && Keyboard.Modifiers == ModifierKeys.Control)
            {
                var png = GetLastSavedImagePath();
                if (png == null || !File.Exists(png))
                {
                    Title = "DeskScribe Overlay (No saved image to set)";
                    e.Handled = true;
                    return;
                }
                var bmp = ConvertPngToBmp(png);
                SetWallpaper(bmp);
                Title = "DeskScribe Overlay (Wallpaper Set)";
                e.Handled = true;
                return;
            }

            if (e.Key == Key.O && Keyboard.Modifiers == ModifierKeys.Control)
            {
                LoadLastSavedImage();
                e.Handled = true;
                return;
            }

            if (e.Key == Key.Escape)
            {
                Hide();
                e.Handled = true;
                return;
            }
        }

        private string SaveCanvasToPng()
        {
            string fileName = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".png";
            string filePath = System.IO.Path.Combine(_saveDir, fileName);

            RenderTargetBitmap rtb = new RenderTargetBitmap(
                (int)RootGrid.ActualWidth,
                (int)RootGrid.ActualHeight,
                96d, 96d,
                PixelFormats.Pbgra32);

            RootGrid.Measure(new Size(RootGrid.ActualWidth, RootGrid.ActualHeight));
            RootGrid.Arrange(new Rect(new Size(RootGrid.ActualWidth, RootGrid.ActualHeight)));
            rtb.Render(RootGrid);

            var encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(rtb));

            using (var fs = new FileStream(filePath, FileMode.Create))
                encoder.Save(fs);

            File.WriteAllText(_configPath,
                JsonConvert.SerializeObject(new { lastSavedImage = filePath }, Formatting.Indented));

            return filePath;
        }

        private string? GetLastSavedImagePath()
        {
            if (!File.Exists(_configPath)) return null;

            try
            {
                var json = File.ReadAllText(_configPath);
                dynamic cfg = JsonConvert.DeserializeObject(json);
                return cfg?.lastSavedImage;
            }
            catch { return null; }
        }

        private string ConvertPngToBmp(string pngPath)
        {
            string bmpPath = System.IO.Path.ChangeExtension(pngPath, ".bmp");

            using var pngStream = File.OpenRead(pngPath);
            var decoder = new PngBitmapDecoder(pngStream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.OnLoad);
            var png = decoder.Frames[0];

            var encoder = new BmpBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(png));

            using var fs = File.Open(bmpPath, FileMode.Create);
            encoder.Save(fs);

            return bmpPath;
        }

        private void SetWallpaper(string bmpPath)
        {
            SystemParametersInfo(
                SPI_SETDESKWALLPAPER,
                0,
                bmpPath,
                SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE);
        }

        private void LoadLastSavedImage()
        {
            var png = GetLastSavedImagePath();
            if (png == null || !File.Exists(png))
            {
                Title = "DeskScribe Overlay (No saved image found)";
                return;
            }

            try
            {
                var bmp = new BitmapImage();
                bmp.BeginInit();
                bmp.CacheOption = BitmapCacheOption.OnLoad;
                bmp.UriSource = new Uri(png);
                bmp.EndInit();

                BackgroundImage.Source = bmp;
                Title = "DeskScribe Overlay (Loaded last image)";
            }
            catch
            {
                Title = "DeskScribe Overlay (Failed to load image)";
            }
        }
    }
}
