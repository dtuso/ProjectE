using System;
using System.Collections.Generic;
using ProjectEuler.Helpers;

namespace ProjectEuler.Problems
{
	class Problem183
	{
		// Let N be a positive integer and let N be split into k equal parts, 
		// r = N/k, so that N = r + r + ... + r.
		// Let P be the product of these parts, P = r × r × ... × r = r^k.

		// For example, if 11 is split into five equal parts, 11 = 2.2 + 2.2 + 2.2 + 2.2 + 2.2, 
		// then P = 2.2^5 = 51.53632.

		// Let M(N) = Pmax for a given value of N.

		// It turns out that the maximum for N = 11 is found by splitting eleven into four equal parts which 
		// leads to Pmax = (11/4)4; that is, M(11) = 14641/256 = 57.19140625, which is a terminating decimal.

		// However, for N = 8 the maximum is achieved by splitting it into three equal parts, so M(8) = 512/27, which is a non-terminating decimal.

		// Let D(N) = N if M(N) is a non-terminating decimal 
		// and D(N) = -N if M(N) is a terminating decimal.

		// For example, ΣD(N) for 5 ≤ N ≤ 100 is 2438.

		// Find Σ D(N) for 5 ≤ N ≤ 10000.

		static Dictionary<int,BigInteger> kToTheK = new Dictionary<int, BigInteger>();
		
		public static void Solve(int startN, int maxN)
		{
			int sum = 0;
			BigInteger num;
			BigInteger den;
			BigInteger lastNum = 0;
			BigInteger lastDen = 0;
			int decimalsOfPrecision = maxN * 2;

			BigInteger offset = BigInteger.Pow(10, decimalsOfPrecision);
			int lastK_at_Max = 1;
			for (int N = startN; N <= maxN; N++)
			{
				BigInteger maxDiv = 0;
				BigInteger lastRem = 0;
				int KAtMax = 0;
				for (int k = lastK_at_Max; k < maxN; k++)
				{
					BigInteger N_big = N * offset;
					BigInteger k_big = k * offset;
					BigInteger kIntoN = N_big/k_big;
					BigInteger remainer = N_big - (kIntoN*k_big);
					BigInteger divToPower = kIntoN.Pow(k);
					//num = BigInteger.Pow(N, k);
					//if (kToTheK.ContainsKey(k))
					//{
					//    den = kToTheK[k];
					//}
					//else
					//{
					//    den = BigInteger.Pow(k, k);
					//    kToTheK.Add(k, den);
					//}
					//BigInteger div = num/den;
					//BigInteger rem = BigInteger.Remainder(num, den);
					//if (rem.IsZero()) rem = 0;
					//BigInteger div = BigInteger.Pow((N_big/k_big), k);
					//BigInteger rem = N_big % k_big;

					Console.WriteLine("\tN={0,-4} at k={1,-4} div={2,-49} rem={3,-49}", N, k, divToPower, remainer);

					if (divToPower >= maxDiv)
					{
						if (divToPower > maxDiv || (divToPower == maxDiv && remainer > lastRem))
						{
							KAtMax = k;
							maxDiv = divToPower;
							lastRem = remainer;
							lastDen = k_big;
							//lastNum = num;
							//lastDen = den;
						}
					}
					else
					{
						break;
					}
				}
				lastK_at_Max = KAtMax; // gives a good starting point for the k loop to start at for the next N value
				Console.WriteLine("N={0,-4} at k={1,-4} rem={2,-28} den={3,-28}", N, KAtMax, lastRem, lastDen);
				Console.WriteLine();
				//string seq = RepeatingFractional.GetSequence(lastNum, lastDen);
				//Console.WriteLine(seq);
				//if (seq.Length == 0)
				//{
				//    //terminating
				//    sum -= N;
				//}
				//else
				//{
				//    //non-terminating
				//    sum += N;
				//}
				//Console.WriteLine("sum: {0,4}\t{1}", sum, seq);
			}

		}



	}

}
