using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace ZimZimma
{
    public class target
    {
        public Bitmap img;
        public float x;
        public Random rr = new Random();
        public float y;
        public float pos = 0.0f;
        public PointF pnn;
        public int w;
        public PointF initpos;
        public bool cb = false;
        public target(curve c)
        {
            w = rr.Next(1, 5);
            img = new Bitmap($"C:\\Users\\Omar\\source\\repos\\ZimZimma\\ZimZimma\\bin\\bitmapimgs\\{w}.bmp");
            //pnn = new PointF();
            img.MakeTransparent(img.GetPixel(0, 0));
            pnn = c.CalcCurvePointAtTime(pos);
            initpos = pnn;
            x = pnn.X;
            y = pnn.Y;
            x -= (img.Width / 2);
            y -= (img.Height / 2);
        }
        public void move(curve c, float p)
        {
             
            PointF f = c.CalcCurvePointAtTime(p);
            x = (float)f.X;
            y = (float)f.Y;
            x -= (img.Width / 2);
            y -= (img.Height / 2);
        }

        public bool newball()
        {
            if (y < (initpos.Y - img.Height - 32) && !cb)
            {
                cb = true;
                return true;
                
            }
            else
            {
                return false;
            }
        }

        public void draw(Graphics g)
        {
            g.DrawImage(img, x, y);
        }
    }
}
