using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;

namespace Paint_Application
{
    public class lPencil : DrawShape    
    {
        public override void Draw(Graphics gp)
        {
            System.Drawing.Pen myPen = new System.Drawing.Pen(Color_, Width);
            gp.DrawLine(myPen, p1, p2);
        }
    }
}
