using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using ProjectEuler.Helpers;

namespace ProjectEuler.Problems
{
	class Problem070
	{
		//Euler's Totient function, φ(n) [sometimes called the phi function], is used to determine 
		// the number of positive numbers less than or equal to n which are relatively prime to n. 
		// For example, as 1, 2, 4, 5, 7, and 8, are all less than nine and relatively prime to nine, φ(9)=6.
		//The number 1 is considered to be relatively prime to every positive number, so φ(1)=1.

		//Interestingly, φ(87109)=79180, and it can be seen that 87109 is a permutation of 79180.

		//Find the value of n, 1 < n < 10^7, for which φ(n) is a permutation of n and the ratio n/φ(n) 
		//produces a minimum.
		
		//9899802  2999880 3.30006600264011 1.00070905112481
		//9923393  9339392 1.06253094419851 1.00070905112481
		//9933012  3310992 3.00001087287435 1.00070905112481
		//9944230  3929440 2.53069903090517 1.00070905112481
		//9960021  6199200 1.60666231126597 1.00070905112481
		//9966730  3976960 2.50611773817187 1.00070905112481
		//9970632  2799360 3.56175411522634 1.00070905112481
		//9983167  9973816 1.00093755489373 1.00070905112481
		//For MaxN: 10000000 the maxFraction 1.00070905112481 ocurred at 8319823
		//End of Main (00:01:22.9708880) 4/4/2008 1:18:27 PM


		public static void Solve()
		{
			int maxN = 10000000;


			/*************************************************/
			/* Stolen from forum found in problem 69         */
			int[] phi = new int[maxN+1];
			for (int n = 2; n <= maxN; n++)
			{
				phi[n] = n;
			}
			for (int n = 2; n <= maxN; n++)
			{
				if(phi[n] == n )
				{
					for (int k = n; k <= maxN; k += n)
					{
						phi[k] = phi[k] / n * (n-1) ;
					}
				}
			}

			/*************************************************/

			double minFraction = 87109d / 79180; // from the above problem definition
			int occuredat = 0;


			for (int n = 2; n <= maxN; n++)
			{

				if ((n.ToString().Length == phi[n].ToString().Length) && Histogram.NumericHistogram(n) == Histogram.NumericHistogram(phi[n]))
				{
					double fraction = n / (double)phi[n];
					Console.Write("\t\t{0,8} {1,8} {2} {3}", n, phi[n], fraction, minFraction);

					if (fraction < minFraction)
					{
						minFraction = fraction;
						occuredat = n;
						Console.WriteLine("\t min Fraction = {0}", minFraction);
					}
					else
						Console.WriteLine();
				}
			}



			Console.WriteLine(" For MaxN: {2} the maxFraction {0} ocurred at {1} ", minFraction, occuredat, maxN);
		}


	}
}
