using Paint_Application.Resources;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Shapes;

namespace Paint_Application
{
    public partial class Form1 : Form
    {

        Color myColor;
        Pen myPen;
        Pen myEraser;


        DrawShape shape;
        DrawShape selected_Shape;
        List<DrawShape> lselected_Shape = new List<DrawShape>();
        List<Group> lgroup = new List<Group>();
        Group _currentGroup;
        Point p1, p2;
        Point StartLocation;
        Point DownLocation = new Point();
        Bitmap bm;
        Graphics g;


        /// <List_of_Shape>
        List<DrawShape> lstObject = new List<DrawShape>();
        lArc larc = new lArc();
        lTriangle ltriangle = new lTriangle();
        lHexagon lpolygon = new lHexagon();
        lPolygon New_Polygon = new lPolygon();
        lStar lstar = new lStar();
        lDiamond ldiamond = new lDiamond();
        List<Point> points = new List<Point>();
        Point[] hexagonPoints;

        //Change the Width and Color
        int width = 1;
        private float zoom = 1.2f;

        /// <Shape_button>
        bool bLine;
        bool bRec;
        bool bCircle;
        bool bEllipse;
        bool bPolygon;
        bool bPolygon_New;
        bool bArc;
        bool bTriangle;
        bool bStar;
        bool bDiamond;
        bool bEraser;
        bool bPencil;
        bool bFill;
        bool bSelect;
        bool bDash;
        bool paint = false;

        public Form1()
        {
            InitializeComponent();
            picMain.SetDoubleBuffered();
            myColor = Color.Black;
            myEraser = new Pen(Color.White, 8);
            myPen = new Pen(btn_Color.BackColor,width);
            bm = new Bitmap(this.picMain.Width, this.picMain.Height);
            g = Graphics.FromImage(bm);
            g.Clear(Color.White);
            picMain.Image = bm;
        }


