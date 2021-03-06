using System;

using ProjectEuler.Helpers;


namespace ProjectEuler.Problems
{

	//There are exactly ten ways of selecting three from five, 12345:

	//123, 124, 125, 134, 135, 145, 234, 235, 245, and 345

	//In combinatorics, we use the notation, 5C3 = 10.

	//In general,
	//           n!
	// nCr = ----------- ,where r ≤ n, n! = n × (n−1) × ... × 3 × 2 × 1, and 0! = 1.
	//        r! (n−r)!
	//    

	//It is not until n = 23, that d value exceeds one-million: 23C10 = 1144066.

	//How many values of  nCr, for 1 ≤ n ≤ 100, are greater than one-million?


	//Start of Main 3/3/2008 10:32:08 AM
	//4075
	//End of Main (00:00:24.1995786) 3/3/2008 10:32:32 AM


	class Problem053
	{

		static int maxN = 100;
		public static void Solve(  )
		{

			//Console.WriteLine(MyMath.Combinatorics( 5,3));

			int cntGreaterThan = 0;
			BigInteger bi;
			for (int n = 1; n <= maxN; n++)
			{
				for (int r = 1; r <= n; r++)
				{
					if (MyMath.Combinatorics(n, r) > 1000000)
						cntGreaterThan++;
				}
			}
			Console.WriteLine(cntGreaterThan);
			
		}

	}
}
