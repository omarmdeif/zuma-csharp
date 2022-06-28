using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace ZimZimma
{
    public class frog
    {
        public Bitmap img;
        public float x;
        public float y;
        public float xdiff, ydiff, angle;
        public Random rr = new Random();
        public int nextbi;
        public Bitmap nextb;
        public float nextbx, nextby;
        public List<target> myb = new List<target>();
        public target pnn;
        public DDA mydda = new DDA();
        public frog(Size f)
        {
            img = new Bitmap("assets\\nfrog.bmp");
            img.MakeTransparent(img.GetPixel(53, 28));
            nextbi = 1;
            Bitmap bimg = new Bitmap($"assets\\b{nextbi}.bmp");
            nextb = new Bitmap(bimg, 14, 12);
            x = (f.Width / 2) - (img.Width / 2);
            y = (f.Height / 2) - (img.Height / 2);
            nextbx = x + 53 - (nextb.Width / 2);
            nextby = y + 28 - (nextb.Height / 2);
        }
        public void swivel(float prevx, float prevy, float currx, float curry)
        {
            //if(prevx > currx)
            //{
            //    if(prevy > curry)
            //    {
            //        xdiff = prevx - currx;
            //        ydiff = prevy - curry;
            //    }
            //    else
            //    {
            //        xdiff = prevx - currx;
            //        ydiff = curry - prevy;
            //    }
            //}
            //else
            //{
            //    if (prevy > curry)
            //    {
            //        xdiff = currx - prevx;
            //        ydiff = prevy - curry;
            //    }
            //    else
            //    {
            //        xdiff = currx - prevx;
            //        ydiff = curry - prevy;
            //    }
            //}

            xdiff = currx - prevx;
            ydiff = curry - prevy;

            angle = (float)(Math.Atan2(ydiff, xdiff) * 180.0f / Math.PI);
            //Bitmap rotatede = new Bitmap(img, img.Width, img.Height);
            ////g = Graphics.FromImage(img);
            ////g.TranslateTransform(img.Width / 2, img.Height / 2);
            ////g.RotateTransform(angle);
            ////g.TranslateTransform(-(float)img.Width / 2, -(float)img.Height / 2);
            //Bitmap rotated = new Bitmap(img.Width, img.Height);
            //rotated.SetResolution(img.HorizontalResolution, img.VerticalResolution);
            //using (Graphics g = Graphics.FromImage(rotated))
            //{
            //    g.TranslateTransform(img.Width / 2, img.Height / 2);
            //    g.RotateTransform(angle);
            //    g.TranslateTransform(- img.Width / 2, -img.Height / 2);
            //    g.DrawImage(img, new Point(0, 0));
            //}
            // img = RotateImage(img, angle);
        }

        public Bitmap RotateImage(Bitmap img, float rotationAngle)
        {
            //create an empty Bitmap image
            Bitmap bmp = new Bitmap(img.Width, img.Height);

            //turn the Bitmap into a Graphics object
            Graphics gfx = Graphics.FromImage(bmp);

            //now we set the rotation point to the center of our image
            gfx.TranslateTransform((float)bmp.Width / 2, (float)bmp.Height / 2);

            //now rotate the image
            gfx.RotateTransform(rotationAngle);

            gfx.TranslateTransform(-(float)bmp.Width / 2, -(float)bmp.Height / 2);

            //set the InterpolationMode to HighQualityBicubic so to ensure a high
            //quality image once it is transformed to the specified size
            gfx.InterpolationMode = InterpolationMode.HighQualityBicubic;

            //now draw our new image onto the graphics object
            gfx.DrawImage(img, new Point(0, 0));

            //dispose of our Graphics object
            gfx.Dispose();

            //return the image
            return bmp;
        }

        public void shoot(float mx, float my)
        {
            pnn = new target(x + img.Width, y + img.Height, nextbi);
            myb.Add(pnn);
            myb[myb.Count - 1].SetVals(myb[myb.Count - 1].x, myb[myb.Count - 1].y, mx, my);

            nextbi = 1;
            Bitmap bimg = new Bitmap($"assets\\b{nextbi}.bmp");
            nextb = new Bitmap(bimg, 14, 12);
            nextbx = x + 53 - (nextb.Width / 2);
            nextby = y + 28 - (nextb.Height / 2);
        }

        public void movemyb(List<target> balls)
        {
            int rm = 999;
            int rmb = 999;
            for (int i = 0; i < myb.Count; i++)
            {
                myb[i].MoveStep();
                if (myb[i].x > 1920 || myb[i].x < 0 || myb[i].y > 1057 || myb[i].y < 0)
                {
                    rm = i;
                }

                if (myb[i].x > ((balls[balls.Count - 1].x + balls[balls.Count - 1].img.Width) + 50)
                    || myb[i].x < 150 || myb[i].y > (1050 - myb[i].img.Height) || myb[i].y < 150)
                {
                    rm = i;
                }
                for (int j = 0; j < balls.Count; j++)
                {
                    if (myb[i].x >= balls[j].x && myb[i].x <= balls[i + 1].x
                      && myb[i].y >= balls[i].y && myb[i].y <= balls[i + 1].y)
                    {
                        if(myb[i].w == balls[j].w)
                        {
                            rmb = j;
                        }
                    }
                }
            }
            if (rm != 999)
            {
                myb.RemoveAt(rm);
            }
            if (rmb != 999)
            {
                balls.RemoveAt(rmb);
            }
        }

        public void draw(Graphics g)
        {
            g.DrawImage(img, x, y);
            g.DrawImage(nextb, nextbx, nextby);
            if (myb.Count > 0)
            {
                for (int i = 0; i < myb.Count; i++)
                {
                    g.DrawImage(myb[i].img, myb[i].x, myb[i].y);
                }
            }


        }

    }
}
