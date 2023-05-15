using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paint_Application
{
    public class lHexagon : DrawShape
    {
        public Point[] hexagonPoints = new Point[6];
        public override void Draw(Graphics gp)
        {
            Pen myPen = new Pen(Color_, Width);
            double angle = 2 * Math.PI / 6;

            for (int i = 0; i < 6; i++)
            {
                int x = (int)(p1.X + (p2.X-p1.X) * Math.Cos(i * angle));
                int y = (int)(p1.Y + (p2.X - p1.X) * Math.Sin(i * angle));
                hexagonPoints[i] = new Point(x, y);
                //To add 5 point of the hexagon
            }
            if (isSelected)
            {
                myPen.DashStyle = DashStyle.Dash;
                myPen.Color = Color.Blue;
                myPen.Width = 6;
                gp.DrawPolygon(myPen, hexagonPoints);

            }
            else if (isDash)
            {
                myPen.DashStyle = DashStyle.Dash;
                myPen.Width = 6;
                gp.DrawPolygon(myPen, hexagonPoints);

            }
            else
            {
                gp.DrawPolygon(myPen, hexagonPoints);
            }
        }
    }
}
