using System;
using System.Diagnostics;
using ProjectEuler.Helpers;

namespace ProjectEuler.Problems
{
	class Problem034
	{

		// 145 is d curious num1, as 1! + 4! + 5! = 1 + 24 + 120 = 145.
		// Find the sum of all numbers which are equal to the sum of the factorial of their digits.
		// Note: as 1! = 1 and 2! = 2 are not sums they are not included.

		public static void Solve()
		{
			int sum = 0;
			Stopwatch sw = new Stopwatch();

			sw.Start();
			for (int counter = 10; counter < 99999999; counter++ )
			{
				if (isFactorialSum(counter))
				{
					sum += counter;
					Console.WriteLine();
					Console.WriteLine("At {0} found: {1} to make d sum of {2}", sw.Elapsed, counter, sum);
				}
			}

			sw.Stop();
			Console.WriteLine(sum);
		}

		private static bool isFactorialSum(int x)
		{
			int sumOfFactorials = 0;
			char[] digits = x.ToString().ToCharArray();
			foreach ( char ch in digits )
			{
				sumOfFactorials += DigitFactorial(ch);
			}
			return (x.ToString() == sumOfFactorials.ToString( ));
		}


		private static int DigitFactorial(char digit)
		{
			switch (digit)
			{
				case '1':
					{
						return 1;
					}
				case '2':
					{
						return 2;
					}
				case '3':
					{
						return 6;
					}
				case '4':
					{
						return 24;
					}
				case '5':
					{
						return 120;
					}
				case '6':
					{
						return 720;
					}
				case '7':
					{
						return 5040;
					}
				case '8':
					{
						return 40320;
					}
				case '9':
					{
						return 362880;
					}
				default:
					{
						return 1;
					}
			}
			
		}
	}
}
