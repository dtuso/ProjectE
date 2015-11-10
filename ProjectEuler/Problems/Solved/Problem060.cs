using System;
using System.Collections.Generic;
using System.Text;
using ProjectEuler.Helpers;


namespace ProjectEuler.Problems
{

	// The primes 3, 7, 109, and 673, are quite remarkable. By taking any two primes and concatenating them in any order the 
	// result will always be prime. For example, taking 7 and 109, both 7109 and 1097 are prime. The sum of these four primes, 
	// 792, represents the lowest sum for a set of four primes with this property.

	// Find the lowest sum for a set of five primes for which any two primes concatenate to produce another prime.


	class Problem060
	{
		public static long numPrimeToGroup = 5;
		public static long maxPrimeToSearchTo = 10000;

		//Start of Main 8/20/2008 4:27:12 PM
		//13      5197    5701    6733    8389
		//Sum: 26033
		//End of Main (00:00:09.2984167) 8/20/2008 4:27:21 PM

		public static void Solve()
		{
			long[] primes = new long[numPrimeToGroup];

			int idx = 0;
			bool doAddNext = true;
			while(idx<numPrimeToGroup)
			{
				long newPrime = (doAddNext) ? AddNextPrime(ref primes, idx) : IncrementThisPrime(ref primes,idx);
				if(newPrime>maxPrimeToSearchTo)
				{
					primes[idx] = 0;
					idx--; // back up one position
					if (idx < 0)
					{
						Console.WriteLine("Nothing could be found with the maxPrime set at this level");
						break;
					}
					doAddNext = false;
					PrintPrimesOnOneLine(primes);
				}
				else
				{
					// we have more primes to fill in
					idx++;
					doAddNext = true;
				}
			}
			PrintPrimesOnOneLine(primes);
			Console.WriteLine("Sum: {0}",SumPrimes(primes));
		}


		private static long AddNextPrime(ref long[] primes, int addIdx)
		{
			long nextPrime = primes[(addIdx == 0) ? 0 : addIdx - 1];
			primes[addIdx] = nextPrime;
			return IncrementThisPrime(ref primes, addIdx);
		}


		private static long IncrementThisPrime(ref long[] primes, int thisIdx)
		{
			long nextPrime = primes[thisIdx];

			bool found = false;
			do
			{
				MyMath.GetNextPrime(ref nextPrime);
				primes[thisIdx] = nextPrime;
				if(nextPrime>maxPrimeToSearchTo)
					break;
				found = ArePrimesPairable(primes);
			} while (!found);
			return nextPrime;
		}

		private static bool ArePrimesPairable(long[] primes)
		{
			bool pairable = true;
			int len = primes.Length;
			for(int outer = 0; outer<len; outer++)
			{
				if (primes[outer] ==0)
					break;
				for (int inner = 0; inner < len; inner++)
				{
					if (primes[inner] == 0)
						break;
					if(outer==inner)
						continue;
					long newPrime = Int64.Parse(primes[outer].ToString() + primes[inner].ToString());
					if(!MyMath.IsPrime(newPrime))
					{
						pairable = false;
						break;
					}
				}
				if(!pairable)
					break;
			}
			return pairable;
		}


		private static long SumPrimes(long[] primes)
		{
			
			long sum = 0;
			foreach (long p in primes)
			{
				sum = checked(sum + p);
			}
			return sum;
		}


		private static void PrintPrimesOnOneLine(long[] primes)
		{
			foreach (long p in primes)
			{
				Console.Write("{0,-7} ",p);
			}
			Console.WriteLine();

		}


	}
}
