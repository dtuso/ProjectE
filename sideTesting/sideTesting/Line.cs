using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;


namespace sideTesting
{
	public class Line
	{
		private Point _pa;
		private Point _pb;

		private int _deltaX;
		private int _deltaY;

		private double _length;
		//private double _slopeX;
		//private double _slopeY;
		//private double _crossesXat;
		//private double _crossesYat;
		//private double _angleFromA;
		//private double _angleFromB;

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

		//public double Slope { get { return _slope; } }

		//public double AngleFromA { get { return _angleFromA; } }

		//public double AngleFromB { get { return _angleFromB; } }

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
}
