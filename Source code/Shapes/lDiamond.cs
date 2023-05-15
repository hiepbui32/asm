using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace Paint_Application
{
    public class lDiamond:DrawShape
    {
        public override void Draw(Graphics gp)
        {
            System.Drawing.Pen myPen = new System.Drawing.Pen(Color_, Width);
            int height = p2.Y - p1.Y; int width = p2.X - p1.X;
            PointF[] diamondPoints = new PointF[4];
            diamondPoints[0] = new PointF(p1.X - width / 2, p1.Y);
            diamondPoints[1] = new PointF(p1.X, p1.Y - height / 2);
            diamondPoints[2] = new PointF(p1.X + width / 2, p1.Y);
            diamondPoints[3] = new PointF(p1.X, p1.Y + height / 2);
            // An array of point to draw the diamond, which has 4-sided
            if (isSelected)
            {
                myPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                myPen.Width = 6;
                myPen.Color = Color.Blue;
                gp.DrawPolygon(myPen, diamondPoints);
            }
            else if (isDash)
            {
                myPen.DashStyle = DashStyle.Dash;
                myPen.Width = 6;
                gp.DrawPolygon(myPen, diamondPoints);

            }
            else
            {
                gp.DrawPolygon(myPen, diamondPoints);
            }
        }

    }
}
