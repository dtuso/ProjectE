using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using ProjectEuler.Helpers;

namespace ProjectEuler.Problems
{
	class p214
	{
		/*
			Let φ be Euler's totient function, i.e. for a natural number n, φ(n) is the number of k, 1  k  n, for which gcd(k,n) = 1.

			By iterating φ, each positive integer generates a decreasing chain of numbers ending in 1.
			E.g. if we start with 5 the sequence 5,4,2,1 is generated.
			Here is a listing of all chains with length 4:
				5,4,2,1
				7,6,2,1
				8,4,2,1
				9,6,2,1
				10,4,2,1
				12,4,2,1
				14,6,2,1
				18,6,2,1

			Only two of these chains start with a prime, their sum is 12.

			What is the sum of all primes less than 40000000 which generate a chain of length 25?
			*/



		//Start of Main 10/30/2008 4:20:51 PM
		//Starting 214 with maxN=40000000 and len=25.
		//   1 % done...   2 % done...   3 % done...   4 % done...   5 % done...   6 % done...   7 % done...   8 % done...   9 % done...  10 % done...  11 % done...  12 % done...  13 % done...  14 % done...  15 % done
		//...  16 % done...  17 % done...  18 % done...  19 % done...  20 % done...  21 % done...  22 % done...  23 % done...  24 % done...  25 % done...  26 % done...  27 % done...  28 % done...  29 % done...  30 % d
		//one...  31 % done...  32 % done...  33 % done...  34 % done...  35 % done...  36 % done...  37 % done...  38 % done...  39 % done...  40 % done...  41 % done...  42 % done...  43 % done...  44 % done...  45
		//% done...  46 % done...  47 % done...  48 % done...  49 % done...  50 % done...  51 % done...  52 % done...  53 % done...  54 % done...  55 % done...  56 % done...  57 % done...  58 % done...  59 % done...
		//60 % done...  61 % done...  62 % done...  63 % done...  64 % done...  65 % done...  66 % done...  67 % done...  68 % done...  69 % done...  70 % done...  71 % done...  72 % done...  73 % done...  74 % done..
		//.  75 % done...  76 % done...  77 % done...  78 % done...  79 % done...  80 % done...  81 % done...  82 % done...  83 % done...  84 % done...  85 % done...  86 % done...  87 % done...  88 % done...  89 % don
		//e...  90 % done...  91 % done...  92 % done...  93 % done...  94 % done...  95 % done...  96 % done...  97 % done...  98 % done...  99 % done... 100 % done...
		//sumPrimes 1677366278943
		//End of Main (00:06:33.5894824) 10/30/2008 4:27:24 PM

		public static int[] _phi;
		public static int[] _chainLen;
		public static int Phi(int n)
		{
			if (_phi[n] != 0)
				return _phi[n];
			//int totient = 0;
			//for (int rp = n - 1; rp > 0; rp--)
			//	if (Divisors.GreatestCommonDivisor(n, rp) == 1)
			//		totient++;
			//_phi[n] = totient;
			//return totient;
			_phi[n] = MyMath.Phi(n);
			return _phi[n];
		}

		public static void Solve(int maxN, int len)
		{

			Console.WriteLine("Starting 214 with maxN={0} and len={1}.", maxN,len);

			_chainLen = new int[maxN+1];
			_phi = new int[maxN+1];

			int onePct = maxN/100;
			int nextPos = onePct;
			long sumPrimes = 0;
			_chainLen[1] = 1;
			int prime = 0;
			MyMath.GetNextPrime(ref prime);
			while (prime < maxN)
			{
				int lenChain = 0;
				// recurse down to 1
				int chain = prime;
				while (chain > 0)
				{
					if (_chainLen[chain] == 0)
					{
						int nextLowerInChain = Phi(chain);
						//End of Main (00:01:41.1864652) 10/29/2008 1:28:00 PM

						if (_chainLen[nextLowerInChain] != 0)
						{
							_chainLen[chain] = 1 + _chainLen[nextLowerInChain];
						}
						chain = nextLowerInChain;
						lenChain++;
					}
					else
					{
						lenChain += _chainLen[chain];
						break;
					}
					if (lenChain > len)
						break;
				}

				_chainLen[prime] = lenChain;
				//Console.WriteLine("lenChain={0}",lenChain);

				if (lenChain == len)
				{
					sumPrimes += prime;
					//Console.WriteLine("{0,10}: sum: {1}", prime, sumPrimes);
				}

				MyMath.GetNextPrime(ref prime);
				if (prime > nextPos)
				{
					Console.Write(" {0,3} % done...", nextPos / onePct);
					nextPos += onePct;
				}
			}

			Console.WriteLine();
			Console.WriteLine("sumPrimes {0}",sumPrimes);

		}
	}
}

