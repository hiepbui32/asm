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
    
    public class lTriangle:DrawShape
    {
        public override void Draw(Graphics gp)
        {
            Pen myPen = new Pen(Color_, Width);
            double xMid= (p1.X+ p2.X)/2;
            //Find the center of a triangle
            Point first= new Point(p1.X, p2.Y);
            Point mid = new Point((int)xMid, p1.Y);
           
            if (isSelected)
            {
                Pen newPen = new Pen(Color.Blue, 6);
                newPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                gp.DrawLine(newPen, first, mid);
                gp.DrawLine(newPen, first, p2);
                gp.DrawLine(newPen, p2, mid);
            }
            else if (isDash)
            {
                myPen.Width = 6;
                myPen.DashStyle = DashStyle.Dash;
                gp.DrawLine(myPen, first, mid);
                gp.DrawLine(myPen, first, p2);
                gp.DrawLine(myPen, p2, mid);

            }
            else
            {
                    gp.DrawLine(myPen, first, mid);
                    gp.DrawLine(myPen, first, p2);
                    gp.DrawLine(myPen, p2, mid);

            }

        }
    }
}
