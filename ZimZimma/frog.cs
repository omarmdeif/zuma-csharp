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
        public frog(Size f)
        {
            img = new Bitmap(@"C:\Users\Omar\source\repos\ZimZimma\ZimZimma\bin\bitmapimgs\nfrog.bmp");
            img.MakeTransparent(img.GetPixel(0, 0));
            x = (f.Width / 2) - (img.Width / 2);
            y = (f.Height / 2) - (img.Height / 2);
        }
        public void swivel(float prevx, float prevy, float currx, float curry)
        {
            if(prevx > currx)
            {
                if(prevy > curry)
                {
                    xdiff = prevx - currx;
                    ydiff = prevy - curry;
                }
                else
                {
                    xdiff = prevx - currx;
                    ydiff = curry - prevy;
                }
            }
            else
            {
                if (prevy > curry)
                {
                    xdiff = currx - prevx;
                    ydiff = prevy - curry;
                }
                else
                {
                    xdiff = currx - prevx;
                    ydiff = curry - prevy;
                }
            }
            
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
            img = RotateImage(img, angle);
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

        public void shoot(List<target> balls)
        {
            for (int i = 0; i < balls.Count; i++)
            {

            }
        }

        public void draw(Graphics g)
        {
            g.DrawImage(img, x, y);
        }

    }
}
