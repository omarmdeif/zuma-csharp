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
            nextbi = rr.Next(1, 6);
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

            angle = (float)((Math.Atan2(ydiff, xdiff) * 180) / Math.PI);
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
            PointF newp = new PointF(img.Width, img.Height);
            // img = RotateImage(img, angle);
        }
        public static Bitmap RotateImage2(Bitmap image, PointF offset, float angle)
        {
            if (image == null)
                throw new ArgumentNullException("image");

            //create a new empty bitmap to hold rotated image
            Bitmap rotatedBmp = new Bitmap(image.Width, image.Height);
            rotatedBmp.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            //make a graphics object from the empty bitmap
            Graphics g = Graphics.FromImage(rotatedBmp);

            //Put the rotation point in the center of the image
            g.TranslateTransform(offset.X, offset.Y);

            //rotate the image
            g.RotateTransform(angle);

            //move the image back
            g.TranslateTransform(-offset.X, -offset.Y);

            //draw passed in image onto graphics object
            g.DrawImage(image, new PointF(0, 0));

            return rotatedBmp;
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

        public void shoot(float mx, float my, List<float> fs)
        {
            float gg= 0.0f;
            fs.Add(gg);
            pnn = new target(x + img.Width, y + img.Height, nextbi);
            myb.Add(pnn);
            myb[myb.Count - 1].SetVals(myb[myb.Count - 1].x, myb[myb.Count - 1].y, mx, my);
            nextbi = rr.Next(1, 6);
            Bitmap bimg = new Bitmap($"assets\\b{nextbi}.bmp");
            nextb = new Bitmap(bimg, 14, 12);
            nextbx = x + 53 - (nextb.Width / 2);
            nextby = y + 28 - (nextb.Height / 2);
        }

        public void movemyb(List<target> balls, curve c)
        {
            int rm = 999;
            for (int i = 0; i < myb.Count; i++)
            {
                myb[i].MoveStep();
                if (myb[i].x + myb[i].img.Width > 1712 || myb[i].x < 150 || myb[i].y + myb[i].img.Height > 951 || myb[i].y < 150)
                {
                    rm = i;
                }
                for (int j = 0; j < balls.Count; j++)
                {

                    if (j < balls.Count - 1)
                    {
                        //top
                        if (((myb[i].x >= balls[j].x && myb[i].x <= balls[j].x + balls[j].img.Width
                          && myb[i].y <= balls[j].y + balls[j].img.Height - 10 && myb[i].y >= balls[j].y)
                          //right
                          || (myb[i].x + myb[i].img.Width - 10 >= balls[j].x && myb[i].x <= balls[j].x
                          && myb[i].y <= balls[j].y + balls[j].img.Height - 10 && myb[i].y >= balls[j].y)
                          //left
                          || (myb[i].x <= balls[j].x + balls[j].img.Width - 10 && myb[i].x >= balls[j].x
                          && myb[i].y <= balls[j].y + balls[j].img.Height - 10 && myb[i].y >= balls[j].y)
                          //bot
                          || (myb[i].x >= balls[j].x && myb[i].x <= balls[j].x + balls[j].img.Width - 10
                          && myb[i].y + myb[i].img.Height >= balls[j].y && myb[i].y <= balls[j].y))
                          && myb[i].w == balls[j].w

                          )
                        {
                            int fct = 0, bct = 0, lf = 0, lb = 0;
                            for (int tf = j + 1; tf < balls.Count; tf++)
                            {
                                if (balls[tf].w == balls[j].w)
                                {
                                    fct++;
                                }
                                else
                                {
                                    lf = tf;
                                    break;
                                }
                            }
                            for (int tb = j - 1; tb >= 0; tb--)
                            {
                                if (balls[tb].w == balls[j].w)
                                {
                                    bct++;
                                }
                                else
                                {
                                    lb = tb;
                                    break;
                                }
                            }
                            if (fct > 0)
                            {
                                for (int tf = j; tf < lf; tf++)
                                {
                                    balls[tf].exp = 1;
                                }
                                myb[i].exp = 1;
                            }
                            if (bct > 0)
                            {
                                for (int tb = j; tb > lb; tb--)
                                {
                                    balls[tb].exp = 1;
                                }
                                myb[i].exp = 1;
                            }
                            if (fct == 0 && bct == 0)
                            {
                                myb[i].x = c.CalcCurvePointAtTime(0.0f).X;
                                myb[i].y = c.CalcCurvePointAtTime(0.0f).Y;
                                balls.Add(myb[i]);
                                rm = i;
                            }
                        }
                        else if (((myb[i].x >= balls[j].x && myb[i].x <= balls[j].x + balls[j].img.Width
                        && myb[i].y <= balls[j].y + balls[j].img.Height - 10 && myb[i].y >= balls[j].y)
                        //right
                        || (myb[i].x + myb[i].img.Width - 10 >= balls[j].x && myb[i].x <= balls[j].x
                        && myb[i].y <= balls[j].y + balls[j].img.Height - 10 && myb[i].y >= balls[j].y)
                        //left
                        || (myb[i].x <= balls[j].x + balls[j].img.Width - 10 && myb[i].x >= balls[j].x
                        && myb[i].y <= balls[j].y + balls[j].img.Height - 10 && myb[i].y >= balls[j].y)
                        //bot
                        || (myb[i].x >= balls[j].x && myb[i].x <= balls[j].x + balls[j].img.Width - 10
                        && myb[i].y + myb[i].img.Height >= balls[j].y && myb[i].y <= balls[j].y))
                        )
                        {
                                
                            balls.Add(myb[i]);
                            rm = i;

                        }
                    }
                }
            }
            if (rm != 999)
            {
                myb.RemoveAt(rm);
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
