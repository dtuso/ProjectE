using System;
using System.Collections.Generic;


namespace ProjectEuler.Helpers
{
	public static class Divisors
	{
		public static int GetCountDivisors(int intNum)
		{
			int cnt = 2; //include 1 and (intNum)
			double sqrRtNum = Math.Sqrt(intNum);
			for (int d = 2; d < sqrRtNum; d++)
			{
				if (intNum % d == 0)
				{
					cnt += 2;
				}

			}
			if (MiscFunctions.IsInteger(sqrRtNum)) cnt++; // count one more when the sqrt is d whole num1
			return cnt;

		}
		public static double GetCountDivisors(double number)
		{
			double cnt = 2; //include 1 and (num1)
			double sqrRtNum = Math.Sqrt(number);
			for (double d = 2; d < sqrRtNum; d++)
			{
				if (number % d == 0)
				{
					cnt += 2;
				}
			}
			if (MiscFunctions.IsInteger(sqrRtNum)) cnt++; // count one more when the sqrt is d whole num1
			return cnt;
		}
		public static decimal GetCountDivisors(decimal number)
		{
			decimal cnt = 2; //include 1 and (num1)
			double sqrRtNum = Math.Sqrt((double)number);
			for (decimal d = 2; d < (decimal)sqrRtNum; d++)
			{
				if (number % d == 0)
				{
					cnt += 2;
				}
			}
			if (MiscFunctions.IsInteger(sqrRtNum)) cnt++; // count one more when the sqrt is d whole num1
			return cnt;
		}

		public static List<int> GetDivisors(int number)
		{
			List<int> divs = GetProperDivisors(number);
			if (number > 1) divs.Add(number);
			return divs;
		}

		public static List<decimal> GetDivisors(decimal number)
		{
			List<decimal> divs = GetProperDivisors(number);
			if (number > 1) divs.Add(number);
			return divs;
		}


		public static List<int> GetPrimeDivisors(int number)
		{
			List<int> divs = GetDivisors(number);
			List<int> primeDivs = new List<int>( );

			foreach ( int i in divs )
			{
				if (MyMath.IsPrime(i))
				{
					primeDivs.Add( i);
				}
			}
			return divs;
		}

		public static List<int> GetProperDivisors(int number)
		{
			List<int> divs = new List<int>();
			divs.Add(1);

			double sqrRtNum = Math.Sqrt(number);
			for (int d = 2; d < sqrRtNum; d++)
			{
				if (number % d == 0)
				{
					divs.Add(d);
					divs.Add(number / d);
				}
			}
			if (MiscFunctions.IsInteger(sqrRtNum))
			{
				if (number>1)
					divs.Add((int)sqrRtNum);
			}
			return divs;
		}

		public static List<decimal> GetProperDivisors(decimal number)
		{
			List<decimal> divs = new List<decimal>();
			divs.Add(1m);

			double sqrRtNum = Math.Sqrt((double)number);
			for (decimal d = 2; d < (decimal)sqrRtNum; d++)
			{
				if (number % d == 0)
				{
					divs.Add(d);
					divs.Add((int)(number / d));
				}
			}
			if (MiscFunctions.IsInteger(sqrRtNum))
			{
				divs.Add((decimal)sqrRtNum);
			}
			return divs;
		}

		public static int GreatestCommonDivisor(int a, int b)
		{

			// brute force
			//int denom = Math.Min(Math.Abs(d), Math.Abs(b));
			//for (; denom > 0; denom--)
			//{
			//    if (d % denom == 0 && b % denom == 0)
			//        break;
			//}
			//return denom;

			// http://en.wikipedia.org/wiki/Euclidean_algorithm

			// recursion proves to be slower than iterative!!!
			//if (b == 0)
			//    return d;
			//else
			//    return GreatestCommonDivisor(b, d % b);

			// http://en.wikipedia.org/wiki/Euclidean_algorithm
			int t = 0;
			while (b != 0)
			{
				t = b;
				b = a % b;
				a = t;
			}
			return a;
		}

		public static long GreatestCommonDivisor(long a, long b)
		{
			// http://en.wikipedia.org/wiki/Euclidean_algorithm
			long t = 0;
			while (b != 0)
			{
				t = b;
				b = a % b;
				a = t;
			}
			return a;
		}

		public static ulong GreatestCommonDivisor(ulong a, ulong b)
		{
			// http://en.wikipedia.org/wiki/Euclidean_algorithm
			ulong t = 0;
			while (b != 0)
			{
				t = b;
				b = a % b;
				a = t;
			}
			return a;
		}

	}
}
