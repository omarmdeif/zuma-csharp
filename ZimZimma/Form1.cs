using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZimZimma
{
    public partial class Form1 : Form
    {

        public Bitmap off;
        public Bitmap map;
        public Timer tt = new Timer();
        public curve obj = new curve();
        enum Modes { CTRL_POINTS, DRAG };
        Modes CurrentMouseMode = Modes.CTRL_POINTS;
        int numOfCtrlPoints = 0;
        int indexCurrDragNode = -1;

        public Form1()
        {
            this.WindowState = FormWindowState.Maximized;
            this.Load += Form1_Load;
            this.Paint += Form1_Paint;
            this.KeyDown += Form1_KeyDown;
            this.MouseDown += Form1_MouseDown;
            this.MouseMove += Form1_MouseMove;
            this.MouseUp += Form1_MouseUp;
            tt.Tick += Tt_Tick;
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if (CurrentMouseMode == Modes.DRAG)
            {
                indexCurrDragNode = -1;
                drawd(CreateGraphics());
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {

            if (CurrentMouseMode == Modes.DRAG && indexCurrDragNode != -1)
            {
                obj.ModifyCtrlPoint(indexCurrDragNode, e.X, e.Y);
                drawd(CreateGraphics());
            }
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {

            switch (CurrentMouseMode)
            {
                case Modes.CTRL_POINTS:

                    obj.SetControlPoint(new Point(e.X, e.Y));
                    numOfCtrlPoints++;

                    break;

                case Modes.DRAG:
                    indexCurrDragNode = obj.isCtrlPoint(e.X, e.Y);
                    break;
            }

            drawd(CreateGraphics());
        }

        private void Tt_Tick(object sender, EventArgs e)
        {
            drawd(CreateGraphics());

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {

                case Keys.Space:
                    if (CurrentMouseMode == Modes.DRAG)
                        CurrentMouseMode = Modes.CTRL_POINTS;
                    else
                        CurrentMouseMode = Modes.DRAG;
                    break;
                case Keys.A:
                    obj.wonfile();
                    break;
                default:
                    break;
            }
            drawd(CreateGraphics());

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            drawd(CreateGraphics());

        }

        private void Form1_Load(object sender, EventArgs e)
        {

            off = new Bitmap(ClientSize.Width, ClientSize.Height);
            map = new Bitmap("C:\\Users\\Omar\\source\\repos\\ZimZimma\\ZimZimma\\bin\\bitmapimgs\\Riverbed.bmp");
            obj.setffile();
            drawd(CreateGraphics());
        }

        void draw(Graphics g)
        {
            g.Clear(Color.White);
           // g.DrawImage(map, 0, 0);

            obj.DrawCurve(g);


        }

        void drawd(Graphics g)
        {
            Graphics g2 = Graphics.FromImage(off);
            draw(g2);
            g.DrawImage(off, 0, 0);

        }
    }
}
