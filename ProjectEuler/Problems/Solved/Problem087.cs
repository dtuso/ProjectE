using System;
using System.Collections.Generic;
using System.Text;
using ProjectEuler.Helpers;


namespace ProjectEuler.Problems
{
	class Problem087
	{

		// The smallest number expressible as the sum of a prime square, prime cube, and prime 
		// fourth power is 28. In fact, there are exactly four numbers below fifty that can be 
		// expressed in such a way:

		// 28 = 2^2 + 2^3 + 2^4
		// 33 = 3^2 + 2^3 + 2^4
		// 49 = 5^2 + 2^3 + 2^4
		// 47 = 2^2 + 3^3 + 2^4

		// How many numbers below fifty million can be expressed as the sum of a prime square, 
		// prime cube, and prime fourth power?


		//Start of Main 5/9/2008 3:42:26 PM
		//16
		//81
		//625
		//2401
		//14641
		//28561
		//83521
		//130321
		//279841
		//707281
		//923521
		//1874161
		//2825761
		//3418801
		//4879681
		//7890481
		//12117361
		//13845841
		//20151121
		//25411681
		//28398241
		//38950081
		//47458321
		//62742241
		//For 50000000 we found 1097343
		//End of Main (00:00:01.1332300) 5/9/2008 3:42:27 PM


		
		const int MAX = 50 * 1000 * 1000;
		private static int[] cntFound = new int[MAX];
		public static void Solve()
		{
			int prime4 = 0;
			double primeTo4th;
			do
			{
				MyMath.GetNextPrime(ref prime4);
				primeTo4th = Math.Pow(prime4, 4);
				Console.WriteLine(primeTo4th);
				int prime3 = 0;
				double primeTo3rd;
				do
				{
					MyMath.GetNextPrime(ref prime3);
					primeTo3rd = Math.Pow(prime3, 3);
					int prime2 = 0;
					double primeTo2nd;
					do
					{
						MyMath.GetNextPrime(ref prime2);
						primeTo2nd = Math.Pow(prime2, 2);
						if (primeTo4th + primeTo3rd + primeTo2nd < MAX)
						{
							int val = (int) primeTo4th + (int) primeTo3rd + (int) primeTo2nd;
							cntFound[val]++;
							//Console.WriteLine(val);

						}
					} while (primeTo4th + primeTo3rd + primeTo2nd < MAX);
				} while (primeTo4th + primeTo3rd < MAX);
			} while (primeTo4th<MAX);

			int numFound = 0;
			for(int i = 0; i< MAX;i++)
			{
				if (cntFound[i] > 0)
					numFound++;
			}
			Console.WriteLine("For {0} we found {1}", MAX, numFound );
		}
	}
}
