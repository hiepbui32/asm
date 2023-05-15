using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paint_Application
{
    public class lCircle : DrawShape
    {
        public override void Draw(Graphics gp)
        {
            Pen myPen = new Pen(Color_, Width);
            if (isSelected)
            {
                myPen.Color = Color.Blue;
                myPen.Width = 6;
                myPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                gp.DrawEllipse(myPen, this.p1.X, this.p1.Y, this.p2.X - this.p1.X, this.p2.X - this.p1.X);
            }
            else if (isDash)
            {
                myPen.Width = 6;
                myPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                gp.DrawEllipse(myPen, this.p1.X, this.p1.Y, this.p2.X - this.p1.X, this.p2.X - this.p1.X);
            }
            else
            {
                gp.DrawEllipse(myPen, this.p1.X, this.p1.Y, this.p2.X - this.p1.X, this.p2.X - this.p1.X);
            }
        }
    }
}
