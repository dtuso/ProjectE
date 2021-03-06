using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using ProjectEuler.Helpers;

namespace ProjectEuler.Problems
{
	class Problem182
	{
				public static void Solve()
		{
			//The RSA encryption is based on the following procedure:

			//Generate two distinct primes p and q.
			//Compute n=pq and φ=(p-1)(q-1).
			//Find an integer e, 1<e<φ, such that gcd(e,φ)=1.
			
			//A message in this system is a number in the interval [0,n-1].
			//A text to be encrypted is then somehow converted to messages (numbers in the interval [0,n-1]).
			//To encrypt the text, for each message, m, c=m^e mod n is calculated.

			//To decrypt the text, the following procedure is needed: calculate d such that ed=1 mod φ,
			//then for each encrypted message, c, calculate m=c^d mod n.
			
			//There exist values of e and m such that me mod n=m.
			//We call messages m for which m^e mod n=m unconcealed messages.

			//An issue when choosing e is that there should not be too many unconcealed messages.
			//For instance, let p=19 and q=37.
			//Then n=19*37=703 and φ=18*36=648.
			//If we choose e=181, then, although gcd(181,648)=1 it turns out that all possible messages
			//m (0≤m≤n-1) are unconcealed when calculating m^e mod n.
			//For any valid choice of e there exist some unconcealed messages.
			//It's important that the number of unconcealed messages is at a minimum.
			
			// Choose p=1009 and q=3643.
			// Find the sum of all values of e, 1<e<φ(1009,3643) and gcd(e,φ)=1, so that the number of
			// unconcealed messages for this value of e is at a minimum.


			//                    Start of Main 4/14/2008 9:15:39 AM
			//n=3675787 and phi=3671136
			//** new lowest unconcealed. e of 3671135 has 9 unconcealed messages for a sum of 3671135
			//e: 3671111 sumE: 7342246  estimated hours: 856.59
			//e: 3671099 sumE: 11013345  estimated hours: 1,102.43
			//e: 3671075 sumE: 14684420  estimated hours: 952.87
			//e: 3671063 sumE: 18355483  estimated hours: 963.86
			//e: 3671051 sumE: 22026534  estimated hours: 923.76
			//e: 3671027 sumE: 25697561  estimated hours: 841.98
			//e: 3671015 sumE: 29368576  estimated hours: 868.03

			Stopwatch sw = new Stopwatch();
			int p = 1009;
			int q = 3643;
			int n = p * q;
			int phi = (p - 1) * (q - 1);
			long sumE = 0;
			int lowestNumUnconcealedMsgs = 100; // found by starting at 1 and working up

			Console.WriteLine("n={0} and phi={1}", n, phi);
			sw.Start();
			//for (int e = phi - 1; e > 2; e -= 2)
			for (int e = 3; e < phi - 1; e += 2)
			{
				if (!IsGcdEqualOne(e, phi))
					continue;

				int unconcealedMsgs = 0;
				for (int testNessage = 0; testNessage < n; testNessage++)
				{
					if (IsUnconcealed( testNessage, e, n))
					{
						unconcealedMsgs++;
						if ( unconcealedMsgs > lowestNumUnconcealedMsgs)
							break;
					}
				}

				if (unconcealedMsgs < lowestNumUnconcealedMsgs)
				{
					sumE = e;
					lowestNumUnconcealedMsgs = unconcealedMsgs;
					Console.WriteLine("** new lowest unconcealed. e of {0} has {1} unconcealed messages for a sum of {2}", e, unconcealedMsgs, sumE);
				}
				else if (unconcealedMsgs == lowestNumUnconcealedMsgs)
				{
					sumE = checked(sumE + e);
					double elapsedSeconds = sw.ElapsedMilliseconds / 1000;
					double deltaE = (phi - e);
					//double estimatedSecondsRemaining = (elapsedSeconds/deltaE)*e;
					double estimatedSecondsRemaining = (elapsedSeconds / e) * deltaE;
					Console.WriteLine("e: {0} sumE: {1}  estimated hours: {2:N2}", e, sumE, estimatedSecondsRemaining/3600);
				}
				else
				{
					//Console.WriteLine("e of {0} has too many unconcealed messages.", e);
				}
			}
		}

		private static int DecryptMessage(int message, int e, int n)
		{
			return ModPow(message, e, n);
		}

		private static bool IsUnconcealed(int message, int e, int n)
		{
			int decoded = DecryptMessage(message, e, n);
			return (decoded == message);
		}


		// http://en.wikipedia.org/wiki/Modular_exponentiation
		public static int ModPow(int bas, int exp, int mod)
		{
			long result = 1;
			long newBase = bas;
			while (exp > 0)
			{
				if ((exp & 1) == 1) result = checked(result * newBase) % mod; // multiply in this bits' contribution while using modulus to keep result small
				exp >>= 1;
				newBase = checked(newBase * newBase) % mod;
			}

			return Int32.Parse(result.ToString());
		}



		private static bool IsGcdEqualOne(long a, long b)
		{
			long r = a % b;
			while (r != 0)
			{
				a = b;
				b = r;
				r = a % b;
			}
			return (b == 1);
		}
		private static bool IsGcdEqualOne(BigInteger a, BigInteger b)
		{
			BigInteger r = a % b;
			while (r != 0)
			{
				a = b;
				b = r;
				r = a % b;
			}
			return (b == 1);
		}
	

	}
}
