using System;
using System.Collections.Generic;
using System.Diagnostics;
using ProjectEuler.Helpers;

namespace ProjectEuler.Problems
{
	class Problem071
	{

		//Consider the fraction, n/d, where n and d are positive integers. 
		//If n<d and HCF(n,d)=1, it is called d reduced proper fraction.

		//If we list the set of reduced proper fractions for d ≤ 8 in ascending order of size, we get:

		//1/8, 1/7, 1/6, 1/5, 1/4, 2/7, 1/3, 3/8, 2/5, 3/7, 1/2, 4/7, 3/5, 5/8, 2/3, 5/7, 3/4, 4/5, 5/6, 6/7, 7/8

		//It can be seen that 2/5 is the fraction immediately to the left of 3/7.

		//By listing the set of reduced proper fractions for d ≤ 1,000,000 in ascending 
		//order of size, find the numerator of the fraction immediately to the left of 3/7.

		//Start of Main 3/10/2008 9:21:16 AM
		//Solving for 8 for the left fraction of 0.4285714285714285714285714286
		//In 00:00:00.0027815: Max Denom=      8: 2/5 = 0.4
		//Solving for 10 for the left fraction of 0.4285714285714285714285714286
		//In 00:00:00.0000156: Max Denom=     10: 2/5 = 0.4
		//Solving for 100 for the left fraction of 0.4285714285714285714285714286
		//In 00:00:00.0001727: Max Denom=    100: 41/96 = 0.4270833333333333333333333333
		//Solving for 1000 for the left fraction of 0.4285714285714285714285714286
		//In 00:00:00.0017614: Max Denom=   1000: 428/999 = 0.4284284284284284284284284284
		//Solving for 1000000 for the left fraction of 0.4285714285714285714285714286
		//In 00:00:01.8514336: Max Denom=1000000: 428570/999997 = 0.4285712857138571415714247143
		//End of Main (00:00:01.8596107) 3/10/2008 9:21:18 AM


		public static void Solve(int size)
		{
			const decimal threeSevenths = 3000000m / 7000000m;
			Console.WriteLine("Solving for {0} for the left fraction of {1}", size, threeSevenths);

			int leftNum = 0;
			int leftDen = 0;
			decimal leftFra = 0;

			Stopwatch sw = new Stopwatch();
			sw.Start();
			
			for (int d = size; d >1; d--)
			{
				//bool foundForThisD = false;
				for (int n = 1 + (int)(threeSevenths*d) ; n > 0; n--)
				{

					if(!MyMath.AreCoprime(d,n))
						continue;
					decimal frac = n / (decimal) d;
					if (frac < threeSevenths)
					{
						if (frac > leftFra)
						{
							int numer = n;
							int denom = d;
							MyMath.ReduceFraction(ref numer, ref denom);
							leftFra = frac;
							leftNum = numer;
							leftDen = denom;
							//foundForThisD = true;
							break;
						}
						//foundForThisD = true;
					}
					if (frac <= leftFra)
					{
						break;
					}
				}

				//if (foundForThisD == false)
				//{
				//    Console.WriteLine("Did not find an 'n' for d={0}", d );
				//}

				//Console.WriteLine("{0} {1,7}",sw.Elapsed,d);
			}

			sw.Stop();
			Console.WriteLine("In {4}: Max Denom={3,7}: {0}/{1} = {2}", leftNum, leftDen, leftFra, size, sw.Elapsed);


		}

	}
}
