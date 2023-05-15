using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace Paint_Application
{
    public class Group
    {
        public Point p1;
        public Point p2;
        public Color Color_ { get; set; }
        public float Width { get; set; }
        public List<DrawShape> Shapes { get; set; } = new List<DrawShape>();
        public void Draw(Graphics gp)
        {
            foreach (var shape in Shapes)
            {
                shape.Draw(gp);
            }
        }
        public virtual void Distance(Point point)
        {
            p1 = new Point((p1.X + point.X), (p1.Y + point.Y));
            p2 = new Point((p2.X + point.X), (p2.Y + point.Y));
        }
    }
}
