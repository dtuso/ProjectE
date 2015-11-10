using System;
using System.Collections.Generic;
using System.Diagnostics;
using ProjectEuler.Helpers;
namespace ProjectEuler.Problems
{
	class Problem058
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

		//Answer
		//26241

		static Dictionary<int, int> cntPrimesForSpiralSize = new Dictionary<int, int>();
		public static void Solve()
		{
			Stopwatch sw = new Stopwatch();
			sw.Start();
			bool greaterThan10 = true;
			for (int spiralSize = 3; greaterThan10; spiralSize += 2)
			{
				double ratio = ComputeCrossRatioOfPrimes(spiralSize);
				Console.WriteLine("S{0,2} x {0,2}  ratio = {1:P}", spiralSize, ratio);
				greaterThan10 = (ratio > 0.1);
			}

			sw.Stop();
			//Console.WriteLine("Spiral Size {0,2}x{0,2} \t found in {2}", spiralSize, sw.Elapsed);
		}

		private static double ComputeCrossRatioOfPrimes(int spiralSize)
		{
			return CountPrimes(spiralSize) / (double)(2 * spiralSize - 1);
		}

		static int CountPrimes(int spiralSize)
		{
			if (cntPrimesForSpiralSize.ContainsKey(spiralSize))
				return cntPrimesForSpiralSize[spiralSize];
			int numPrimes = 0;
			if ((spiralSize) != 3)
			{
				if (cntPrimesForSpiralSize.ContainsKey(spiralSize - 2))
					numPrimes = cntPrimesForSpiralSize[spiralSize - 2];
			}
			double square = (double)spiralSize * (double)spiralSize;// highest corner piece
			numPrimes += IsPrime(square) ? 1 : 0;

			square -= (spiralSize - 1);// next highest
			numPrimes += IsPrime(square) ? 1 : 0;

			square -= (spiralSize - 1);// next highest
			numPrimes += IsPrime(square) ? 1 : 0;

			square -= (spiralSize - 1);// lowest
			numPrimes += IsPrime(square) ? 1 : 0;

			cntPrimesForSpiralSize.Add(spiralSize, numPrimes);

			return numPrimes;

		}

		public static bool IsPrime(double number)
		{
			if (number <= 1)
				return false;
			if (number <= 3)
				return true;

			double max = Math.Sqrt(number);

			for (double den = 2; den <= max; den++)
			{
				if ((number % den) == 0)
				{
					return false;
				}
			}
			return true;

		}

		public static bool IsPrime(BigInteger number)
		{
			if (number <= 1)
				return false;
			if (number <= 3)
				return true;

			BigInteger max = 1 + number.sqrt(); 
			for (BigInteger den = 2; den <= max; den++)
			{
				if ((number % den) == 0)
				{
					return false;
				}
			}
			return true;

		}

		public static bool IsInteger(double num)
		{
			return (num == (int)num);
		}
	}
}
