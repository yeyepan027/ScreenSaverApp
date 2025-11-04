using System.Drawing;
using System.Drawing.Drawing2D;

namespace ScreenSaverApp
{
    /// <summary>
    /// Represents a rectangle shape with gradient fill.
    /// </summary>
    public class RectangleShape : Shape
    {
        // Constructor passes values to the base Shape class
        public RectangleShape(int x, int y, int dx, int dy, int size, Color color)
            : base(x, y, dx, dy, size, color) { }

        /// <summary>
        /// Draws the rectangle with a gradient effect.
        /// </summary>
        public override void Draw(Graphics g)
        {
            // Gradient fill for visual effect
            using (Brush brush = new LinearGradientBrush(
                new Rectangle(X, Y, Size, Size),
                Color,
                Color.FromArgb(255 - Color.R, 255 - Color.G, 255 - Color.B), // Opposite color for gradient
                45)) // Angle of gradient
            {
                g.FillRectangle(brush, X, Y, Size, Size);
            }
        }
    }
}