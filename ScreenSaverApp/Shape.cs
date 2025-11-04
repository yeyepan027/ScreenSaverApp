using System.Drawing;

namespace ScreenSaverApp
{
    /// <summary>
    /// Base class for all shapes. Handles position, size, color, and movement logic.
    /// </summary>
    public abstract class Shape
    {
        public int X, Y;       // Position
        public int DX, DY;     // Movement speed
        public int Size;       // Shape size
        public Color Color;    // Shape color

        public Shape(int x, int y, int dx, int dy, int size, Color color)
        {
            X = x; Y = y; DX = dx; DY = dy; Size = size; Color = color;
        }

        /// <summary>
        /// Draw the shape (implemented by subclasses).
        /// </summary>
        public abstract void Draw(Graphics g);

        /// <summary>
        /// Move the shape and bounce off panel edges.
        /// </summary>
        public virtual void Move(Size bounds)
        {
            X += DX;
            Y += DY;

            // Bounce horizontally
            if (X < 0 || X + Size > bounds.Width) DX = -DX;
            // Bounce vertically
            if (Y < 0 || Y + Size > bounds.Height) DY = -DY;
        }

        /// <summary>
        /// Get bounding rectangle for collision detection.
        /// </summary>
        public Rectangle GetBounds()
        {
            return new Rectangle(X, Y, Size, Size);
        }
    }
}