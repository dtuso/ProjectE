using System;
using System.Text;

namespace ProjectEuler.Problems
{
	class Problem024
	{
		//A permutation is an ordered arrangement of objects. For example, 3124 is one possible permutation of the 
		//digits 1, 2, 3 and 4. If all of the permutations are listed numerically or alphabetically, we call 
		//it lexicographic order. The lexicographic permutations of 0, 1 and 2 are:

		//012   021   102   120   201   210

		//What is the millionth lexicographic permutation 
		//of the digits 0, 1, 2, 3, 4, 5, 6, 7, 8 and 9?

		//Starting at 2/29/2008 3:12:35 PM
		//Solved: At position 1000000 we have 2783915460
		//End of main (00:00:01.3275465) 2/29/2008 3:12:37 PM

		public static char[] chars;
		public static void Solve(int numDigits, int seqNumToFind, bool showEachStep)
		{
			chars = BuildArray(numDigits);

			int cntr = 1;
			bool foundNext = true;
			while (foundNext && cntr != seqNumToFind)
			{
				if (showEachStep) Console.WriteLine("{0,5}: {1}", cntr, CharArrayToString(chars));
				cntr++;
				foundNext = findNext();
			}
			if (!foundNext) cntr--;
			Console.WriteLine("Solved: At position {0} we have {1}", cntr, CharArrayToString(chars));

		}

		private static char[] BuildArray(int length)
		{
			char[] digits = new char[length];

			for (int i = 0; i < length; i++)
			{
				digits[i] = Char.Parse(i.ToString());
			}
			return digits;
		}

		private static bool findNext()
		{
			int hi = findHighestIndex();
			if (hi < 0)
				return false;
			swapWithSmallestAfterHighest(hi);
			swapLastFewElements(hi);

			return true;
		}

		private static int findHighestIndex()
		{
			// first find the largest index i so that P(i) < P(i + 1).
			int i = chars.Length - 2;
			while ((i >= 0) && (Int32.Parse(chars[i].ToString()) > Int32.Parse(chars[i + 1].ToString())))
			{
				--i;
			}
			return i;
		}

		private static void swapWithSmallestAfterHighest(int hi)
		{
			// P(i) will be swapped with the smallest of the elements after P(i), but not larger than P(i)
			int lo = chars.Length - 1;
			while (Int32.Parse(chars[hi].ToString()) > Int32.Parse(chars[lo].ToString()))
			{
				lo--;
			}
			Swap(hi, lo);
		}

		private static void swapLastFewElements(int hi)
		{
			int k = 0;
			for (int j = hi + 1; j < (chars.Length + hi) / 2 + 1; ++j, ++k)
			{
				Swap(j, (chars.Length - k - 1));
			}
		}

		private static void Swap(int first, int second)
		{
			char pswap = chars[first];
			chars[first] = chars[second];
			chars[second] = pswap;
		}

		private static string CharArrayToString(char[] charArray)
		{
			StringBuilder sb = new StringBuilder();
			foreach (char c in charArray)
			{
				sb.Append(c);
			}
			return sb.ToString();
		}


	}
}
