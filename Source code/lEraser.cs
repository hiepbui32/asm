using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media;


namespace Paint_Application
{
    public class lEraser: DrawShape
    {
        public override void Draw(Graphics gp)
        {
            System.Drawing.Pen myEraser = new System.Drawing.Pen(System.Drawing.Color.White, 8);
            gp.DrawLine(myEraser, p1, p2);
        }
    }
}
