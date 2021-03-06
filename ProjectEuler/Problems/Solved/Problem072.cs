using System;
using System.Diagnostics;
using ProjectEuler.Helpers;

namespace ProjectEuler.Problems
{
	class Problem072
	{

		//Problem 72

		//Consider the fraction, n/d, where n and d are positive integers. 
		//If n<d and HCF(n,d)=1, it is called a reduced proper fraction.

		//If we list the set of reduced proper fractions for 
		//    d ≤ 8 in ascending order of size, we get:

		//1/8, 1/7, 1/6, 1/5, 1/4, 2/7, 1/3, 3/8, 2/5, 3/7, 1/2, 4/7, 3/5, 
		//5/8, 2/3, 5/7, 3/4, 4/5, 5/6, 6/7, 7/8

		//It can be seen that there are 21 elements in this set.

		//How many elements would be contained in the set of reduced proper 
		//fractions for d ≤ 1,000,000?



		//303963552391
		//End of Main (21:34:38.7624901) 4/1/2008 3:03:29 PM

		public static void Solve(int maxD)
		{

			Console.WriteLine("Solving For {0}", maxD);

			Stopwatch sw = new Stopwatch();
			sw.Start();

			decimal cnt = 0;
			for (int d = maxD; d>1; d--)
			{
				int step = (d%2 == 0) ? 2 : 1;
				for (int n = 1; n < d; n += step)
				{
					if (Divisors.GreatestCommonDivisor(n, d) == 1)
					{
						cnt++;
					}
				}
				if (d % (1+(maxD / 10000)) == 0)
				{
					Console.WriteLine("{0,8} {1} {2}", d, sw.Elapsed, cnt);
					sw.Reset();
					sw.Start();
				}
			}
			Console.WriteLine();
			Console.WriteLine(cnt);
		}




	}
}
