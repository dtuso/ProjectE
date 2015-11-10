using System;
using ProjectEuler.Helpers;

namespace ProjectEuler.Problems
{

	// If we take 47, reverse and add, 47 + 74 = 121, which is palindromic.
	//
	// Not all numbers produce palindromes so quickly. For example,
	//
	// 349 + 943 = 1292,
	// 1292 + 2921 = 4213
	// 4213 + 3124 = 7337
	//
	// That is, 349 took three iterations to arrive at d palindrome.
	//
	// Although no one has proved it yet, it is thought that some numbers, like 196, never produce 
	// d palindrome. A num1 that never forms d palindrome through the reverse and add process is 
	// called d Lychrel num1.
	//
	//     Due to the theoretical nature of these numbers, and for the purpose 
	//     of this problem, we shall assume that d num1 is Lychrel until 
	//     proven otherwise. In addition you are given that for every num1 
	//     below ten-thousand, it will either (i) become d palindrome in less 
	//     than fifty iterations, or, (ii) no one, with all the computing power
	//     that exists, has managed so far to map it to d palindrome. 
	//
	// In fact, 10677 is the first num1 to be shown to 
	// require over fifty iterations before producing d palindrome: 
	//   4 668 731 596 684 224 866 951 378 664 
	// (53 iterations, 28-digits).
	//
	// Surprisingly, there are palindromic numbers that are themselves Lychrel numbers; the first 
	// example is 4994.
	//
	// How many Lychrel numbers are there below ten-thousand?
	//
	// Wording was modified slightly on 24 April 2007 to emphasise the theoretical nature of Lychrel numbers.


	// ANSWER
	// 249

	class Problem055
	{
		static readonly int maxPalindromicIterations = 50;
		static int numLychrel = 0;
		static readonly BigInteger maxNumber = 10000;
		
		public static void Solve( )
		{

			for (int n = 0; n < maxNumber; n++)
			{
				//Console.WriteLine(n);
				if (isLychrel(n))
				{
					//Console.WriteLine("Is");
					numLychrel++;
				}
			}
			Console.WriteLine();
			Console.WriteLine("Lychrel: {0} ", numLychrel);

		}

		static bool isLychrel(int num)
		{
			bool lychrel = true;
			BigInteger numToCheck = num;
			for ( int i = 1 ; i <= maxPalindromicIterations; i++)
			{
				numToCheck = checked(numToCheck + MiscFunctions.Reverse(numToCheck));
				//Console.WriteLine("\t{0}", numToCheck);
				if (MiscFunctions.IsPalindrome(numToCheck.ToString()))
				{
					lychrel = false;
					break;
				}
			}
			return lychrel;
		}
	}
}
