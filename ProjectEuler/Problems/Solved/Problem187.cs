using System;
using System.Collections.Generic;
using System.Text;
using ProjectEuler.Helpers;

namespace ProjectEuler.Problems
{
	class Problem187
	{

		// A composite is a number containing at least two prime factors. 
		// For example, 15 = 3 × 5; 9 = 3 × 3; 12 = 2 × 2 × 3.

		// There are ten composites below thirty containing precisely two,
		// not necessarily distinct, prime factors: 4, 6, 9, 10, 14, 15, 21, 22, 25, 26.

		// How many composite integers, n < 10^8, have precisely two, 
		// not necessarily distinct, prime factors? 


		//Start of Main 4/8/2008 4:12:12 PM
		//17427258
		//End of Main (00:05:35.2978101) 4/8/2008 4:17:47 PM




		public static void Solve()
		{
			int max = (int) Math.Pow(10, 8);

			//max = 300000;
			bool[] finds = new bool[max+1];
			int prime1=0;
			MyMath.GetNextPrime(ref prime1);

			while(prime1*prime1<=max)
			{
				int prime2 = prime1;
				long product = checked((long) prime1 * prime2);
				while (product < max)
				{
					if (product < max)
					{
						finds[product] = true;
					}
					MyMath.GetNextPrime(ref prime2);
					product = checked(prime1 * prime2);
				}

				MyMath.GetNextPrime(ref prime1);

			}

			int cnt = 0;
			foreach (bool b in finds)
			{
				if (b) cnt++;
			}
			Console.WriteLine(cnt);

		}


	}
}
