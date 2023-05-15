using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paint_Application
{
    public class lRectangle : DrawShape
    {
        public override void Draw(Graphics gp)
        {
            Pen myPen = new Pen(Color_, Width);
            int x = Math.Min(p1.X, p2.X);
            int y = Math.Min(p1.Y, p2.Y);
            int width = Math.Abs(p1.X - p2.X);
            int height = Math.Abs(p1.Y - p2.Y);
            if (isSelected)
            {
                myPen.Color = Color.Blue;
                myPen.Width = 6;
                myPen.DashStyle = DashStyle.Dash;
                gp.DrawRectangle(myPen, x, y, width, height);
            }
            else if (isDash)
            {
                myPen.DashStyle = DashStyle.Dash;
                myPen.Width = 6;
                gp.DrawRectangle(myPen, x, y, width, height);
            }
            else
            {
                gp.DrawRectangle(myPen, x, y, width, height);
            }
        }
    }
}
