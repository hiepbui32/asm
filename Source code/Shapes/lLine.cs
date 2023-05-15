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
    public class lLine:DrawShape
    {
        public override void Draw(Graphics gp)
        {
            Pen myPen = new Pen(Color_, Width);
            if (isSelected)
            {
                myPen.Color = Color.Blue;
                myPen.Width = 6;
                myPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                gp.DrawLine(myPen, this.p1, this.p2);
            }
            else if (isDash)
            {
                myPen.DashStyle = DashStyle.Dash;
                myPen.Width = 6;
                gp.DrawLine(myPen, this.p1, this.p2);
            }
            else
            {
                gp.DrawLine(myPen, this.p1, this.p2);
            }
        }
    }
}
