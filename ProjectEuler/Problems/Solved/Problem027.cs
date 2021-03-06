using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

using ProjectEuler.Helpers;


namespace ProjectEuler.Problems
{
	class Problem027
	{

		// Euler published the remarkable quadratic formula:

		// n² + n + 41

		// It turns out that the formula will produce 40 primes for the consecutive values n = 0 to 39. 
		// However, when n = 40, 40² + 40 + 41 = 40(40 + 1) + 41 is divisible by 41, and certainly when 
		// n = 41, 41² + 41 + 41 is clearly divisible by 41.

		// Using computers, the incredible formula  n² − 79n + 1601 was discovered, which produces 80 
		// primes for the consecutive values n = 0 to 79. The product of the coefficients, −79 and 1601, 
		// is −126479.

		// Considering quadratics of the form:

		//    n² + an + b, where |d| < 1000 and |b| < 1000

		//    where |n| is the modulus/absolute value of n
		//    e.g. |11| = 11 and |−4| = 4

		// Find the product of the coefficients, d and b, for the quadratic expression that produces the maximum 
		// num1 of primes for consecutive values of n, starting with n = 0.

		//Start of Main 3/3/2008 1:04:07 PM
		//d:-999 b:-999 with d count of 2
		//d:-981 b:-999 with d count of 3
		//d:-955 b:-997 with d count of 5
		//d:-827 b:-997 with d count of 9
		//d:  77 b:-997 with d count of 18
		//d:  73 b:-961 with d count of 20
		//d:  11 b:-253 with d count of 22
		//d:   1 b:-169 with d count of 25
		//d:  -1 b:-109 with d count of 29
		//d:  -3 b:-107 with d count of 30
		//d:  -5 b:-103 with d count of 31
		//d:  -7 b: -97 with d count of 32
		//d:  -9 b: -89 with d count of 33
		//d: -11 b: -79 with d count of 34
		//d: -13 b: -67 with d count of 35
		//d: -15 b: -53 with d count of 36
		//d: -17 b: -37 with d count of 37
		//d: -19 b: -19 with d count of 38
		//d: -21 b:   1 with d count of 39
		//d: -23 b:  23 with d count of 40
		//d:  -1 b:  41 with d count of 41
		//d:  -3 b:  43 with d count of 42
		//d:  -5 b:  47 with d count of 43
		//d:  -7 b:  53 with d count of 44
		//d:  -9 b:  61 with d count of 45
		//d: -11 b:  71 with d count of 46
		//d: -13 b:  83 with d count of 47
		//d: -15 b:  97 with d count of 48
		//d: -17 b: 113 with d count of 49
		//d: -19 b: 131 with d count of 50
		//d: -21 b: 151 with d count of 51
		//d: -23 b: 173 with d count of 52
		//d: -25 b: 197 with d count of 53
		//d: -27 b: 223 with d count of 54
		//d: -29 b: 251 with d count of 55
		//d: -31 b: 281 with d count of 56
		//d: -33 b: 313 with d count of 57
		//d: -35 b: 347 with d count of 58
		//d: -37 b: 383 with d count of 59
		//d: -39 b: 421 with d count of 60
		//d: -41 b: 461 with d count of 61
		//d: -43 b: 503 with d count of 62
		//d: -45 b: 547 with d count of 63
		//d: -47 b: 593 with d count of 64
		//d: -49 b: 641 with d count of 65
		//d: -51 b: 691 with d count of 66
		//d: -53 b: 743 with d count of 67
		//d: -55 b: 797 with d count of 68
		//d: -57 b: 853 with d count of 69
		//d: -59 b: 911 with d count of 70
		//d: -61 b: 971 with d count of 71
		//Final answer is -61*971 = -59231 with d count of 71
		//End of Main (00:00:05.4744692) 3/3/2008 1:04:13 PM


		private static int maxCount = 0;
		private static Point ab = new Point(-1000, -1000);

		private static int absMaxA = 1000;
		private static int absMaxB = 1000;
		public static void Solve()
		{

			for (int b = 1 - absMaxB; b < absMaxB; b++)
			{
				for (int a = 1 - absMaxA; a < absMaxA; a++)
				{
					CountPrimes(a, b);
				}
			}
			Console.WriteLine("Final answer is {0}*{1} = {2} with d count of {3}", ab.X, ab.Y, ab.X * ab.Y, maxCount);
		}

		private static void CountPrimes( int a, int b )
		{
			int n = 0;
			int count = 0;
			
			bool zeroPrime = false;
			if(MyMath.IsPrime(b))
			{
				zeroPrime = true;
				n=1;
				count=1;
			}
			if (zeroPrime)
			{
				bool isPrime;
				do
				{
					double quad = Math.Pow( n, 2 ) + ( a * n ) + b ;

					if (MiscFunctions.IsInteger(quad) && MyMath.IsPrime((int)quad))
					{
						//Console.WriteLine("\t{0}", quad);
						isPrime = true;
						count++;
					}
					else
					{
						isPrime= false;
					}
					n++;
				} while ( isPrime );

			}

			//Console.WriteLine("at d={0,4} b={1,4} ({2,7})\t with d count of {3}", d, b, d*b, n);
			if (count > maxCount)
			{
				maxCount = count;
				ab.X = a;
				ab.Y = b;
				Console.WriteLine( "\ta:{0,4} b:{1,4} withda count of {2}", a,b,count);
			}
		}
	}
}
