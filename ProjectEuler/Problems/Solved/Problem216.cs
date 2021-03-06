using System;
using System.Collections.Generic;
using System.Text;
using ProjectEuler.Helpers;

namespace ProjectEuler.Problems
{
	class p216
	{
		/*
			Consider numbers t(n) of the form t(n) = 2n^2-1 with n > 1.
			The first such numbers are 7, 17, 31, 49, 71, 97, 127 and 161.
			It turns out that only 49 = 7*7 and 161 = 7*23 are not prime.
			For n ≤ 10000 there are 2202 numbers t(n) that are prime.

			How many numbers t(n) are prime for n ≤ 50,000,000 ?

SStart of Main 11/14/2008 3:48:11 PM
Solving 216 to 50000000
max: 50000000 Primes: 5437849

		 */

		public static void Solve(int max)
		{
			Console.WriteLine("Solving 216 to {0}", max);
			long cntPrimes = 0;
			for(int n=max; n>1; n-- )
			{
				if(t(n).isProbablePrime())
				{
					cntPrimes++;
				}
				if(n%10000==0)
					Console.Write(" {0}" , n);
			}
			Console.WriteLine();
			Console.WriteLine("max: {0} Primes: {1}", max, cntPrimes);
		}

		public static BigInteger t(int n)
		{
			BigInteger bi = n;
			bi *= n;
			bi *= 2;
			bi--;
			return bi;
		}
	}
}
