using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;

namespace ZimZimma
{
    public class curve
    {
		public List<Point> ControlPoints;
		public List<PointF> secCurvepoints;
		public Point pnn;

		public float t_inc = 0.0001f;

		public Color cl = Color.Red;
		public Color clr1 = Color.Black;
		public Color ftColor = Color.Black;
		public Color bl = Color.Blue;
		public Color rd = Color.Red;

		public List<PointF> bCurvePoints;


		public curve()
		{
			ControlPoints = new List<Point>();
			secCurvepoints = new List<PointF>();
			bCurvePoints = new List<PointF>();
		}


		private float Factorial(int n)
		{
			float res = 1.0f;

			for (int i = 2; i <= n; i++)
				res *= i;

			return res;
		}

		private float C(int n, int i)
		{
			float res = Factorial(n) / (Factorial(i) * Factorial(n - i));
			return res;
		}

		private double Calc_B(float t, int i)
		{
			int n = ControlPoints.Count - 1;
			double res = C(n, i) *
							Math.Pow((1 - t), (n - i)) *
							Math.Pow(t, i);
			return res;
		}

		public Point GetPoint(int i)
		{
			return ControlPoints[i];
		}

		public PointF CalcCurvePointAtTime(float t)
		{
			PointF pt = new PointF();
			for (int i = 0; i < ControlPoints.Count; i++)
			{
				float B = (float)Calc_B(t, i);
				pt.X += B * ControlPoints[i].X;
				pt.Y += B * ControlPoints[i].Y;
			}

			return pt;
		}

		private void DrawControlPoints(Graphics g)
		{
			Font Ft = new Font("System", 10);
			for (int i = 0; i < ControlPoints.Count; i++)
			{
				g.FillEllipse(new SolidBrush(clr1),
								ControlPoints[i].X - 5,
								ControlPoints[i].Y - 5, 10, 10);

				g.DrawString("P# " + i, Ft, new SolidBrush(ftColor), ControlPoints[i].X - 15, ControlPoints[i].Y - 15);
			}
		}

		public void setffile()
        {
			StreamReader sr = new StreamReader(@"C:\Users\Omar\source\repos\ZimZimma\ZimZimma\bin\bitmapimgs\lcp.txt", true);
            while (!sr.EndOfStream)
            {
				string[] a = sr.ReadLine().Split(',');
				pnn = new Point(Int32.Parse(a[0]), Int32.Parse(a[1]));
				ControlPoints.Add(pnn);
            }
        }

		public int isCtrlPoint(int XMouse, int YMouse)
		{
			Rectangle rc;
			for (int i = 0; i < ControlPoints.Count; i++)
			{
				rc = new Rectangle(ControlPoints[i].X - 5, ControlPoints[i].Y - 5, 10, 10);
				if (XMouse >= rc.Left && XMouse <= rc.Right && YMouse >= rc.Top && YMouse <= rc.Bottom)
				{
					return i;
				}
			}
			return -1;
		}

		public void ModifyCtrlPoint(int i, int XMouse, int YMouse)
		{
			Point p = ControlPoints[i];

			p.X = XMouse;
			p.Y = YMouse;
			ControlPoints[i] = p;
		}

		public void SetControlPoint(Point pt)
		{
			ControlPoints.Add(pt);
		}



		private void DrawCurvePoints(Graphics g)
		{
			if (ControlPoints.Count <= 0)
				return;

			PointF curvePoint;
			for (float t = 0.0f; t <= 1.0; t += t_inc)
			{
				curvePoint = CalcCurvePointAtTime(t);
				//secCurvepoints.Add(curvePoint);
				g.FillEllipse(new SolidBrush(cl),
										curvePoint.X - 4, curvePoint.Y - 4,
										8, 8);
			}
		}

		public void setseccurvepoints(float t)
		{
			PointF curvePoint;
			curvePoint = CalcCurvePointAtTime(t);
			bCurvePoints.Add(curvePoint);
		}

		private void drawseccurve(Graphics g)
		{
			for (int i = 0; i < bCurvePoints.Count; i++)
			{
				g.FillRectangle(Brushes.Blue, bCurvePoints[i].X, bCurvePoints[i].Y - 5, 8, 100);
			}
		}

		public bool isSecCurve(float t)
		{
			PointF curvePoint;
			curvePoint = CalcCurvePointAtTime(t);
			for (int i = 0; i < bCurvePoints.Count; i++)
			{
				if (curvePoint == bCurvePoints[i])
				{
					return true;
				}
			}
			return false;
		}

		public void wonfile()
        {
			StreamWriter sw = new StreamWriter(@"C:\Users\Omar\source\repos\ZimZimma\ZimZimma\bin\bitmapimgs\lcp.txt", true);
            for (int i = 0; i < ControlPoints.Count; i++)
            {
				sw.WriteLine($"{ControlPoints[i].X},{ControlPoints[i].Y}");
            }
			sw.Close();
        }

		public void DrawCurve(Graphics g)
		{
			DrawControlPoints(g);
			DrawCurvePoints(g);
			//drawseccurve(g);
		}


	}
}
