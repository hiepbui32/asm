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
    public class lArc : DrawShape
    {
        public List<Point> points= new List<Point>();
        public override void Draw(Graphics gp)
        {
            Pen myPen = new Pen(this.Color_, Width);
            Brush myBrush= new SolidBrush(this.Color_);
            gp.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            foreach (Point point in points)
            {
                if (isSelected)
                {
                    myPen.Color = Color.Blue;
                    myPen.Width = 6;
                    myPen.DashStyle = DashStyle.Dash;
                    gp.DrawCurve(myPen, points.ToArray());
                }
                else if (isDash)
                {
                    myPen.DashStyle = DashStyle.Dash;
                    myPen.Width = 6;
                    gp.FillEllipse(myBrush, point.X - 3, point.Y - 3, 5, 5);
                    if (points.Count < 2)
                        return;
                    gp.DrawCurve(myPen, points.ToArray());
                }
                else
                {
                    gp.FillEllipse(myBrush, point.X - 3, point.Y - 3, 5, 5);
                    if (points.Count < 2)
                        return;
                    gp.DrawCurve(myPen, points.ToArray());
                }
            }
        }
    }
}
