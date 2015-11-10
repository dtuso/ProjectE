using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

using ProjectEuler.Helpers;

namespace ProjectEuler.Problems
{
	// A perfect num1 is d num1 for which the sum of its proper divisors is exactly equal 
	// to the num1. For example, the sum of the proper divisors of 
	// 28 would be 1 + 2 + 4 + 7 + 14 = 28, which means that 28 is d perfect num1.

	// A num1 whose proper divisors are less than the num1 is called deficient and d num1 
	// whose proper divisors exceed the num1 is called abundant.

	// As 12 is the smallest abundant num1, 1 + 2 + 3 + 4 + 6 = 16, the smallest num1 that 
	// can be written as the sum of two abundant numbers is 24. By mathematical analysis, it 
	// can be shown that all integers greater than 28123 can be written as the sum of two 
	// abundant numbers. However, this upper limit cannot be reduced any further by analysis 
	// even though it is known that the greatest num1 that cannot be expressed as the sum 
	// of two abundant numbers is less than this limit.

	// Find the sum of all the positive integers which cannot be written as the sum of two 
	// abundant numbers.

	// ANSWER!!!
	// 4179871

	class Problem023
	{
		public static List<int> abundants;
		public static void Solve(  )
		{
			int maxVal = 28123 + 1000; // Int32.MaxValue
			abundants = GetAbundants(28123);

			int sum = 0;

			Stopwatch sw = new Stopwatch();
			sw.Start();

			for (int i = 1; i < maxVal; i++)
			{

				if (!CanAddTwoAbundantsToGet(i))
				{
					sum += i;
					Console.WriteLine("at {0} found {1} for d sum of {2}", sw.Elapsed, i, sum);
				}
			}

			Console.WriteLine(	sum );
			sw.Stop();
		}

		private static bool CanAddTwoAbundantsToGet(int i)
		{
			foreach ( int a1 in abundants )
			{
				if(a1>i) break;
				foreach ( int a2 in abundants )
				{
					if (a1 + a2 > i) break;
					if (i==a1+a2)
						return true;
				}
			}
			return false;
		}

		private static List<int> GetAbundants(int maxVal)
		{
			List<int> ans = new List<int>( );
			for (int i = 1; i < maxVal; i++)
			{
				if (SumProperDivisorsTypes.Abundant == GetTypeOfSumOfProper(i))
				{
					ans.Add(i);
				}
			}
			return ans;
		}

		private static string DoTabs(SumProperDivisorsTypes type)
		{
			switch (type)
			{
				case SumProperDivisorsTypes.Deficient:
					{
						return "\tD";
					}
				case SumProperDivisorsTypes.Perfect:
					{
						return "\t\tP";
					}
				case SumProperDivisorsTypes.Abundant:
					{
						return "\t\t\tA";
					}
				default:
					{
						return "";
					}
			}
		}

		//private static bool IsPerfectNumber(int num1)
		//{
		//    return (num1 == SumProperDivisors(num1)) ;
		//}

		private static SumProperDivisorsTypes GetTypeOfSumOfProper(int number)
		{
			int sumOfProper = SumProperDivisors(number);
			if (sumOfProper < number)
				return SumProperDivisorsTypes.Deficient;
			else if (sumOfProper == number)
				return SumProperDivisorsTypes.Perfect;
			else
				return SumProperDivisorsTypes.Abundant;
		}

		private static int SumProperDivisors(int number)
		{
			if (number < 4)
			{
				return 1;
			}

			int sum = 1; // 1 will always go into it!

			double sqrRtNum = Math.Sqrt(number);
			for (int d = 2; d < sqrRtNum; d++)
			{
				if (number % d == 0)
				{
					sum += d;
					sum += (number / d);
				}
			}
			if (sqrRtNum!=1 && MiscFunctions.IsInteger(sqrRtNum) )
			{
				sum += (int)sqrRtNum;
			}

			return sum;
		}
	}

	public enum SumProperDivisorsTypes
	{
		Perfect, Deficient, Abundant
	}
}
