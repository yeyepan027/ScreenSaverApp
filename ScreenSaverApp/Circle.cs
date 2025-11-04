using System.Drawing;

namespace ScreenSaverApp
{
    /// <summary>
    /// Represents a circle shape in the screensaver.
    /// </summary>
    public class Circle : Shape
    {
        // Constructor passes values to the base Shape class
        public Circle(int x, int y, int dx, int dy, int size, Color color)
            : base(x, y, dx, dy, size, color) { }

        /// <summary>
        /// Draws the circle using the specified graphics object.
        /// </summary>
        public override void Draw(Graphics g)
        {
            using (Brush brush = new SolidBrush(Color))
            {
                g.FillEllipse(brush, X, Y, Size, Size);
            }
        }
    }
}