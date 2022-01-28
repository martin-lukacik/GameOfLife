using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GameOfLife
{
    public class UICell
    {
        Cell _owner = null;

        Rectangle _rect = new Rectangle();

        private readonly Brush AliveCellColor = new SolidColorBrush(Color.FromRgb(0x2D, 0x96, 0x3E));
        private readonly Brush DeadCellColor = new SolidColorBrush(Color.FromRgb(0x00, 0x00, 0x00));

        public UICell(Cell owner)
        {
            _owner = owner;

            _rect.Fill = DeadCellColor;
        }

        internal void Update()
        {
            if (_owner.IsAlive() == true)
                _rect.Fill = AliveCellColor;
            else
                _rect.Fill = DeadCellColor;
        }

        public Rectangle GetShape()
        {
            return _rect;
        }

        public void SetSize(double size, double padding)
        {
            _rect.Width = _rect.Height = size - padding;

            Canvas.SetLeft(_rect, _owner.x * (size + padding));
            Canvas.SetTop(_rect, _owner.y * (size + padding));
        }
    }
}
