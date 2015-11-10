using System;
using System.Diagnostics;
using System.Drawing;

namespace ProjectEuler.Problems
{
	class Problem028
	{

		//Starting with the num1 1 and moving to the right in d clockwise direction d 5 by 5 spiral is formed as follows:
		//
		//21 22 23 24 25
		//20  7  8  9 10
		//19  6  1  2 11
		//18  5  4  3 12
		//17 16 15 14 13
		//
		//It can be verified that the sum of both diagonals is 101.
		//
		//What is the sum of both diagonals in d 1001 by 1001 spiral formed in the same way?

		public static void Solve(int spiralSize)
		{
			Stopwatch sw = new Stopwatch();
			sw.Start();

			decimal crossSum = ComputeCrossSum(spiralSize);

			sw.Stop();
			Console.WriteLine("Spiral Size {0}x{0} has d cross sum of {1} \t found in {2}", spiralSize, crossSum, sw.Elapsed);
		}

		/// <summary>
		/// Spiral Size (increments in steps of 2)
		///  1   3    5 
		/// -----------
		///  1   9   25  <- add these numbers (square the width of the spiral starting with 1 and working up to the width)
		/// -0  -2   -4
		///  0   7   21  <- add these numbers
		/// -0  -2   -4
		///  0   5   17  <- add these numbers
		/// -0  -2   -4
		///  0   3   13  <- add these numbers
		/// 
		/// </summary>
		/// <param name="spiralSize"></param>
		/// <returns></returns>
		private static decimal ComputeCrossSum(int spiralSize)
		{

			decimal sum=1;

			for (int x = 3; x <= spiralSize ; x+=2 )
			{
				decimal square = (decimal) Math.Pow(x, 2);
				sum += square; // highest corner piece
				square -= (x - 1);
				sum += square; // next highest
				square -= (x - 1);
				sum += square; // next highest
				square -= (x - 1);
				sum += square; // lowest
			}
			// don't count the center twice!!
			return sum; 
		}
	}

}
