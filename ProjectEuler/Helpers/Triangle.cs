using System;
using System.Drawing;
using System.Text;

namespace ProjectEuler.Helpers
{
	public class Triangle
	{
		private Point _a;
		private Point _b;
		private Point _c;
		private Line _lineAB;
		private Line _lineAC;
		private Line _lineBC;
		private double? _area;
		private bool? _containsOrigin;
		private double _aRadians; //CA-AB
		private double _bRadians; //AB-BC
		private double _cRadians; //AC-CB
		private bool? _isRightTriangle;


		public double Area
		{
		
			get
			{
				if (_area == null)
				{
					double p = _a.X - _c.X;
					double q = _a.Y - _c.Y;
					double r = _b.X - _c.X;
					double s = _b.Y - _c.Y;
					_area = 0.5 * Math.Abs(p * s - q * r);
				}

				return (double)_area;

			}
		}
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

		public bool IsRightTriangle
		{
			get
			{
				if(_isRightTriangle==null)
				{
					DetermineIfRightTriangle();
				}
				return (_isRightTriangle == true);
			}
		}

		public bool ContainsOrigin
		{
			get
			{
				if (_containsOrigin == null)
				{
					DetermineIfContainsOrigin();
				}
				return (_containsOrigin == true);
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
			double a2 = Math.Pow(a, 2);

			double b = AC.Length;
			double b2 = Math.Pow(b, 2);

			double c = AB.Length;
			double c2 = Math.Pow(c, 2);
			_aRadians = (b == 0 || c == 0) ? 0 : Math.Acos((b2 + c2 - a2) / (2 * b * c));
			_bRadians = (a == 0 || c == 0) ? 0 : Math.Acos((a2 + c2 - b2) / (2 * a * c));
			_cRadians = (a == 0 || b == 0) ? 0 : Math.Acos((a2 + b2 - c2) / (2 * a * b));

		}

		private void DetermineIfContainsOrigin()
		{
			// check the smallest angle between AB and AC
			Point O = new Point(0, 0);
			Triangle tA = new Triangle(O, B, C);
			Triangle tB = new Triangle(A, O, C);
			Triangle tC = new Triangle(A, B, O);

			if ((Math.Round(tA.ADegrees, 10) == 180d) ||
				(Math.Round(tB.BDegrees, 10) == 180d) ||
				(Math.Round(tC.CDegrees, 10) == 180d))
				_containsOrigin = false; // line is directly on the origin!
			else
			{
				double sumAngles = Math.Round(tA.ADegrees + tB.BDegrees + tC.CDegrees, 10);
				_containsOrigin = (360d == sumAngles);
			}
		}


		private void DetermineIfRightTriangle()
		{
			_isRightTriangle = (Math.Round(ADegrees, 10) == 90.00d || Math.Round(BDegrees, 10) == 90.00d || Math.Round(CDegrees, 10) == 90.00d);
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

	public class Line
	{
		private Point _pa;
		private Point _pb;

		private int _deltaX;
		private int _deltaY;

		private double _length;

		public Point A
		{
			get { return _pa; }
			set { _pa = value; }
		}

		public Point B
		{
			get { return _pb; }
			set { _pb = value; }
		}

		public double Length { get { return _length; } }

		public Line(Point A, Point B)
		{
			_pa = A;
			_pb = B;
			CalculateLine();
		}

		private void CalculateLine()
		{
			// calc deltas
			_deltaX = Math.Max(A.X, B.X) - Math.Min(A.X, B.X);
			_deltaY = Math.Max(A.Y, B.Y) - Math.Min(A.Y, B.Y);

			// calc length
			_length = Math.Sqrt(_deltaX * _deltaX + _deltaY * _deltaY);

		}
	}

	public class PointD 
	{
		public double X;
		public double Y;
		public PointD( double x, double y)
		{
			X = x;
			Y = y;
		}

		public override string ToString()
		{
			return String.Format("Point({0},{1})", X, Y);
		}

	}
	
	public class LineDIntersection
	{
		public bool LinesAreParallel;
		public bool LinesAreCoincident;
		public bool LinesIntersect;
		public PointD Intersection;
		public double ua;
		public double ub;

	}

	public class LineD
	{
		public override string ToString()
		{
			return String.Format("Line from {0} to {1}", _pa, _pb);
		}
		private PointD _pa;
		private PointD _pb;

		private double _deltaX;
		private double _deltaY;

		private double _length;

		public PointD A
		{
			get { return _pa; }
			set { _pa = value; }
		}

		public PointD B
		{
			get { return _pb; }
			set { _pb = value; }
		}

		private void CalculateLine()
		{
			// calc deltas
			_deltaX = Math.Max(A.X, B.X) - Math.Min(A.X, B.X);
			_deltaY = Math.Max(A.Y, B.Y) - Math.Min(A.Y, B.Y);

			// calc length
			_length = Math.Sqrt(_deltaX * _deltaX + _deltaY * _deltaY);

		}

		public double Length { get { return _length; } }

		public LineD(PointD A, PointD B)
		{
			_pa = A;
			_pb = B;
			CalculateLine();
		}

		public LineDIntersection GetIntersection(LineD b)
		{
			// http://local.wasp.uwa.edu.au/~pbourke/geometry/lineline2d/

			double x1 = A.X;
			double x2 = B.X;
			double x3 = b.A.X;
			double x4 = b.B.X;
			double y1 = A.Y;
			double y2 = B.Y;
			double y3 = b.A.Y;
			double y4 = b.B.Y;
			double numua = (x4 - x3)*(y1 - y3) - (y4 - y3)*(x1 - x3);
			double numub = (x2 - x1) * (y1 - y3) - (y2 - y1) * (x1 - x3);
			double denom = (y4 - y3) * (x2 - x1) - (x4 - x3) * (y2 - y1);
			//double denom = (b.B.Y - b.A.Y) * (B.X - A.X) - (b.B.X - b.A.X) * (B.Y - A.Y);
			//double uanum = (b.B.X - b.A.X) * (A.Y - b.A.Y) - (b.B.Y - b.A.Y) * (A.X - b.A.X);
			//double ubnum = (B.X - A.X) * (A.Y - b.A.Y) - (B.Y - A.Y) * (B.Y - A.Y);

			LineDIntersection intersection = new LineDIntersection();

			if (denom == 0)
			{
				if (numua == 0 && numub == 0)
				{
					// lines are coincident!
					intersection.LinesIntersect = false;
					intersection.LinesAreCoincident = true;
					return intersection;
				}

				// lines are parallel!
				intersection.LinesIntersect = false;
				intersection.LinesAreParallel = true;
				return intersection;
			}
			else
			{
				double ua = (numua / denom);
				double ub = (numub / denom);
				intersection.ua = ua;
				intersection.ub = ub;

				if (ua >= 0.0d && ua <= 1.0d && ub >= 0.0d && ub <= 1.0d)
				{
					double x = x1 + ua*(x2 - x1);
					double y = y1 + ua*(y2 - y1);
					intersection.LinesIntersect = true;
					intersection.Intersection = new PointD(x, y);
				}
				else
				{
					intersection.LinesIntersect = false;
				}

				return intersection;
			}

		}
	}

	public class LineF
	{
		private PointF _pa;
		private PointF _pb;

		private double _deltaX;
		private double _deltaY;

		private double _length;

		public PointF A
		{
			get { return _pa; }
			set { _pa = value; }
		}

		public PointF B
		{
			get { return _pb; }
			set { _pb = value; }
		}

		public double Length { get { return _length; } }

		public LineF(PointF A, PointF B)
		{
			_pa = A;
			_pb = B;
			CalculateLine();
		}

		private void CalculateLine()
		{
			// calc deltas
			_deltaX = Math.Max(A.X, B.X) - Math.Min(A.X, B.X);
			_deltaY = Math.Max(A.Y, B.Y) - Math.Min(A.Y, B.Y);

			// calc length
			_length = Math.Sqrt(_deltaX * _deltaX + _deltaY * _deltaY);

		}

		public PointF? GetIntersection(LineF b)
		{
			// http://local.wasp.uwa.edu.au/~pbourke/geometry/lineline2d/
			PointF? intersection = null;

			double denom = (b.B.Y - b.A.Y)*(B.X - A.X) - (b.B.X - b.A.X)*(B.Y - A.Y);
			double uanum = (b.B.X - b.A.X)*(A.Y - b.A.Y) - (b.B.Y - b.A.Y)*(A.X - b.A.X);
			double ubnum = (B.X - A.X) * (A.Y - b.A.Y) - (B.Y-A.Y)*(B.Y - A.Y);

			if (denom == 0 || uanum == 0 || ubnum == 0)
			{
				// lines or coincident
				intersection = null;
			}
			else
			{
				double x = A.X + (uanum/denom)*(B.X - A.X);
				double y = B.Y + (ubnum/denom)*(B.Y - A.Y);
				intersection = new PointF((float)x,(float)y);
			}
			
			return intersection;
		}
	}


}
