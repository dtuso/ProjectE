using System;
using System.Collections.Generic;
using System.Text;
using ProjectEuler.Helpers;

namespace ProjectEuler.Problems
{
	class Problem051
	{
		//By replacing the 1st digit of *57, it turns out that six of the possible values: 
		//157, 257, 457, 557, 757, and 857, are all prime.

		//By replacing the 3rd and 4th digits of 56**3 with the same digit, this 5-digit number 
		//is the first example having seven primes, yielding the family: 56003, 56113, 56333, 
		//56443, 56663, 56773, and 56993. Consequently 56003, being the first member of this 
		//family, is the smallest prime with this property.

		//Find the smallest prime which, by replacing part of the number (not necessarily 
		//adjacent digits) with the same digit, is part of an eight prime value family.

		public static void Solve(int numPrimesToFind )
		{
			bool found = false;
			//Start of Main 7/30/2008 3:14:47 PM
			//numPrimesToFind=8
			//        Found 8 primes in 121313
			//End of Main (00:00:00.8191638) 7/30/2008 3:14:47 PM

			long basePrime = 10;
			Console.WriteLine();
			Console.WriteLine("numPrimesToFind={0} ", numPrimesToFind);
			do
			{
				MyMath.GetNextPrime( ref basePrime);

				string strPrime = basePrime.ToString();
				for (int originalDigit = 0; originalDigit < 10; originalDigit++)
				{
					int cnt = 1; // include the base prime!

					for (int replacementDigit = 0; replacementDigit < 10; replacementDigit++)
					{
						if ( originalDigit == replacementDigit)
							continue;

						string newNumStr = strPrime.Replace(originalDigit.ToString(), replacementDigit.ToString());
						if(newNumStr==strPrime)
							continue;

						long newInt = Int64.Parse(newNumStr);

						if (newInt >= basePrime && MyMath.IsPrime(newInt))
						{
							//Console.WriteLine(" {0} {1} {2} {3}", basePrime, originalDigit, replacementDigit, newInt);
							cnt++;
						}

					} 

					//Console.WriteLine("\tFound {0} primes.", countFound);
					if (numPrimesToFind == cnt)
					{
						found = true;
						break;
					}

				}

				//Console.WriteLine(" {0} {1} {2} {3}", basePrime, originalDigit, replacementDigit, newInt);

				//int len = basePrime.ToString().Length;
				//for (int numDigitsToReplace = 1; numDigitsToReplace < len; numDigitsToReplace++)
				//{
				//    MaskVariations variations = new MaskVariations(len, numDigitsToReplace);
				//    do
				//    {
				//        int countFound = CountPrimes(basePrime, variations.BoolMask, false);

				//        //Console.WriteLine("\tFound {0} primes.", countFound);
				//        if(numPrimeToFind == countFound)
				//        {
				//            CountPrimes(basePrime, variations.BoolMask, true);
				//            //found = true;
				//            break;
				//        }
				//    } while (variations.GetNext());

				//    if (found) break;

				//}
			} while (!found);

			Console.WriteLine("\tFound {0} primes in {1}", numPrimesToFind, basePrime);

		}

		private static int CountPrimes(int basePrime, bool[] mask, bool display)
		{
			int cnt = 1; // always include the basePrime;

			if (display)
			{
				int i = 0;
				Console.Write(" {0,7} masked ", basePrime);
				foreach (bool bitSet in mask)
				{
					Console.Write(bitSet ? '*' : basePrime.ToString()[i]);
					i++;
				}
				Console.Write(": \t");
			}


			for (int digit = 0; digit < 10; digit++)
			{
				StringBuilder newNum = new StringBuilder();
				for (int i = 0; i < mask.Length; i++)
				{
					if (mask[i])
					{
						newNum.Append(digit);
					}
					else
					{
						newNum.Append(basePrime.ToString()[i]);
					}
				}
				int newInt = Int32.Parse(newNum.ToString());

				//if (display) Console.Write("\tReplacing with {0}'s to get {1} ", digit, newNum);

				if (MyMath.IsPrime(newInt))
				{
					if (newInt > basePrime)  
						cnt++;
					//if (display) Console.WriteLine(" PRIME!!");
					if (display && (newInt >= basePrime)) Console.Write(" {0} ", newInt);
				}
				else
				{
					//if (display) Console.WriteLine("");
				}

			}
			if (display) Console.WriteLine("   {0}", cnt);
			return cnt;
		}

	}
}
