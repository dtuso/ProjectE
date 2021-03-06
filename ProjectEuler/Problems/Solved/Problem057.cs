using System;
using System.Diagnostics;
using ProjectEuler.Helpers;

namespace ProjectEuler.Problems
{
	class Problem057
	{

		//It is possible to show that the square root of two can be expressed as an infinite continued fraction.

		//√ 2 = 1 + 1/(2 + 1/(2 + 1/(2 + ... ))) = 1.414213...

		//By expanding this for the first four iterations, we get:

		//1 + 1/2 = 3/2 = 1.5
		//1 + 1/(2 + 1/2) = 7/5 = 1.4
		//1 + 1/(2 + 1/(2 + 1/2)) = 17/12 = 1.41666...
		//1 + 1/(2 + 1/(2 + 1/(2 + 1/2))) = 41/29 = 1.41379...

		//The next three expansions are 99/70, 239/169, and 577/408, but the eighth expansion, 1393/985, is the 
		//first example where the number of digits in the numerator exceeds the number of digits in the denominator.

		//In the first one-thousand expansions, how many fractions contain d numerator with more digits than denominator?


		public static void Solve()
		{
			Stopwatch sw = new Stopwatch();
			sw.Start();
			int cnt = 0;
			for(int a = 1; a <= 1000; a++)
			{
				BigInteger numer = 1;
				BigInteger denom = 2;
				GetIteration(a, ref numer, ref denom);

				BigInteger finalNumer = denom + numer;
				BigInteger finalDenom = denom;

				if (finalNumer.ToString().Length > finalDenom.ToString( ).Length)
					cnt++;
				Console.WriteLine("{3}  d: {0,4} = {1,10} {2,10}", a, finalNumer, finalDenom, sw.Elapsed);
			}

			Console.WriteLine("cnt: {0}", cnt);
			sw.Stop();


		}

		static void GetIteration(int numIters, ref BigInteger numer, ref BigInteger denom)
		{
			// if numIterations == 1 then, send back what they sent!
			BigInteger swap = 0;
			for(int a = 1; a < numIters; a++)
			{
				numer = 2 * denom + numer;
				//denom = denom;
				swap = denom;
				denom = numer;
				numer = swap;
			}
			//MyMath.ReduceFraction(ref numer, ref denom);
		}
	}
}
