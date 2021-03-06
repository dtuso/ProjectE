using System;
using System.Diagnostics;
using ProjectEuler.Helpers;

namespace ProjectEuler.Problems
{
	class Problem092
	{

		// A number chain is created by continuously adding the square of the digits in a 
		// number to form a new number until it has been seen before.

		// For example,

		// 44 → 32 → 13 → 10 → 1 → 1
		// 85 → 89 → 145 → 42 → 20 → 4 → 16 → 37 → 58 → 89

		// Therefore any chain that arrives at 1 or 89 will become stuck in an endless loop. 
		// What is most amazing is that EVERY starting number will eventually arrive at 1 or 89.

		// How many starting numbers below ten million will arrive at 89?


		//Start of Main 3/10/2008 10:05:26 AM
		//In 00:02:05.7303499     Found cnt1: 1418853  cnt89: 8581146
		//End of Main (00:02:05.7310994) 3/10/2008 10:07:32 AM



		public static void Solve()
		{
			Stopwatch sw = new Stopwatch();
			sw.Start();
			int arrivedAt =0;
			int cnt1 = 0;
			int cnt89 = 0;
			for(int n = 1; n<10000000;n++)
			{
				arrivedAt = NumberChain(n);
				if ( arrivedAt == 1 )
				{
					cnt1++;
				}
				if ( arrivedAt == 89 )
				{
					cnt89++;
				}
			}
			sw.Stop();
			Console.WriteLine("In {2} \tFound cnt1: {0}  cnt89: {1} ", cnt1, cnt89, sw.Elapsed);

		}

		private static int NumberChain(int n)
		{
			int x = n;
			while ( x != 1 && x !=89)
			{
				x = SumDigitSquares(x);
			}
			return x;
		}

		private static int SumDigitSquares(int num)
		{
			string s = num.ToString();
			int sum = 0;
			for(int i=0;i<s.Length;i++)
			{
				sum += (int)Math.Pow(Int32.Parse(s[i].ToString( )), 2);
			}
			return sum;
		}
	

	}
}
