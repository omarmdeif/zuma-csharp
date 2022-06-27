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
        public Form1()
        {
            this.WindowState = FormWindowState.Maximized;
            this.Load += Form1_Load;
            this.Paint += Form1_Paint;
            this.KeyDown += Form1_KeyDown;
            tt.Tick += Tt_Tick;
        }

        private void Tt_Tick(object sender, EventArgs e)
        {
            drawd(CreateGraphics());

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
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
            drawd(CreateGraphics());
        }

        void draw(Graphics g)
        {
            g.Clear(Color.White);
            g.DrawImage(map, 0, 0);


        }

        void drawd(Graphics g)
        {
            Graphics g2 = Graphics.FromImage(off);
            draw(g2);


        }
    }
}
