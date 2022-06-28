using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace ZimZimma
{
    public class target
    {
        public Bitmap img;
        public Bitmap eimg;
        public float x;
        public Random rr = new Random();
        public float y;
        public float pos = 0.0f;
        public PointF pnn;
        public int w;
        public PointF initpos;
        public bool cb = false;
        public int exp = 0;
        public target(curve c)
        {
            w = rr.Next(1, 6);
            img = new Bitmap($"assets\\{w}.bmp");
            //pnn = new PointF();
            img.MakeTransparent(img.GetPixel(0, 0));
            eimg = new Bitmap("assets\\ex.bmp");
            eimg.MakeTransparent(eimg.GetPixel(0, 0));
            pnn = c.CalcCurvePointAtTime(pos);
            initpos = pnn;
            x = pnn.X;
            y = pnn.Y;
            x -= (img.Width / 2);
            y -= (img.Height / 2);
        }

        public target(float fx, float fy, int wb)
        {
            w = wb;
            img = new Bitmap($"assets\\{w}.bmp");
            //pnn = new PointF();
            img.MakeTransparent(img.GetPixel(0, 0));
            eimg = new Bitmap("assets\\ex.bmp");
            eimg.MakeTransparent(eimg.GetPixel(0, 0));
            x = fx;
            y = fy;
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
            if(exp > 0)
            {
                g.DrawImage(eimg, x, y);
            }
            else if(exp == 0)
            {
                g.DrawImage(img, x, y);
            }
            
        }

        public float xs, ys, xe, ye, tempx, tempy, dx, dy, m, invM, currX, currY;
        int Speed = 20;
        public void SetVals(float a, float b, float c, float d)
        {
            xs = a;
            ys = b;
            xe = c;
            ye = d;
            //////////////////
            dx = xe - xs;
            dy = ye - ys;
            m = dy / dx;
            invM = dx / dy;
            /////////////////
            currX = xs;
            currY = ys;
        }


        public void MoveStep()
        {
            if (Math.Abs(dx) > Math.Abs(dy))
            {
                if (xs < xe)
                {
                    currX += Speed;
                    currY += m * Speed;
                    if (currX >= xe)
                    {
                        SetVals(xe, ye, xs, ys);
                    }
                }
                else
                {
                    currX -= Speed;
                    currY -= m * Speed;
                    if (currX <= xe)
                    {
                        SetVals(xe, ye, xs, ys);
                    }
                }
            }
            else
            {
                if (ys < ye)
                {
                    currY += Speed;
                    currX += invM * Speed;
                    if (currY >= ye)
                    {
                        SetVals(xe, ye, xs, ys);
                    }
                }
                else
                {
                    currY -= Speed;
                    currX -= invM * Speed;
                    if (currY <= ye)
                    {
                        SetVals(xe, ye, xs, ys);
                    }
                }
            }
            x = currX;
            y = currY;

        }
    }
}
