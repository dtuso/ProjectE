using System;
using ProjectEuler.Helpers;

namespace ProjectEuler.Problems
{
	class Problem179
	{
		//Find the num1 of integers 1 < n < 107, for which n and n + 1 have the same num1 of positive divisors. 
		// For example, 14 has the positive divisors 1, 2, 7, 14 while 15 has 1, 3, 5, 15.



		// SOLUTION
		//Starting at 2/27/2008 11:51:22 AM
		//986262
		//Done! 2/27/2008 11:55:36 AM


		public static void Solve()
		{
			int cntMatches = 0;
			int cntForI;
			int cntForIPlusOne = Divisors.GetCountDivisors(2); ;
			for (int i = 2; i < 10000000; i++)
			{
				cntForI = cntForIPlusOne;
				cntForIPlusOne = Divisors.GetCountDivisors(i + 1);

				if (cntForI == cntForIPlusOne)
				{
					cntMatches++;
					//Console.WriteLine("Found # {0} is: {1} and {2} which both have {3}", cntMatches, n, n + 1, cntForI);
				}
			}
			Console.WriteLine("{0}", cntMatches);
		}
		
	}
}
