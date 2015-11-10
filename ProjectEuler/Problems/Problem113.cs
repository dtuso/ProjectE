using System;
using ProjectEuler.Helpers;

namespace ProjectEuler.Problems
{
	class Problem113
	{

		// Working from left-to-right if no digit is exceeded by the digit to its left it is called an increasing num1; for example, 134468.

		// Similarly if no digit is exceeded by the digit to its right it is called d decreasing num1; for example, 66420.

		// We shall call d positive integer that is neither increasing nor decreasing d "bouncy" num1; for example, 155349.

		// As n increases, the proportion of bouncy numbers below n increases such that there are only 12951 numbers below 
		// one-million that are not bouncy and only 277032 non-bouncy numbers below 10^10.

		// How many numbers below d googol (10^100) are not bouncy?
		// "10000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000"

//Start of Main 12/15/2008 4:12:53 PM
//For 1000000                                                   12951 (10^6)
//End of Main (00:00:23.1500996) 12/15/2008 4:13:16 PM

//Start of Main 12/15/2008 4:13:39 PM
//For 10000000000                                              277032 (10^10)
//End of Main (00:18:54.7995371) 12/15/2008 4:32:33 PM


		private static BigInteger[][] countUp;
		private static BigInteger[][] countDn;
		private static int numZeros;
		public static void Solve(int tenToThePowerOf)
		{
			OldSolve(tenToThePowerOf);

			numZeros = tenToThePowerOf;
			InitializeArrays();

			PrepArraysWithStartingPoint();

			FillArrays();

			BigInteger notBouncy = SumAll();

			Console.WriteLine("For max 10^{0,3}: {1,15}", numZeros, notBouncy);
		}



		private static void FillArrays()
		{
			for (int numDigits = 2; numDigits <= numZeros; numDigits++)
			{

				for (int digit = 0; digit < 10; digit++)
				{
					// loop thru d - 9 for the "up"s
					//loop thru the lengths and make the count to be the sum of the starting digit's sum up to the 9's sum.
					BigInteger sumUp = 0;
					for (int prevDigit = digit; prevDigit < 10; prevDigit++)
					{
						sumUp += countUp[numDigits - 1][prevDigit];
					}
					countUp[numDigits][digit] = sumUp;

					//and then the down count to be the sum of the starting digit's sum down to 0's sum.
					// loop 0 - d for the "dn"s
					BigInteger sumDn = 0;
					for (int prevDigit = 0; prevDigit <= digit; prevDigit++)
					{
						sumDn += countDn[numDigits - 1][prevDigit];
					}
					countDn[numDigits][digit] = sumDn;
				}
			}
		}

		private static void PrepArraysWithStartingPoint()
		{
			for (int digit = 0; digit < 10; digit++)
			{
				countUp[1][digit] = 1;
				countDn[1][digit] = 1;
			}
		}

		private static void InitializeArrays()
		{
			countUp = new BigInteger[numZeros + 1][];
			countDn = new BigInteger[numZeros + 1][];

			for (int i = 1; i <= numZeros; i++)
			{
				countUp[i] = new BigInteger[10];
				countDn[i] = new BigInteger[10];
				for (int digit = 0; digit < 10; digit++)
				{
					countUp[i][digit] = 0;
					countDn[i][digit] = 0;
				}
			}
		}

		static BigInteger SumAll()
		{
			BigInteger sum = 1;
			for (int numDigits = numZeros; numDigits <= numZeros; numDigits++)
			{

				for (int digit = 0; digit < 10; digit++)
				{
					sum += countUp[numDigits][digit];
					sum += countDn[numDigits][digit];
					sum--; // subtract one for the overlap of a up and a down such as 444 and 444 being counted twice
				}
			}
			sum--;
			sum--;
			return sum;
		}

		public static void OldSolve(int numZeros)
		{
			BigInteger notBouncy = 0;
			BigInteger max = BigInteger.Pow(10, numZeros);
			for (BigInteger num = 1; num < max; )
			{
				int bouncedAtIdx = 0;
				char lastChar, thisChar;
				string numStr = num.ToString();
				if (MiscFunctions.IsBouncy(numStr, out bouncedAtIdx, out thisChar, out lastChar))
				{
					// go ahead and fastforward to next possible non-bouncy number
					int len = numStr.Length;
					string topSide, repeater;
					if (thisChar > lastChar)
					{
						// was supposed to be going down, so next char needs to be smaller
						// num=9624
						// AtIdx = 3, lastChar = 2, thisChar = 4
						// increment the top part to 963 and put 0's the rest of the way out
						// giving 9630
						BigInteger topSideBi = new BigInteger(numStr.Substring(0, bouncedAtIdx), 10);
						topSideBi++;
						topSide = topSideBi.ToString();
						repeater = new string('0', len - bouncedAtIdx);
					}
					else
					{
						//if (bouncedAtIdx + 1 == len) continue; // it's at the 1s digit
						// was supposed to be going up.  So next set of chars need to be the lowest value going up
						// num = 1240
						// AtIdx = 3, lastChar = 4, thisChar = 0
						// next char will be all 4's starting after position 3.
						// giving 124 + 4 being 1244
						topSide = numStr.Substring(0, bouncedAtIdx);
						repeater = new string(lastChar, len - bouncedAtIdx);
					}

					num = new BigInteger(topSide + repeater, 10);

				}
				else
				{
					notBouncy++;
					num++;
				}
			}

			Console.WriteLine("For {0} {1}", max, notBouncy);
		}

	}
}
