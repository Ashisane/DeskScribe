using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.IO;
using System.Windows.Media.Imaging;
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
        private bool _isDrawingEnabled = true;
        private bool _isMouseDown;
        private Polyline? _currentLine;
        
        private readonly string _saveDir =
            System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), "DeskScribe");
        private readonly string _configDir =
            System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "DeskScribe");
        private readonly string _configPath;
        
        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool SystemParametersInfo(
            int uAction, int uParam, string lpvParam, int fuWinIni);

        private const int SPI_SETDESKWALLPAPER = 20;
        private const int SPIF_UPDATEINIFILE = 0x01;
        private const int SPIF_SENDWININICHANGE = 0x02;


        public MainWindow()
        {
            InitializeComponent();
            _currentBrush = _brushes[_currentBrushIndex];
            Cursor = Cursors.Pen;
            
            Directory.CreateDirectory(_saveDir);
            Directory.CreateDirectory(_configDir);

            _configPath = System.IO.Path.Combine(_configDir, "config.json");
            
            RenderOptions.SetBitmapScalingMode(DrawCanvas, BitmapScalingMode.NearestNeighbor);
            RenderOptions.SetEdgeMode(DrawCanvas, EdgeMode.Aliased);
            
            MouseDown += OnMouseDown;
            MouseMove += OnMouseMove;
            MouseUp   += OnMouseUp;
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
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (!_isDrawingEnabled || !_isMouseDown || _currentLine == null) return;
            _currentLine.Points.Add(e.GetPosition(DrawCanvas));
        }

        private void OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            _isMouseDown = false;
            _currentLine = null;
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            // Alt + D toggle (SystemKey for Alt combos)
            if (e.SystemKey == Key.D && Keyboard.Modifiers == ModifierKeys.Alt)
            {
                _isDrawingEnabled = !_isDrawingEnabled;
                Cursor = _isDrawingEnabled ? Cursors.Pen : Cursors.Arrow;
                Title  = _isDrawingEnabled ? "DeskScribe Overlay (Drawing)" : "DeskScribe Overlay (View)";
                e.Handled = true;
            }
            else if (e.Key == Key.C && Keyboard.Modifiers == ModifierKeys.Control)
            {
                // Ctrl + C → Clear the entire canvas
                DrawCanvas.Children.Clear();
                _currentLine = null;
                _isMouseDown = false;

                // Clear background PNG
                BackgroundImage.Source = null;

                // DO NOT delete config.json because user wants to keep last saved reference

                Title = "DeskScribe Overlay (Canvas Cleared)";

                DrawCanvas.UpdateLayout();
                DrawCanvas.InvalidateVisual();

                e.Handled = true;
            }
            else if (e.Key == Key.K && Keyboard.Modifiers == ModifierKeys.Control)
            {
                // Ctrl + K → Rotate through brushes
                _currentBrushIndex = (_currentBrushIndex + 1) % _brushes.Length;
                _currentBrush = _brushes[_currentBrushIndex];
                Title = $"DeskScribe Overlay (Brush: {_currentBrush.ToString().Replace("System.Windows.Media.", "")})";
                e.Handled = true;
            }
            else if (e.Key == Key.OemPlus && Keyboard.Modifiers == ModifierKeys.Control)
            {
                // Ctrl + '+' → increase stroke
                _currentStrokeThickness = Math.Min(_currentStrokeThickness + 1, 20);
                Title = $"DeskScribe Overlay (Brush Size: {_currentStrokeThickness})";
                e.Handled = true;
            }
            else if (e.Key == Key.OemMinus && Keyboard.Modifiers == ModifierKeys.Control)
            {
                // Ctrl + '-' → decrease stroke
                _currentStrokeThickness = Math.Max(_currentStrokeThickness - 1, 1);
                Title = $"DeskScribe Overlay (Brush Size: {_currentStrokeThickness})";
                e.Handled = true;
            }
            else if (e.Key == Key.Z && Keyboard.Modifiers == ModifierKeys.Control)
            {
                // Ctrl + Z -> undo last stroke (remove the most recently added child)
                if (DrawCanvas.Children.Count > 0)
                {
                    DrawCanvas.Children.RemoveAt(DrawCanvas.Children.Count - 1);
                }
                e.Handled = true;
            }
            else if (e.Key == Key.S && Keyboard.Modifiers == ModifierKeys.Control)
            {
                // Ctrl + S -> save canvas as png
                string saved = SaveCanvasToPng();
                Title = $"DeskScribe Overlay (Saved: {System.IO.Path.GetFileName(saved)})";
                DrawCanvas.UpdateLayout();
                DrawCanvas.InvalidateVisual();

                e.Handled = true;
            }
            else if (e.Key == Key.B && Keyboard.Modifiers == ModifierKeys.Control)
            {
                // Ctrl + B -> set saved png to background
                string? pngPath = GetLastSavedImagePath();

                if (pngPath == null || !File.Exists(pngPath))
                {
                    Title = "DeskScribe Overlay (No saved image to set)";
                    e.Handled = true;
                    return;
                }

                string bmpPath = ConvertPngToBmp(pngPath);
                SetWallpaper(bmpPath);

                Title = "DeskScribe Overlay (Wallpaper Set)";
                DrawCanvas.UpdateLayout();
                DrawCanvas.InvalidateVisual();
                
                e.Handled = true;
            }
            else if (e.Key == Key.O && Keyboard.Modifiers == ModifierKeys.Control)
            {
                // Ctrl + O -> open prev saved image on canvas
                LoadLastSavedImage();
                DrawCanvas.UpdateLayout();
                DrawCanvas.InvalidateVisual();
                e.Handled = true;
            }
            else if (e.Key == Key.Escape)
            {
                this.Hide();
            }
        }
        
        private string SaveCanvasToPng()
        {
            // File path
            string fileName = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".png";
            string filePath = System.IO.Path.Combine(_saveDir, fileName);

            // Render canvas into a bitmap
            RenderTargetBitmap rtb = new RenderTargetBitmap(
                (int)RootGrid.ActualWidth,
                (int)RootGrid.ActualHeight,
                96d, 96d,
                PixelFormats.Pbgra32
            );

            RootGrid.Measure(new Size(RootGrid.ActualWidth, RootGrid.ActualHeight));
            RootGrid.Arrange(new Rect(new Size(RootGrid.ActualWidth, RootGrid.ActualHeight)));
            rtb.Render(RootGrid);

            // Encode PNG with transparency
            PngBitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(rtb));

            using (FileStream fs = new FileStream(filePath, FileMode.Create))
                encoder.Save(fs);

            // Save last saved file path into config.json
            File.WriteAllText(_configPath, JsonConvert.SerializeObject(new
            {
                lastSavedImage = filePath
            }, Formatting.Indented));

            return filePath;
        }
        private string? GetLastSavedImagePath()
        {
            if (!File.Exists(_configPath))
                return null;

            try
            {
                var json = File.ReadAllText(_configPath);
                dynamic cfg = JsonConvert.DeserializeObject(json);

                return cfg?.lastSavedImage;
            }
            catch
            {
                return null;
            }
        }
        private string ConvertPngToBmp(string pngPath)
        {
            string bmpPath = System.IO.Path.ChangeExtension(pngPath, ".bmp");

            using (var pngStream = File.OpenRead(pngPath))
            {
                var decoder = new PngBitmapDecoder(pngStream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.OnLoad);
                BitmapSource png = decoder.Frames[0];

                BitmapEncoder encoder = new BmpBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(png));

                using (var fs = File.Open(bmpPath, FileMode.Create))
                {
                    encoder.Save(fs);
                }
            }

            return bmpPath;
        }
        private void SetWallpaper(string bmpPath)
        {
            SystemParametersInfo(
                SPI_SETDESKWALLPAPER,
                0,
                bmpPath,
                SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE
            );
        }
        private void LoadLastSavedImage()
        {
            string? pngPath = GetLastSavedImagePath();

            if (pngPath == null || !File.Exists(pngPath))
            {
                Title = "DeskScribe Overlay (No saved image found)";
                return;
            }

            try
            {
                BitmapImage bmp = new BitmapImage();
                bmp.BeginInit();
                bmp.CacheOption = BitmapCacheOption.OnLoad;
                bmp.UriSource = new Uri(pngPath);
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