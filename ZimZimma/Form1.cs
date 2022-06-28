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
        public List<target> balls = new List<target>();
        public frog ff;
        public float pnnf;
        public target pnnt;
        public List<float> fs = new List<float>();
        enum Modes { CTRL_POINTS, DRAG };
        Modes CurrentMouseMode = Modes.CTRL_POINTS;
        int numOfCtrlPoints = 0;
        int whichstate = 0;
        float prevx, prevy, currx, curry;
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
            tt.Start();
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
            currx = e.X; curry = e.Y;
            if (CurrentMouseMode == Modes.DRAG && indexCurrDragNode != -1)
            {
                obj.ModifyCtrlPoint(indexCurrDragNode, e.X, e.Y);
                drawd(CreateGraphics());
            }
            if (whichstate >= 1)
            {
                ff.swivel(currx, curry, prevx, prevy);

            }
            whichstate++;
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {

            //switch (CurrentMouseMode)
            //{
            //    case Modes.CTRL_POINTS:

            //        obj.SetControlPoint(new Point(e.X, e.Y));
            //        numOfCtrlPoints++;

            //        break;

            //    case Modes.DRAG:
            //        indexCurrDragNode = obj.isCtrlPoint(e.X, e.Y);
            //        break;
            //}
            prevx = e.X; prevy = e.Y;
            ff.shoot(prevx, prevy, fs);
            drawd(CreateGraphics());
        }

        private void Tt_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < balls.Count; i++)
            {
                fs[i] += 0.003f;
                balls[i].move(obj, fs[i]);
                if (balls[i].newball() && balls.Count <= 25)
                {
                    pnnt = new target(obj);
                    pnnf = 0.0f;
                    fs.Add(pnnf);
                    balls.Add(pnnt);
                }
            }
            if(ff.myb != null)
            {
                ff.movemyb(balls, obj);
            }
            drawd(CreateGraphics());
            int rma = 999;
            for (int i = 0; i < balls.Count; i++)
            {
                if(balls[i].exp == 1)
                {
                    rma = i;
                }
            }
            if(rma != 999)
            {
                balls.RemoveAt(rma);
            }
            rma = 999;
            for (int i = 0; i < ff.myb.Count; i++)
            {
                if (ff.myb[i].exp == 1)
                {
                    rma = i;
                }
            }
            if (rma != 999)
            {
                ff.myb.RemoveAt(rma);
            }

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
                case Keys.X:
                    
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
            //map = new Bitmap("assets\\Riverbed.bmp");
            obj.setffile();
            pnnt = new target(obj);
            balls.Add(pnnt);
            pnnf = 0.0f;
            fs.Add(pnnf);
            ff = new frog(ClientSize);
            drawd(CreateGraphics());
        }

        public void draw(Graphics g)
        {
            g.Clear(Color.White);
            // g.DrawImage(map, 0, 0);
            ff.draw(g);
            
            obj.DrawCurve(g);
            for (int i = 0; i < balls.Count; i++)
            {
                balls[i].draw(g);
            }


        }

        public void drawd(Graphics g)
        {
            Graphics g2 = Graphics.FromImage(off);
            draw(g2);
            g.DrawImage(off, 0, 0);

        }
    }
}
