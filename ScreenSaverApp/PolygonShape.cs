using System.Drawing;
using System.Drawing.Drawing2D;

namespace ScreenSaverApp
{
    /// <summary>
    /// Represents an irregular polygon shape that rotates over time.
    /// </summary>
    public class PolygonShape : Shape
    {
        private float angle = 0; // Rotation angle

        // Constructor passes values to the base Shape class
        public PolygonShape(int x, int y, int dx, int dy, int size, Color color)
            : base(x, y, dx, dy, size, color) { }

        /// <summary>
        /// Draws the polygon and applies rotation.
        /// </summary>
        public override void Draw(Graphics g)
        {
            // Define polygon points
            Point[] points = {
                new Point(X, Y),
                new Point(X + Size, Y + Size / 2),
                new Point(X + Size / 2, Y + Size)
            };

            // Apply rotation using transformation matrix
            Matrix matrix = new Matrix();
            matrix.RotateAt(angle, new PointF(X + Size / 2, Y + Size / 2));
            matrix.TransformPoints(points);

            // Fill polygon with color
            using (Brush brush = new SolidBrush(Color))
            {
                g.FillPolygon(brush, points);
            }

            angle += 2; // Increment rotation angle for animation
        }
    }
}