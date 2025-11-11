using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

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

        public MainWindow()
        {
            InitializeComponent();
            _currentBrush = _brushes[_currentBrushIndex];
            Cursor = Cursors.Pen;

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

            else if (e.Key == Key.Escape)
            {
                Close();
            }
        }
    }
}