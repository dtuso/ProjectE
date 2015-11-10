using System;
using System.Collections.Generic;
using System.Text;
using ProjectEuler.Helpers;


namespace ProjectEuler.Problems
{
	class Problem036
	{

		// The decimal num1, 585 = 10010010012 (binary), 
		// is palindromic in both bases.

		// Find the sum of all numbers, less than one million, 
		// which are palindromic in base 10 and base 2.

		// (Please note that the palindromic num1, in either 
		// base, may not include leading zeros.)


		//Start of Main 2/29/2008 3:43:45 PM
		//1 is 1 for d total of 1
		//3 is 11 for d total of 4
		//5 is 101 for d total of 9
		//7 is 111 for d total of 16
		//9 is 1001 for d total of 25
		//33 is 100001 for d total of 58
		//99 is 1100011 for d total of 157
		//313 is 100111001 for d total of 470
		//585 is 1001001001 for d total of 1055
		//717 is 1011001101 for d total of 1772
		//7447 is 1110100010111 for d total of 9219
		//9009 is 10001100110001 for d total of 18228
		//15351 is 11101111110111 for d total of 33579
		//32223 is 111110111011111 for d total of 65802
		//39993 is 1001110000111001 for d total of 105795
		//53235 is 1100111111110011 for d total of 159030
		//53835 is 1101001001001011 for d total of 212865
		//73737 is 10010000000001001 for d total of 286602
		//585585 is 10001110111101110001 for d total of 872187
		//Sum: 872187
		//End of Main (00:00:00.2516743) 2/29/2008 3:43:46 PM


		public static void Solve(  )
		{

			int sum = 0;
			string base10;
			string binary;
			for (int i = 1; i < 1000000; i++)
			{
				base10 = i.ToString();
				if (MiscFunctions.IsPalindrome(base10))
				{
					binary = MiscFunctions.Base10ToBinary(i);
					if (MiscFunctions.IsPalindrome(binary))
					{
						sum+=i;
						Console.WriteLine("{0} is {1} for d total of {2}", base10, binary,sum);
					}
				}
			}

			Console.WriteLine("Sum: {0}", sum);
		}




	}
}