        //First we need to determine the location of the point we have just click
        static Point set_point(PictureBox pb, Point pt)
        {
            float pX = 1f * pb.Image.Width / pb.Width;
            float pY = 1f * pb.Image.Height / pb.Height;
            return new Point((int)(pt.X * pX), (int)(pt.Y * pY));
        }
        //Find the current Color
        private void Validate(Bitmap bmp, Stack<Point> sp, int x, int y, Color Old_Color, Color new_Color)
        {
            Color cv = bmp.GetPixel(x, y);
            if (cv == Old_Color)
            {
                sp.Push(new Point(x, y));
                bmp.SetPixel(x, y, new_Color);
            }
        }
        //Fill method
        public void Fill1(Bitmap bmp, int x, int y, Color new_Color)
        {
            try
            {
                Color old_color = bmp.GetPixel(x, y);
                Stack<Point> pixels = new Stack<Point>();
                pixels.Push(new Point(x, y));
                bmp.SetPixel(x, y, new_Color);
                if (old_color == new_Color) return;
                while (pixels.Count > 0)
                {
                    Point pt = (Point)pixels.Pop();
                    if (pt.X > 0 && pt.Y > 0 && pt.X < bmp.Width - 1 && pt.Y < bmp.Height - 1)
                    {
                        Validate(bmp, pixels, pt.X - 1, pt.Y, old_color, new_Color);
                        Validate(bmp, pixels, pt.X, pt.Y - 1, old_color, new_Color);
                        Validate(bmp, pixels, pt.X + 1, pt.Y, old_color, new_Color);
                        Validate(bmp, pixels, pt.X, pt.Y + 1, old_color, new_Color);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
           
        }
        public void Update_Point()
        {
            foreach (DrawShape shape in lstObject)
            {
                shape.p1.X = (int)(shape.p1.X * zoom);
                shape.p1.Y = (int)(shape.p1.Y * zoom);
                shape.p2.Y = (int)(shape.p2.Y * zoom);
                shape.p2.X = (int)(shape.p2.X * zoom);
            }
        }

        //----------- Refresh the program when client click on other button -----------
        void Refresh()
        {
            this.bRec = false;
            this.bEraser = false;
            this.bLine = false;
            this.bEllipse = false;
            this.bCircle = false;
            this.bArc = false;
            this.bDiamond = false;
            this.bPolygon = false;
            this.bPolygon_New = false;
            this.bTriangle = false;
            this.bStar = false;
            this.bPencil = false;
            this.bSelect = false;
            this.bFill = false;
            larc = new lArc();
            lpolygon = new lHexagon();
            ltriangle = new lTriangle();
            lstar = new lStar();
            ldiamond = new lDiamond();
        }
        void RefreshColor()
        {
            btn_Zoom_in.BackColor = Color.FromArgb(245, 246, 247);
            btn_Zoom_out.BackColor = Color.FromArgb(245, 246, 247);
            btn_Pencil.BackColor = Color.FromArgb(245, 246, 247);
            btn_Eraser.BackColor = Color.FromArgb(245, 246, 247);
            btn_Fill.BackColor = Color.FromArgb(245, 246, 247);
            button1.BackColor = Color.FromArgb(245, 246, 247);
        }


        // -------------------- Witdh and Color Button --------------------
        private void btn_thinLine_Click(object sender, EventArgs e)
        {
            width = 1;
            bDash = false;
        }
        private void btn_mediumLine_Click(object sender, EventArgs e)
        {
            width = 4;
            bDash = false;
        }
        private void button4_Click(object sender, EventArgs e)
        {
            width = 8;
            bDash = false;
        }

        //------------------------------------------------------------





        // -------------------- Function of Program --------------------
        private void btn_Star_Click(object sender, EventArgs e)
        {

            Refresh();
            bStar = true;

        }
        private void btn_Pencil_Click(object sender, EventArgs e)
        {
            Refresh();
            RefreshColor();
            btn_Pencil.BackColor = Color.DarkGray;
            bPencil = true;
        }
        private void btn_Line_Click(object sender, EventArgs e)
        {
            Refresh();
            this.bLine = true;

        }
        private void btn_Rectangle_Click(object sender, EventArgs e)
        {
            Refresh();
            this.bRec = true;

        }
        private void btn_Eraser_Click(object sender, EventArgs e)
        {
            Refresh();
            RefreshColor();
            btn_Eraser.BackColor = Color.DarkGray;
            this.bEraser = true;
        }
        private void btn_Triagle_Click(object sender, EventArgs e)
        {
            Refresh();
            this.bTriangle = true;
        }
        private void btn_Circle_Click(object sender, EventArgs e)
        {
            Refresh();
            this.bCircle = true;
        }
        private void btn_Polygon_Click(object sender, EventArgs e)
        {
            Refresh();
            this.bPolygon = true;

        }
        private void btn_New_Polygon_Click(object sender, EventArgs e)
        {
            Refresh();
            bPolygon_New = true;
        }
        private void btn_Diamond_Click(object sender, EventArgs e)
        {
            Refresh();
            this.bDiamond = true;
        }
        private void btn_Ellipse_Click(object sender, EventArgs e)
        {
            Refresh();
            this.bEllipse = true;
        }
        private void btn_Arc_Click(object sender, EventArgs e)
        {
            Refresh();
            this.bArc = true;
        }
        private void btn_Fill_Click(object sender, EventArgs e)
        {
            Refresh();
            RefreshColor();
            btn_Fill.BackColor = Color.DarkGray;
            this.bFill = true;
        }
        private void btn_Save_Click(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog();
            sfd.Filter = "Image(*.jpg)|*.jpg|(*.*|*.*";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                //creating new bitmap
                Bitmap btmp = bm.Clone(new System.Drawing.Rectangle(0, 0, picMain.Width, picMain.Height), bm.PixelFormat);
                btmp.Save(sfd.FileName, ImageFormat.Jpeg);
                MessageBox.Show("Succesed");
            }
            else
            {
                MessageBox.Show("Cannot save, try again");
            }
        }

        //------------------------------------------------------------


        //-------------------- Function for edit color and fill color -------------------- 
        private void pictureBox_Colors_MouseClick(object sender, MouseEventArgs e)
        {
            Point point = set_point(pictureBox_Colors, e.Location);
            pictureBox_Colors.BackColor = ((Bitmap)pictureBox_Colors.Image).GetPixel(point.X, point.Y);
            btn_Color.BackColor = pictureBox_Colors.BackColor;
        }
        private void btn_EditColor_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.AllowFullOpen = true;
            cd.FullOpen = true;
            cd.AnyColor = true;
            if (cd.ShowDialog() == DialogResult.OK)
            {
                btn_Color.BackColor = cd.Color;
            }
        }
        private void btn_Clear_Click(object sender, EventArgs e)
        {
            g.Clear(Color.White);
            picMain.Image = bm;
            Refresh();
            lstObject.Clear();
            larc.points.Clear();
            New_Polygon.points.Clear();
            lselected_Shape.Clear();
        }
        private void btn_Clear_selected_Item_Click(object sender, EventArgs e)
        {
            var selectedShapes = lstObject.Where(s => s.isSelected).ToList();
            if (selectedShapes.Any())
            {
                foreach (var shape in selectedShapes)
                {
                    lstObject.Remove(shape);
                }
                picMain.Invalidate();
            }
            g.Clear(Color.White);
            picMain.Image = bm;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Refresh();
            RefreshColor();
            button1.BackColor = Color.DarkGray;
            bSelect = true;
        }
        private void btn_Zoom_in_Click(object sender, EventArgs e)
        {
            RefreshColor();
            btn_Zoom_in.BackColor = Color.DarkGray;
            picMain.Image = new Bitmap(picMain.Width, picMain.Height);
            zoom = 1.5f;
            var selectedShapes = lstObject.Where(s => s.isSelected).ToList();
            if (bSelect && selectedShapes.Any())
            {
                foreach (var shape in selectedShapes)
                {
                    shape.p1.X = (int)(shape.p1.X * zoom);
                    shape.p1.Y = (int)(shape.p1.Y * zoom);
                    shape.p2.Y = (int)(shape.p2.Y * zoom);
                    shape.p2.X = (int)(shape.p2.X * zoom);
                }
            }
            else
            {
                foreach (DrawShape shape in lstObject)
                {
                    shape.p1.X = (int)(shape.p1.X * zoom);
                    shape.p1.Y = (int)(shape.p1.Y * zoom);
                    shape.p2.Y = (int)(shape.p2.Y * zoom);
                    shape.p2.X = (int)(shape.p2.X * zoom);
                }
            }

            picMain.Invalidate();

        }
        private void btn_Zoom_out_Click(object sender, EventArgs e)
        {
            RefreshColor();
            btn_Zoom_out.BackColor = Color.DarkGray;
            zoom = 0.7f;

            var selectedShapes = lstObject.Where(s => s.isSelected).ToList();
            if (bSelect && selectedShapes.Any())
            {
                foreach (var shape in selectedShapes)
                {
                    shape.p1.X = (int)(shape.p1.X * zoom);
                    shape.p1.Y = (int)(shape.p1.Y * zoom);
                    shape.p2.Y = (int)(shape.p2.Y * zoom);
                    shape.p2.X = (int)(shape.p2.X * zoom);
                }
            }
            else
            {
                foreach (DrawShape shape in lstObject)
                {
                    shape.p1.X = (int)(shape.p1.X * zoom);
                    shape.p1.Y = (int)(shape.p1.Y * zoom);
                    shape.p2.Y = (int)(shape.p2.Y * zoom);
                    shape.p2.X = (int)(shape.p2.X * zoom);
                }
            }
            picMain.Invalidate();

        }
        private void btn_DashStyle_Click(object sender, EventArgs e)
        {
            bDash = true;
        }
        private void btn_Ungroup_Click(object sender, EventArgs e)
        {
            foreach (DrawShape shape in lstObject)
            {
                shape.isSelected = false;
                picMain.Refresh();
            }
            g.Clear(Color.White);
            picMain.Image = bm;
        }



        //-------------------- PicMain Events for painting--------------------
        private void picMain_MouseDown(object sender, MouseEventArgs e)
        {
            p1 = e.Location;
            if (bPencil)
            {
                this.paint = true;
                p1 = e.Location;
            }
            if (bEraser)
            {
                p1 = e.Location;
                this.paint = true;
            }
            if (bLine)
            {
                DrawShape myObj;
                myObj = new lLine();
                myObj.p1 = e.Location;
                myObj.Color_ = btn_Color.BackColor;
                myObj.Width = width;
                this.lstObject.Add(myObj);
                this.paint = true;
                if (bDash)
                {
                    myObj.isDash = true;
                }
            }
            if (bRec)
            {
                DrawShape myObj;
                myObj = new lRectangle();
                myObj.p1 = e.Location;//Set the first point of shape
                myObj.Color_ = btn_Color.BackColor;//Set the color of shape
                myObj.Width = width;//Set the width of shape
                this.lstObject.Add(myObj);// Add the object to the list<DrawShape>
                if (bDash)
                {
                    myObj.isDash = true;
                }
                this.paint = true;
            }
            if (bEllipse)
            {
                DrawShape myObj;
                myObj = new lEclipse();
                myObj.p1 = e.Location;
                myObj.Color_ = btn_Color.BackColor;
                myObj.Width = width;
                this.lstObject.Add(myObj);
                if (bDash) myObj.isDash = true;
                this.paint = true;
            }
            if (bTriangle)
            {
                ltriangle.p1 = ltriangle.p2 = e.Location;
                ltriangle.Color_ = btn_Color.BackColor;
                ltriangle.Width = width;
                this.lstObject.Add(ltriangle);
                this.paint = true; 
                if (bDash) ltriangle.isDash = true;

            }
            if (bPolygon)
            {
                lpolygon.p1 = lpolygon.p2 = e.Location;
                lpolygon.Color_ = btn_Color.BackColor;
                lpolygon.Width = width;
                if (bDash) lpolygon.isDash = true;
                this.lstObject.Add(lpolygon);
                this.paint = true;
            }
            if (bStar)
            {
                lstar.p1 = lstar.p2 = e.Location;
                lstar.Color_ = btn_Color.BackColor;
                lstar.Width = width;
                this.lstObject.Add(lstar);
                if (bDash) lstar.isDash = true;
                this.paint = true;
            }
            if (bDiamond)
            {
                ldiamond.p1 = e.Location;
                ldiamond.Color_ = btn_Color.BackColor;
                ldiamond.Width = width;
                this.lstObject.Add(ldiamond);
                if (bDash) ldiamond.isDash = true;
                this.paint = true;
            }
            if (bCircle)
            {
                DrawShape myObj;
                myObj = new lCircle();
                myObj.p1 = e.Location; 
                myObj.Color_ = btn_Color.BackColor; 
                myObj.Width = width;
                this.lstObject.Add(myObj);
                if (bDash) myObj.isDash = true;
                this.paint = true;
            }


            //--------------------- For Select function --------------------- 
            //If user lick on the Select button, it will pick a single shape. Or if user press the Ctrl button, the list of shapes will be add
            if (bSelect || ModifierKeys == Keys.Control)
            {
                foreach (DrawShape shape in lstObject)
                {
                    if (shape.Bound.Contains(e.Location))
                    {
                        shape.isSelected = true;//Set the shape is selected
                        selected_Shape = shape; // Single shape
                        shape.originalLocation = e.Location; // Update the original location
                        //break;
                    }
                }

            }
        }
        private void picMain_MouseUp(object sender, MouseEventArgs e)
        {
            paint = false;
            for (int i = 0; i < this.lstObject.Count; i++)
            {
                if (bArc == false)
                {
                    this.lstObject[i].Draw(g);// Draw the object when the Mouse is up
                }
            }
            if (bSelect && selected_Shape != null)
            {
                selected_Shape = null; // Drop the shape have just pick down
            }
        }
        private void picMain_MouseMove(object sender, MouseEventArgs e)
        {
            if (paint)
            {
                if (bPencil)
                {
                    p2 = e.Location;
                    if (bDash)
                    {
                        myPen.Width = 6;
                        myPen.DashStyle = DashStyle.Dash;
                         g.DrawLine(myPen, p1, p2);
                    }
                    else
                    {
                        myPen.Color = btn_Color.BackColor;
                        myPen.Width = width;
                        g.DrawLine(myPen, p1, p2);
                    }
                    p1 = p2;
                }
                if (bEraser)
                {
                    p2 = e.Location;
                    g.DrawLine(myEraser, p1, p2);
                    p1 = p2;
                }
                if (bPencil == false && bEraser == false && lstObject.Count > 0)
                {
                    this.lstObject[this.lstObject.Count - 1].p2 = e.Location;//Update the second point of the shape 
                }
            }
            if (bSelect && selected_Shape != null)
            {
                // First we need to determine the distance between the original point to the latest point
                int dx = (e.Location.X - selected_Shape.originalLocation.X);
                int dy = (e.Location.Y - selected_Shape.originalLocation.Y);
                Point newPoint = new Point(dx, dy);
                picMain.Image = new Bitmap(picMain.Width, picMain.Height);
                
                // After we have the distance, we will draw the shape
                if (e.Button == MouseButtons.Left&& ModifierKeys != Keys.Control) // Single shape
                {
                    selected_Shape.Distance(newPoint);
                    selected_Shape.Draw(g);
                }
                if (ModifierKeys == Keys.Control&& e.Button == MouseButtons.Left) // List of shapes
                {
                    foreach (DrawShape shape in lstObject)
                    {

                        if (shape.isSelected)
                        {
                            shape.Distance(newPoint);
                        }
                        shape.Draw(g);

                    }
                }
                picMain.Refresh();
                selected_Shape.originalLocation = e.Location;
            }
            p2 = e.Location;
            picMain.Refresh();
        }
        private void picMain_Paint(object sender, PaintEventArgs e)
        {
            Graphics ga = e.Graphics;
            for (int i = 0; i < this.lstObject.Count; i++)
            {
                this.lstObject[i].Draw(ga);
            }
        }

        private void pictureBox_Colors_Click(object sender, EventArgs e)
        {

        }

        private void picMain_MouseClick(object sender, MouseEventArgs e)
        {
            if (bArc)
            {
                larc.Color_ = btn_Color.BackColor;
                larc.Width = width;
                this.lstObject.Add(larc);
                this.paint = true;
                larc.points.Add(e.Location);
            }
            if (bFill)
            {
                Point point = set_point(picMain, e.Location);
                Fill1(bm, point.X, point.Y, btn_Color.BackColor);
            }
            if (bPolygon_New)
            {
                if (e.Button == MouseButtons.Left)
                {
                    New_Polygon.Color_ = btn_Color.BackColor;
                    New_Polygon.Width = width;
                    this.lstObject.Add(New_Polygon);
                    this.paint = true;
                    New_Polygon.points.Add(e.Location);
                }
            }

        }
        //---------------------------------------------------------------------


        

    }
}
