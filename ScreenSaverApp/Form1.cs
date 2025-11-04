using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ScreenSaverApp
{
    public partial class Form1 : Form
    {
        // List to store all shapes (polymorphic collection)
        List<Shape> shapes = new List<Shape>();
        // Random generator for colors, sizes, and directions
        Random rand = new Random();

        public Form1()
        {
            InitializeComponent();
            this.DoubleBuffered = true; // Enable double buffering for the form to reduce flicker

            // Enable double buffering for the drawing panel using reflection
            typeof(Panel).InvokeMember("DoubleBuffered",
                System.Reflection.BindingFlags.SetProperty |
                System.Reflection.BindingFlags.Instance |
                System.Reflection.BindingFlags.NonPublic,
                null, drawingPanel, new object[] { true });

            // Attach resize event to pause/resume animation when minimized
            this.Resize += Form1_Resize;
        }

        /// <summary>
        /// Initializes shapes when the form loads and starts the timer.
        /// </summary>
        private void Form1_Load(object sender, EventArgs e)
        {
            // Add initial shapes to the panel
            shapes.Add(new Circle(50, 50, 5, 5, 40, Color.Red));          // Circle
            shapes.Add(new RectangleShape(100, 100, 4, 6, 50, Color.Blue)); // Rectangle with gradient
            shapes.Add(new PolygonShape(200, 150, 3, 4, 60, Color.Green));  // Irregular polygon
            shapes.Add(new PictureShape(300, 200, 2, 3, 70));               // Picture-based shape

            timer1.Start(); // Start the timer for animation
        }

        /// <summary>
        /// Handles drawing and movement of shapes, including collision detection.
        /// </summary>
        private void drawingPanel_Paint(object sender, PaintEventArgs e)
        {
            // Move and draw each shape
            foreach (Shape s in shapes)
            {
                s.Move(drawingPanel.ClientSize); // Update position based on panel size
                s.Draw(e.Graphics);              // Draw shape on the panel
            }

            // Collision detection between shapes
            for (int i = 0; i < shapes.Count; i++)
            {
                for (int j = i + 1; j < shapes.Count; j++)
                {
                    if (shapes[i].GetBounds().IntersectsWith(shapes[j].GetBounds()))
                    {
                        // Reverse directions with safety checks
                        shapes[i].DX = shapes[i].DX == 0 ? rand.Next(1, 3) : -shapes[i].DX;
                        shapes[i].DY = shapes[i].DY == 0 ? rand.Next(1, 3) : -shapes[i].DY;
                        shapes[j].DX = shapes[j].DX == 0 ? rand.Next(1, 3) : -shapes[j].DX;
                        shapes[j].DY = shapes[j].DY == 0 ? rand.Next(1, 3) : -shapes[j].DY;
                    }
                }
            }
        }

        /// <summary>
        /// Timer tick event: applies visual changes and refreshes the panel.
        /// </summary>
        private void timer1_Tick(object sender, EventArgs e)
        {
            // Apply random visual changes occasionally
            foreach (Shape s in shapes)
            {
                if (rand.Next(100) < 5) // 5% chance per tick
                {
                    s.Color = Color.FromArgb(rand.Next(256), rand.Next(256), rand.Next(256)); // Random color
                    s.Size = Math.Max(rand.Next(30, 80), 10); // Random size (minimum 10)
                }
            }

            drawingPanel.Invalidate(); // Force panel redraw
        }

        /// <summary>
        /// Adds a new random shape at the mouse click position.
        /// </summary>
        private void drawingPanel_MouseClick(object sender, MouseEventArgs e)
        {
            int choice = rand.Next(4);
            int dx = rand.Next(-5, 6);
            int dy = rand.Next(-5, 6);
            if (dx == 0) dx = 1; // Ensure non-zero movement
            if (dy == 0) dy = 1;

            switch (choice)
            {
                case 0:
                    shapes.Add(new Circle(e.X, e.Y, dx, dy, 40, Color.Purple));
                    break;
                case 1:
                    shapes.Add(new RectangleShape(e.X, e.Y, dx, dy, 50, Color.Orange));
                    break;
                case 2:
                    shapes.Add(new PolygonShape(e.X, e.Y, dx, dy, 60, Color.Cyan));
                    break;
                case 3:
                    shapes.Add(new PictureShape(e.X, e.Y, dx, dy, 70));
                    break;
            }
        }

        /// <summary>
        /// Pauses animation when minimized and resumes when restored.
        /// </summary>
        private void Form1_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
                timer1.Stop();
            else
                timer1.Start();
        }
    }
}

//final Codes