using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint_Application
{
    public class lPolygon : DrawShape
    {
        public List<Point> points = new List<Point>();
        public override void Draw(Graphics gp)
        {
            Brush brush = new SolidBrush(Color.Black);
            Pen pen = new Pen(this.Color_, this.Width);
            gp.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            foreach (Point point in points)
            {
                gp.FillEllipse(brush, point.X - 3, point.Y - 3, 5, 5);
            }
            Point[] polyPoints = new Point[points.Count + 1];
            points.CopyTo(polyPoints);
            polyPoints[points.Count] = points[0];
            if (isSelected)
            {
                pen.Color = Color.Blue;
                pen.Width = 6;
                pen.DashStyle = DashStyle.Dash;
                gp.DrawPolygon(pen, polyPoints);
            }
            else if (isDash)
            {
                pen.DashStyle = DashStyle.Dash;
                pen.Width = 6;
                gp.DrawPolygon(pen, polyPoints);
            }
            else
            {
                gp.DrawPolygon(pen, polyPoints);
            }
        }
    }
}
