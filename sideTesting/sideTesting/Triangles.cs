using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;


namespace sideTesting
{

	public class Triangle
	{
		private Point _a;
		private Point _b;
		private Point _c;
		private Line _lineAB;
		private Line _lineAC;
		private Line _lineBC;
		private Line _area;
		private bool? _containsOrigin;
		private double _aRadians; //CA-AB
		private double _bRadians; //AB-BC
		private double _cRadians; //AC-CB



		public Point A
		{
			get { return _a; }
		}

		public Point B
		{
			get { return _b; }
		}

		public Point C
		{
			get { return _c; }
		}

		public Line AB
		{
			get { return _lineAB; }
		}

		public Line AC
		{
			get { return _lineAC; }
		}

		public Line BC
		{
			get { return _lineBC; }
		}

		public bool ContainsOrigin
		{
			get 
			{
				if (_containsOrigin == null)
				{
					DetermineIfContainsOrigin();
				}
				return (_containsOrigin==true); 
			}
		}

		public double ARadians
		{
			get { return _aRadians; }
		}

		public double ADegrees
		{
			get { return ToDegrees(_aRadians); }
		}

		public double BRadians
		{
			get { return _bRadians; }
		}

		public double BDegrees
		{
			get { return ToDegrees(_bRadians); }
		}

		public double CRadians
		{
			get { return _cRadians; }
		}

		public double CDegrees
		{
			get { return ToDegrees(_cRadians); }
		}

		public Triangle(Point A, Point B, Point C)
		{
			_a = A;
			_b = B;
			_c = C;
			_lineAB = new Line(A, B);
			_lineAC = new Line(A, C);
			_lineBC = new Line(B, C);
			CalculateAngles();
		}


		private void CalculateAngles()
		{
			//http://answers.google.com/answers/threadview?id=38131
			
			//A := arccos((b^2 + c^2 - a^2)/2bc) 
			//B := arccos((a^2 + c^2 - b^2)/2ac) 
			//C := arccos((a^2 + b^2 - c^2)/2ab) 

			double a = BC.Length;
			double a2 = Math.Pow(a,2);

			double b = AC.Length;
			double b2 = Math.Pow(b,2);

			double c = AB.Length;
			double c2 = Math.Pow(c,2);
			_aRadians = (b == 0 || c == 0) ? 0:Math.Acos((b2 + c2 - a2) / (2 * b * c));			
			_bRadians = (a == 0 || c == 0) ? 0: Math.Acos((a2 + c2 - b2) / (2 * a * c));
			_cRadians = (a == 0 || b == 0) ? 0 : Math.Acos((a2 + b2 - c2) / (2 * a * b));

		}

		private void DetermineIfContainsOrigin()
		{
			// check the smallest angle between AB and AC
			Point O = new Point(0, 0);
			Triangle tA = new Triangle(O, B, C);
			Triangle tB = new Triangle(A, O, C);
			Triangle tC = new Triangle(A, B, O);
			


			if ((Math.Round(tA.ADegrees,10) == 180d) ||
				(Math.Round(tB.BDegrees, 10) == 180d) ||
				(Math.Round(tC.CDegrees, 10) == 180d))
				_containsOrigin = false; // line is directly on the origin!
			else
			{
				double sumAngles = Math.Round(tA.ADegrees + tB.BDegrees + tC.CDegrees, 10);
				_containsOrigin = (360d == sumAngles);
			}




		}


		private double ToDegrees(double radians)
		{
			return 180 * radians / Math.PI;
		}

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendFormat("A: {0}, B: {1}, C: {2} | ", A, B, C);
			sb.AppendFormat("AB: {0}, AC: {1}, BC: {2} | ", AB.Length, AC.Length, BC.Length);
			sb.AppendFormat("<A: {0:N2}, <B: {1:N2}, <C: {2:N2} | ", ADegrees, BDegrees, CDegrees);
			sb.AppendFormat("Contains Origin: {0} ", ContainsOrigin);
			return sb.ToString();
		}

		

	}

}
