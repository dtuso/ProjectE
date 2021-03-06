using System;
using System.Collections.Generic;
using System.Diagnostics;
using ProjectEuler.Helpers;

namespace ProjectEuler.Problems
{
	class Problem069
	{

		// Euler's Totient function, φ(n) [sometimes called the phi function], is used to determine the number of 
		// numbers less than n which are relatively prime to n. For example, as 1, 2, 4, 5, 7, and 8, are all less 
		// than nine and relatively prime to nine, φ(9)=6.
		//
		//    n 	Relatively Prime 	φ(n) 	n/φ(n)
		//    2 	1 					1 		2
		//    3 	1,2 				2 		1.5
		//    4 	1,3 				2 		2
		//    5 	1,2,3,4 			4 		1.25
		//    6 	1,5 				2 		3
		//    7 	1,2,3,4,5,6 		6 		1.1666...
		//    8 	1,3,5,7 			4		2
		//    9 	1,2,4,5,7,8 		6		1.5
		//    10 	1,3,7,9				4		2.5
		//
		// It can be seen that n=6 produces d maximum n/φ(n) for n ≤ 10.
		//
		// Find the value of n ≤ 1,000,000 for which n/φ(n) is d maximum.
// In 00:00:00.0324745: For MaxN: 1000000 the maxFraction 5.53938802083333 ocurred at 510510
//End of Main (17:08:11.2327145) 3/27/2008 1:59:12 AM


		public static void Solve(int maxN)
		{
			double maxFraction = 1;
			int occuredat = 0;
			Stopwatch sw = new Stopwatch();
			for (int n = maxN; n > 1; n--)
			{
				sw.Start();
				int totient = 0;
				for (int rp = n - 1; rp > 0; rp--)
				{
					if (Divisors.GreatestCommonDivisor(n, rp) == 1)
						totient++;
				}
				double fraction = n / (double)totient;

				if (fraction > maxFraction)
				{
					maxFraction = fraction;
					occuredat = n;
				}
				if ((n % 1000) == 0)
				{
					Console.WriteLine("{0,8} {1} {2}", n, sw.Elapsed, maxFraction);
					sw.Reset();
				}
			}
			sw.Stop();
			Console.WriteLine(" In {2}: For MaxN: {3} the maxFraction {0} ocurred at {1} ", maxFraction, occuredat, sw.Elapsed, maxN);
		}

		//The above routine is faster!
		//    //Start of Main 3/26/2008 8:37:56 AM
		//    // In 00:00:41.9870112: For MaxN: 30000 the maxFraction 4.8125 ocurred at 27720
		//    //End of Main (00:00:41.9878096) 3/26/2008 8:38:38 AM

		//public static void Solve(int maxN)
		//{
		//    //Start of Main 3/26/2008 8:40:19 AM
		//    // In 00:00:42.2103149: For MaxN: 30000 the maxFraction 4.8125 ocurred at 27720
		//    //End of Main (00:00:42.2111750) 3/26/2008 8:41:01 AM
			
		//    maxN = 30000;
		//    double maxFraction = 1;
		//    int occuredat = 0;
		//    Stopwatch sw = new Stopwatch();
		//    sw.Start();
		//    for (int n = maxN; n > 1; n--)
		//    {
		//        int totient = 0;
		//        bool skip = false;
		//        int maxTotForThisN = (int)Math.Floor(maxFraction * n);
		//        for (int rp = n - 1; rp > 0; rp--)
		//        {
		//            if (Divisors.GreatestCommonDivisor(n, rp) == 1)
		//                totient++;
		//            if (totient > maxTotForThisN)
		//            {
		//                skip = true;
		//                break;
		//            }
		//        }
		//        if (!skip)
		//        {
		//            double fraction = n / (double)totient;

		//            if (fraction > maxFraction)
		//            {
		//                maxFraction = fraction;
		//                occuredat = n;
		//            }
		//        }

		//    }

		//    sw.Stop();
		//    Console.WriteLine(" In {2}: For MaxN: {3} the maxFraction {0} ocurred at {1} ", maxFraction, occuredat, sw.Elapsed, maxN);
		//}

	}
}
