using System.Drawing;

namespace ScreenSaverApp
{
    /// <summary>
    /// Represents a shape that displays an image from resources.
    /// </summary>
    public class PictureShape : Shape
    {
        // Image loaded from project resources
        Image img = Properties.Resources.ganda;

        // Constructor passes values to the base Shape class
        public PictureShape(int x, int y, int dx, int dy, int size)
            : base(x, y, dx, dy, size, Color.Transparent) { }

        /// <summary>
        /// Draws the image at the shape's position and size.
        /// </summary>
        public override void Draw(Graphics g)
        {
            g.DrawImage(img, X, Y, Size, Size);
        }
    }
}