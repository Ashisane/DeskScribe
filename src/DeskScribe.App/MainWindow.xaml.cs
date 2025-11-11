using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace DeskScribe.App
{
    public partial class MainWindow : Window
    {
        private bool _isDrawingEnabled = true;
        private bool _isMouseDown;
        private Polyline? _currentLine;

        public MainWindow()
        {
            InitializeComponent();
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
                Stroke = Brushes.Black,
                StrokeThickness = 2
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
            else if (e.Key == Key.Escape)
            {
                Close();
            }
            else if (e.Key == Key.C && Keyboard.Modifiers == ModifierKeys.Control)
            {
                // Ctrl + C → Clear the entire canvas
                DrawCanvas.Children.Clear();
                e.Handled = true;
            }
        }
    }
}