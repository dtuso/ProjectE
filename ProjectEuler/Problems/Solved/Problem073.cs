using System;
using System.Diagnostics;
using ProjectEuler.Helpers;

namespace ProjectEuler.Problems
{
	class Problem073
	{
		// Consider the fraction, n/d, where n and d are positive integers. If n<d and HCF(n,d)=1, 
		// it is called a reduced proper fraction.

		// If we list the set of reduced proper fractions for d ≤ 8 in ascending order of size, we get:

		//1/8, 1/7, 1/6, 1/5, 1/4, 2/7, 1/3, 3/8, 2/5, 3/7, 1/2, 4/7, 3/5, 5/8, 2/3, 5/7, 3/4, 4/5, 5/6, 6/7, 7/8

		// It can be seen that there are 3 fractions between 1/3 and 1/2.

		// How many fractions lie between 1/3 and 1/2 in the sorted 
		// set of reduced proper fractions for d ≤ 10,000?


		//Start of Main 3/25/2008 4:28:51 PM
		//5077083
		//End of Main (00:00:01.4750081) 3/25/2008 4:28:53 PM



		public static void Solve(int maxD)
		{

			Stopwatch sw = new Stopwatch();
			//List<int> fracs = new List<int>();
			

			int numPossibilities = maxD*maxD + (maxD - 1);
			int[] cnts = new int[numPossibilities];
			double fracHalf = 1 / 2d;
			double fracThird = 1 / 3d;
			sw.Start();
			for (int d = maxD; d > 1; d--)
			{
				int min = 1+ (int) Math.Floor(d / (double)3);
				int max = 1+ (int) Math.Ceiling(d / (double)2);
				for(int n=min;n<=max;n++)
				{
					//int rn = n;
					//int rd = d;
					//MyMath.ReduceFraction(ref rn, ref rd);
					//string frac = String.Format("{0}/{1}",rn,rd);
					//double reducedFrac = n/(double) d;
					//if (reducedFrac < fracHalf && reducedFrac > fracThird)
					//{
						if (Divisors.GreatestCommonDivisor(n, d) == 1)
						{
							int bitd = maxD*d + n;
							cnts[bitd]++;
						}
						//if (!fracs.Contains(bitd))
						//{
						//    //Console.WriteLine("{0} / {1} = {2}", n, d, reducedFrac);
						//    fracs.Add(bitd);
						//}
					//}
				}
				//Console.WriteLine("{0,5} {1}",d,sw.Elapsed);
			}
			int cnt = 0;
			//cnt = fracs.Count;
			foreach (int i in cnts)
			{
				if (i != 0)
					cnt++;
			}

			Console.WriteLine(cnt);
		}
	}
}
