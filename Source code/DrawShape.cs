using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paint_Application
{
    public abstract class DrawShape
    {
        public Point p1;
        public Point p2;
        public Point originalLocation;
        // To serve the Select and Group function

        //Adjust Color and Width
        public Color Color_ { get; set; } 
        public float Width { get; set; }
        public bool isDash { get; set; }

        public bool isSelected { get; set; }
        // To determine that whether this shape is selected?


        // A function to create a bound of a shape, we use it to select shape
        public Rectangle Bound {

            get
            {
                int x = Math.Min(p1.X, p2.X);
                int y = Math.Min(p1.Y, p2.Y);
                int width = Math.Abs(p1.X - p2.X);
                int height = Math.Abs(p1.Y - p2.Y);
                return new Rectangle(x, y, width, height);
            }
           
        }
        // A function to move the shape with the lastet point
        public virtual void Distance(Point point)
        {
            p1 = new Point((p1.X + point.X), (p1.Y + point.Y));
            p2 = new Point((p2.X + point.X), (p2.Y + point.Y));
        }
        // An abstract function to draw all of shape, we use it for Paint Event
        public abstract void Draw(Graphics gp);

    }
}
