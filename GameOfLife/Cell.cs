using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GameOfLife
{
    public class Cell
    {
        public int x = 0;
        public int y = 0;
        private bool alive = false;

        public int neighbours = 0;

        public Rectangle shape = new Rectangle();



        private readonly Brush AliveCellColor = new SolidColorBrush(Color.FromRgb(45, 150, 62));
        private readonly Brush DeadCellColor = new SolidColorBrush(Color.FromRgb(0, 0, 0));

        public Cell(int x, int y)
        {
            this.x = x;
            this.y = y;

            shape.Fill = DeadCellColor;
        }

        public void SetSize(double size, double padding)
        {
            shape.Width = shape.Height = size - padding;

            Canvas.SetLeft(shape, x * (size + padding));
            Canvas.SetTop(shape, y * (size + padding));
        }

        public void Switch()
        {
            alive = !alive;

            if (alive == true)
                shape.Fill = AliveCellColor;
            else
                shape.Fill = DeadCellColor;
        }
        
        public bool IsAlive()
        {
            return alive;
        }
    }
}
