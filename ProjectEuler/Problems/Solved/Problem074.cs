using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ProjectEuler.Problems
{
	class Problem074
	{

		//The number 145 is well known for the property that the sum of the factorial of its digits 
		//is equal to 145:

		//1! + 4! + 5! = 1 + 24 + 120 = 145

		//Perhaps less well known is 169, in that it produces the longest chain of numbers that link 
		//back to 169; it turns out that there are only three such loops that exist:

		//169 → 363601 → 1454 → 169
		//871 → 45361 → 871
		//872 → 45362 → 872

		//It is not difficult to prove that EVERY starting number will eventually get stuck in a 
		//loop. For example,

		//69 → 363600 → 1454 → 169 → 363601 (→ 1454)
		//78 → 45360 → 871 → 45361 (→ 871)
		//540 → 145 (→ 145)

		//Starting with 69 produces a chain of five non-repeating terms, but the longest non-repeating 
		//chain with a starting number below one million is sixty terms.

		//How many chains, with a starting number below one million, contain exactly sixty 
		//non-repeating terms?

		//Start of Main 4/8/2008 12:12:53 PM
		//00:00:20.9566401 402
		//End of Main (00:00:20.9582046) 4/8/2008 12:13:14 PM


	
		public static void Solve()
		{
			long max = 1000000;
			int num60NonRepeatingTermsNumbers = 0;

			Stopwatch sw = new Stopwatch();
			sw.Start();

			for (long x = 1; x < max; x++)
			{
				List<long> used = new List<long>();
				long thisSumFac = x;
				while(!used.Contains(thisSumFac) )
				{
					used.Add(thisSumFac);
					thisSumFac = FactorialSum(thisSumFac);
					if (used.Count > 60) break;
				}
				//Console.WriteLine("{0} {1}",x,used.Count);
				if (used.Count==60)
					num60NonRepeatingTermsNumbers++;
			}

			Console.WriteLine("{0} {1}",sw.Elapsed,num60NonRepeatingTermsNumbers);

		}

		// upwards of 6 times slower than the switch case on the char value
		//private static int[] digitFactorials = new int[] { 1, 1, 2, 6, 24, 120, 720, 5040, 40320, 3628800 };
		//private static long FactorialSum(long num)
		//{
		//    long sumOfFactorials = 0;
		//    string str = num.ToString();
		//    for (int i = 0; i < str.Length; i++ )
		//    {
		//        sumOfFactorials += digitFactorials[Int32.Parse(str.Substring(i, 1))];
		//    }
		//    return sumOfFactorials;

		//}
		private static long FactorialSum(long num)
		{
			int sumOfFactorials = 0;
			char[] digits = num.ToString().ToCharArray();
			foreach (char ch in digits)
			{
				switch (ch)
				{
					case '1':
					{
						sumOfFactorials += 1;
						break;
					}
					case '2':
					{
						sumOfFactorials += 2;
						break;
					}
					case '3':
					{
						sumOfFactorials += 6;
						break;
					}
					case '4':
					{
						sumOfFactorials += 24;
						break;
					}
					case '5':
					{
						sumOfFactorials += 120;
						break;
					}
					case '6':
					{
						sumOfFactorials += 720;
						break;
					}
					case '7':
					{
						sumOfFactorials += 5040;
						break;
					}
					case '8':
					{
						sumOfFactorials += 40320;
						break;
					}
					case '9':
					{
						sumOfFactorials += 362880;
						break;
					}
					default:
					{
						sumOfFactorials += 1;
						break;
					}
				}
			}
			return (sumOfFactorials);
		}
	}
}
