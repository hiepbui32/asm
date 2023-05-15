using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace Paint_Application
{
    public class lStar:DrawShape
    {
        public override void Draw(Graphics gp)
        {
            System.Drawing.Pen myPen = new System.Drawing.Pen(Color_, Width);

            //First,, we need to calculate to find the angle and the radius to draw a star
            const double numPoints = 5;
            PointF[] starPoints = new PointF[(int)(numPoints * 2)];
            double angle = Math.PI / 2;
            double angleIncrement = Math.PI / numPoints;
            double radius = p2.X-p1.X;
            //Next, make an array which has 10 element tho draw the star
            for (int i = 0; i < numPoints * 2; i++)
            {
                double innerRadius = i % 2 == 0 ? radius * 0.4 : radius;
                float x = p1.X + (float)(Math.Cos(angle) * innerRadius);
                float y = p1.Y + (float)(Math.Sin(angle) * innerRadius);
                starPoints[i] = new PointF(x, y);
                angle += angleIncrement;
            }
            gp.DrawPolygon(myPen, starPoints);
            if (isSelected)
            {
                myPen.DashStyle = DashStyle.Dash;
                myPen.Width = 6;
                myPen.Color = Color.Blue;
                gp.DrawPolygon(myPen, starPoints);
            }
            else if (isDash)
            {
                myPen.DashStyle = DashStyle.Dash;
                myPen.Width = 6;
                gp.DrawPolygon(myPen, starPoints);
            }
            else
            {
                gp.DrawPolygon(myPen, starPoints);
            }
        }
      
    }
}
