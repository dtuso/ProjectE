using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectEuler.Problems
{
	class Problem047
	{
		//The first two consecutive numbers to have two distinct prime factors are:

		//14 = 2 × 7
		//15 = 3 × 5

		//The first three consecutive numbers to have three distinct prime factors are:

		//644 = 2² × 7 × 23
		//645 = 3 × 5 × 43
		//646 = 2 × 17 × 19.

		//Find the first four consecutive integers to have four distinct primes factors. 
		//What is the first of these numbers?


		//Starting at 3/5/2008 4:28:25 PM
		//134043
		//End of main (00:00:01.3128267) 3/5/2008 4:28:26 PM


		public static void Solve()
		{
			// Problem047
			int numConsecutive = 4;

			for (int n = 1; ; n++)
			{
				bool found = true;
				for (int cons = 0; cons < numConsecutive; cons++)
				{
					found = found && (GetCountDistinctPrimeDivisors(n + cons) == numConsecutive);

				}

				if (found)
				{

					Console.WriteLine(n);
					//for (int cons = 0; cons < numConsecutive; cons++)
					//{
					//    Console.WriteLine(GetCountDistinctPrimeDivisors(n + cons) );
					//}
					break;
				}
			}
		}

		public static bool IsPrime(int number)
		{
			number = Math.Abs(number);
			if (number <= 1)
				return false;
			if (number <= 3)
				return true;
			double max = Math.Sqrt(number);
			for (double den = 2; den <= max; den++)
			{
				if (IsInteger(number / den))
				{
					return false;
				}
			}
			return true;

		}

		public static void GetNextPrime(ref int number)
		{
			do { } while (!IsPrime(++number));
		}

		public static int GetCountDistinctPrimeDivisors(int number)
		{
			List<int> divs = GetProperDivisors(number);
			List<int> primes = new List<int>();
			foreach (int div in divs)
			{
				if (IsPrime(div) && !primes.Contains(div))
					primes.Add(number);
			}
			return primes.Count;
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
			if (IsInteger(sqrRtNum))
			{
				divs.Add((int)sqrRtNum);
			}
			return divs;
		}

		public static bool IsInteger(double num)
		{
			return (num == (int)num);
		}

	}
}
