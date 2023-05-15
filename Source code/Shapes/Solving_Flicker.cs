using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paint_Application
{
    public static class SolvingFlicker
    {
        public static void SetDoubleBuffered(this PictureBox ptb)
        {
            typeof(PictureBox).InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance
                | BindingFlags.SetProperty, null, ptb, new object[] { true });
        }
    }
}
