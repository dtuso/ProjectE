using System;
using System.Collections.Generic;
using System.Text;
using ProjectEuler.Helpers;

namespace ProjectEuler.Problems
{
	class Problem050
	{


		//The prime 41, can be written as the sum of six consecutive primes:
		//41 = 2 + 3 + 5 + 7 + 11 + 13

		//This is the longest sum of consecutive primes that adds to a prime below one-hundred.

		//The longest sum of consecutive primes below one-thousand that adds to a prime, contains 
		//21 terms, and is equal to 953.

		//Which prime, below one-million, can be written as the sum of the most consecutive primes?


		// 995989 = starting with     677 for 433 times
		// 996487 = starting with     233 for 497 times
		// 996601 = starting with     101 for 521 times
		// 996617 = starting with    1559 for 343 times
		// 996967 = starting with    1291 for 367 times
		// 996979 = starting with    1543 for 345 times
		// 997081 = starting with    1033 for 393 times
		// 997153 = starting with     787 for 421 times
		// 997333 = starting with      53 for 531 times
		// 997651 = starting with       7 for 543 times
		// 997879 = starting with    1439 for 355 times
		// 998681 = starting with     271 for 491 times
		// 998857 = starting with     167 for 509 times
		// 999133 = starting with    1021 for 395 times
		// 999149 = starting with     571 for 449 times
		// 999721 = starting with     251 for 495 times
		//Prime 997651 had 543 consecutive!
		//End of Main (00:35:52.9214873) 3/19/2008 3:15:30 PM



		public static void Solve()
		{
			int max = 10000000; // one million
			int maxConsecutive = 0;
			int occursAt = 0;


			int[] primes = new int[max];
			int numPrimes = 0;
			int p = 2;
			while (p < max)
			{
				primes[numPrimes++] = p;
				MyMath.GetNextPrime(ref p);
			}

			return;

			int n = 5;
			do
			{
				bool found = false;
				int thisPrime = 3;
				int maxToTestTo = (maxConsecutive == 0) ? n : (n / maxConsecutive)-(maxConsecutive/2);
				while (thisPrime < maxToTestTo && !found)
				{
					// now find a sequence
					int cntPrimes = 0;
					int sumPrimes = 0;
					int seriesPrime = thisPrime;
					while (sumPrimes < n && !found)
					{
						sumPrimes += seriesPrime;
						cntPrimes++;
						found = (sumPrimes == n);
						MyMath.GetNextPrime(ref seriesPrime);
					}

					if (found)
					{
						Console.WriteLine("{0,7} = starting with {1,7} for {2,3} times ", n, thisPrime, cntPrimes);
						if (cntPrimes > maxConsecutive)
						{
							maxConsecutive = cntPrimes;
							occursAt = n;

						}
						maxConsecutive = (maxConsecutive < cntPrimes) ? cntPrimes : maxConsecutive;
					}
					MyMath.GetNextPrime(ref thisPrime);
				}
				MyMath.GetNextPrime(ref n);
			} while (n < max);
			Console.WriteLine("Prime {0} had {1} consecutive!", occursAt, maxConsecutive);
		}


	}
}
