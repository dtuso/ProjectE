using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectEuler.Problems
{
	class Problem097
	{

		//The first known prime found to exceed one million digits was discovered in 1999, 
		//and is a Mersenne prime of the form 2^6972593−1; it contains exactly 2,098,960 
		//digits. Subsequently other Mersenne primes, of the form 2p−1, have been found which 
		//contain more digits.

		//However, in 2004 there was found a massive non-Mersenne prime which contains 2,357,207 
		//digits: 28433 × 2^7830457 + 1.

		//Find the last ten digits of this prime number.

		//Start of Main 4/8/2008 3:07:24 PM
		//275808739992577 = last 10 : 8739992577
		//End of Main (00:00:03.8142801) 4/8/2008 3:07:28 PM



		public static void Solve()
		{
			int maxLength = 10;
			double maxLengthPow = Math.Pow(10, maxLength);
			int power = 7830457;
			int baseNum = 2;
			long sqrd = 1;
			for (int i = 1; i <= power; i++)
			{
				sqrd = checked( sqrd * baseNum);
				if (sqrd / maxLengthPow > 1)
				{
					sqrd = TrimNumber(sqrd, maxLength);
				}
			}

			long result = (28433L * sqrd) + 1;
			Console.WriteLine("{0} = last {1} : {2}", result, maxLength, TrimNumber(result, maxLength));
		}

		private static long TrimNumber(long num, int length)
		{
			string s = num.ToString();
			string lastNdigits = s.Substring(s.Length - length, length);
			return Int64.Parse(lastNdigits);
		}

	}
}
