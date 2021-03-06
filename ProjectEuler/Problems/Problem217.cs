using System;
using System.Collections.Generic;
using System.Text;
using ProjectEuler.Helpers;

namespace ProjectEuler.Problems
{
	class Problem217
	{
		/*
		
			A positive integer with k (decimal) digits is called balanced if its first ⌈k/2⌉ digits sum to the same value as its last ⌈k/2⌉ digits, where ⌈x⌉, 		
			pronounced ceiling of x, is the smallest integer ≥ x, thus ⌈π⌉ = 4 and ⌈5⌉ = 5.

			So, for example, all palindromes are balanced, as is 13722.

			Let T(n) be the sum of all balanced numbers less than 10^n.
			Thus: T(1) = 45, T(2) = 540 and T(5) = 334795890.

			Find T(47) mod 315

		 */

		public static void Solve(int n)
		{
			BigInteger max = BigInteger.Pow(10, n);
			Console.WriteLine("Starting P217 with n={0}", n);
			BigInteger sumBalanced = 0;
			for (BigInteger bi = 1; bi < max; )
			{
				int how = HowWellBalanced(bi);
				if(how==0)
				{
					sumBalanced += bi;
				}
					
				else if(how>0)
				{
					Math.Log(how);
					
				}
				else
				{
					// how < 0
				}
				bi++;
			}
			Console.WriteLine("T({0}) = {1}", n, sumBalanced);
			Console.WriteLine();

		}

		private static int HowWellBalanced(BigInteger bi)
		{
			char[] digits = bi.ToString().ToCharArray();
			int midIdx = (int)Math.Ceiling(digits.Length / (double)2) - 1;
			int maxIdx = digits.Length - 1;
			int left = 0;
			int right = 0;
			//int idxLeft = midIdx;
			//int idxRight = digits.Length - midIdx - 1;
			for (int i = 0; i <= midIdx; i++)
			{
				left += (int)digits[i];
				right += (int)digits[maxIdx - i];
			}
			return left - right;
		}
		private static bool WellBalanced(BigInteger bi)
		{
			char[] digits = bi.ToString().ToCharArray();
			int midIdx = (int)Math.Ceiling(digits.Length/(double) 2) - 1;
			int maxIdx = digits.Length - 1;
			int left = 0;
			int right = 0;
			//int idxLeft = midIdx;
			//int idxRight = digits.Length - midIdx - 1;
			for (int i = 0; i <= midIdx ;i++ )
			{
				left += (int)digits[i];
				right += (int)digits[maxIdx - i];
			}
			return left==right;
		}
	}
}
