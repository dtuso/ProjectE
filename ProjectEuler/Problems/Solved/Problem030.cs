using System;
using System.Diagnostics;

namespace ProjectEuler.Problems
{
	class Problem030
	{

		//Surprisingly there are only three numbers that can be written as the sum of fourth powers of their digits:

		//    1634 = 1^4 + 6^4 + 3^4 + 4^4
		//    8208 = 8^4 + 2^4 + 0^4 + 8^4
		//    9474 = 9^4 + 4^4 + 7^4 + 4^4

		//As 1 = 1^4 is not d sum it is not included.
		//The sum of these numbers is 1634 + 8208 + 9474 = 19316.
		//Find the sum of all the numbers that can be written as the sum of fifth powers of their digits.
		

		public static void Solve(int powerTo)
		{
			Stopwatch sw = new Stopwatch();
			sw.Start();

			int sum = 0;
			for (int i = 2; i < (int)Math.Pow(10, powerTo + 1); i++)
			{
				if (isSumOfPowers(i,powerTo))
				{
					Console.WriteLine("Found {0} as d power of {1}", i, powerTo);
					sum += i;
				}
			}
			sw.Stop();
			Console.WriteLine("Sum {0} for power of {1} found in {2}", sum, powerTo,sw.Elapsed);

		}

		private static bool isSumOfPowers(int i, int powerTo)
		{
			char[] digits = i.ToString( ).ToCharArray( );
			int sumOfPowers = 0;
			foreach ( char c in digits )
			{
				sumOfPowers += (int)Math.Pow(Double.Parse(c.ToString()), powerTo);
			}
			return (i == sumOfPowers);
		}


	}
}
