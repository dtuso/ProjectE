using System;
using System.Collections.Generic;
using System.Text;
using ProjectEuler.Helpers;

namespace ProjectEuler.Problems
{

	// http://en.wikipedia.org/wiki/Pell%27s_equation
	
	// http://www.physicsforums.com/showpost.php?s=c4451a8bbc08cfa3f0b20f33dc15acdc&p=274367&postcount=4

	class Problem066
	{
		// Consider quadratic Diophantine equations of the form:

		// x^2 – Dy^2 = 1

		// For example, when D=13, the minimal solution in x is 649^(2) – 13×180^(2) = 1.

		// It can be assumed that there are no solutions in positive integers when D is square.

		// By finding minimal solutions in x for D = {2, 3, 5, 6, 7}, we obtain the following:

		//  3^2 – 2×2^2 = 1
		//  2^2 – 3×1^2 = 1
		//  9^2 – 5×4^2 = 1
		//  5^2 – 6×2^2 = 1
		//  8^2 – 7×3^2 = 1

		//Hence, by considering minimal solutions in x for D ≤ 7, the largest x is obtained when D=5.

		//Find the value of D ≤ 1000 in minimal solutions of x for which the largest value of x is obtained.

		public static void Solve(int MAX_D)
		{
			Console.WriteLine("Problem 66, MAX D={0}", MAX_D);

			ulong max_x = 0;
			int max_x_D = 0;
			for(int D = MAX_D; D >= 2; D--)
			{
				if (MyMath.IsSquareNumber(D)) continue;
				ulong x = FindMinimalSolutionX_Double(D);
				Console.WriteLine("D: {0,4} min x: {1}", D, x);
				if (x > max_x)
				{
					max_x = x;
					max_x_D = D;
				}
			}
			Console.WriteLine("Max X = {0} at D = {1}", max_x, max_x_D);
		}


		private static ulong FindMinimalSolutionX(int D)
		{
			ulong minX = 0;
			int y = 0;
			while (minX == 0)
			{
				y++;
				//// x^2 - D * y^2 = 1
				//// x^2 = 1 + D * y^2
				//// x = _V(1 + D * y^2);
				decimal y2 = checked(((decimal)y) * y);
				decimal dy2 = checked(D * y2);
				decimal dy2p1 = checked(1 + dy2);

				decimal lower = MyMath.GuessSquareRoot(dy2p1, 15);
				decimal x =  Decimal.Floor(lower);
				while (true)
				{
					decimal x2 = checked(x * x);
					if (x2 > dy2p1)
						break;
					if (x2 == dy2p1)
					{
						minX = (ulong)x;
						break;
					}
					x++;
				}
			}
			return minX;
		}
		private static ulong FindMinimalSolutionX_Double(int D)
		{
			ulong minX = 0;
			int y = 0;
			while (true)
			{
				y++;
				//// x^2 - D * y^2 = 1
				//// x^2 = 1 + D * y^2
				//// x = _V(1 + D * y^2);
				double y2 = Math.Pow(y, 2);

				double dy2 = checked(D * y2);
				double dy2p1 = checked(1 + dy2);

				//if(dy2p1 != Math.Floor(dy2p1))
				//{
				//    Console.WriteLine("Uh-oh: {0} y: {1} {2}",D, y, dy2p1);
				//}
				double x = Math.Sqrt(dy2p1);
				if(Math.Pow(x,2) != dy2p1)
				{
					//Console.WriteLine("Uh-oh: {0} x: {1} {2} {3}", D, x, Math.Pow(x,2), dy2p1);
					double xf = Math.Floor(x);
					if(Math.Pow(xf,2)==dy2p1)
					{
						minX = Convert.ToUInt64(xf.ToString());
						break;
					}
				}
				if (MiscFunctions.IsInteger(x))
				{
					minX = Convert.ToUInt64(x.ToString());
					break;
				}
			}
			return minX;
		}
		private static int FindMinimalSolutionX_by_BigInteger(int D)
		{
			int minX = 0;
			BigInteger y = 0;
			while (minX == 0)
			{
				y++;
				//// x^2 - D * y^2 = 1
				//// x^2 = 1 + D * y^2
				//// x = _V(1 + D * y^2);
				BigInteger inside = 1 + (D * (y * y));
				BigInteger x = inside.sqrt();
				BigInteger remainder = inside - (x * x);
				if (remainder == 0)
				{
					minX = Convert.ToInt32(x.ToString());
				}
			}
			return minX;
		}

		private static int FindMinimalSolutionX_looping_X(int D)
		{
			int minX = 0;
			ulong intY = 0;
			while(minX==0)
			{
				intY++;
				//// x^2 - D * y^2 = 1
				//// x^2 = 1 + D * y^2
				//// x = _V(1 + D * y^2);
				ulong y2 = checked(intY*intY);
				ulong dy2 = checked((ulong)D * y2);
				ulong dy2p1 = checked(1 + dy2);
				ulong x = 0;
				while(true)
				{
					x++;
					ulong x2 = checked(x*x);
					if (x2 > dy2p1)
						break;
					if (x2 == dy2p1)
					{
						minX = Convert.ToInt32(x.ToString());
						break;
					}
				}
			}
			return minX;
		}

	}
}
/*
Start of Main 1/5/2009 11:52:19 AM
Problem 66, MAX D=60
D:    2 min x: 3
D:    3 min x: 2
D:    5 min x: 9
D:    6 min x: 5
D:    7 min x: 8
D:    8 min x: 3
D:   10 min x: 19
D:   11 min x: 10
D:   12 min x: 7
D:   13 min x: 649
D:   14 min x: 15
D:   15 min x: 4
D:   17 min x: 33
D:   18 min x: 17
D:   19 min x: 170
D:   20 min x: 9
D:   21 min x: 55
D:   22 min x: 197
D:   23 min x: 24
D:   24 min x: 5
D:   26 min x: 51
D:   27 min x: 26
D:   28 min x: 127
D:   29 min x: 9801
D:   30 min x: 11
D:   31 min x: 1520
D:   32 min x: 17
D:   33 min x: 23
D:   34 min x: 35
D:   35 min x: 6
D:   37 min x: 73
D:   38 min x: 37
D:   39 min x: 25
D:   40 min x: 19
D:   41 min x: 2049
D:   42 min x: 13
D:   43 min x: 3482
D:   44 min x: 199
D:   45 min x: 161
D:   46 min x: 24335
D:   47 min x: 48
D:   48 min x: 7
D:   50 min x: 99
D:   51 min x: 50
D:   52 min x: 649
D:   53 min x: 66249
D:   54 min x: 485
D:   55 min x: 89
D:   56 min x: 15
D:   57 min x: 151
D:   58 min x: 19603
D:   59 min x: 530
D:   60 min x: 31
Max X = 66249
End of Main (00:00:00.4246254) 1/5/2009 11:52:19 AM
*/