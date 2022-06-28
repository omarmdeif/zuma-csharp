using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace ZimZimma
{
    public class DDA
    {
        public float xs, ys, xe, ye, tempx, tempy, dx, dy, m, invM, currX, currY;
        int Speed = 10;
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
        }

    }
}
