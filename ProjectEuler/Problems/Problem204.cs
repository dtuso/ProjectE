using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using ProjectEuler.Helpers;

namespace ProjectEuler.Problems
{

	//A Hamming number is a positive number which has no prime factor larger than 5.
	//So the first few Hamming numbers are 1, 2, 3, 4, 5, 6, 8, 9, 10, 12, 15.
	//There are 1105 Hamming numbers not exceeding 10^8.

	//We will call a positive number a generalised Hamming number of type n, if it 
	//has no prime factor larger than n.
	//Hence the Hamming numbers are the generalised Hamming numbers of type 5.

	//How many generalised Hamming numbers of type 100 are there which don't exceed 10^9?

	class Problem204
	{
		public static List<long> primesOverTypeNum = new List<long>();

		public static void Solve(long maxNum, int hammingTypeNum)
		{
			int cntHammings = 0;
			PreloadPrimes(maxNum, hammingTypeNum);
			for (int i = 1; i <= maxNum; i++)
			{
				if (IsHammingNumber(i))
					cntHammings++;
				if(i%100000==0)
					Console.WriteLine("{0} {1}", i, DateTime.Now);
			}

			Console.WriteLine("{0}", cntHammings);
			
		}

		private static void PreloadPrimes(long maxNum, int hammingTypeNum)
		{
			Stopwatch sw = new Stopwatch();
			sw.Start();
			long prime = hammingTypeNum;
			do
			{
				MyMath.GetNextPrime(ref prime);
				primesOverTypeNum.Add(prime);
			} while (prime <= maxNum);

			Console.WriteLine("Found All Primes: {0}", sw.Elapsed);
			
			//primesOverTypeNum.Reverse();
			//Console.WriteLine("Reversed: {0}", sw.Elapsed);


		}

		public static bool IsHammingNumber(long num)
		{
			if (primesOverTypeNum.Contains(num))
				return false;
			foreach (long prime in primesOverTypeNum)
			{
				if(prime>num)
					continue;
				if(num%prime==0)
					return false;
			}
			return true;
		}
	}
}
