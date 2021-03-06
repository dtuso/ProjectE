using System;
using System.Diagnostics;
using ProjectEuler.Helpers;

namespace ProjectEuler.Problems
{


	class Problem120
	{
		//Let r be the remainder when (d−1)^n + (d+1)^n is divided by d^2.

		//For example, if d = 7 and n = 3, then r = 42: 63 + 83 = 728 ≡ 42 mod 49. And as n varies, 
		//so too will r, but for d = 7 it turns out that r-max = 42.

		//For 3 ≤ d ≤ 1000, find ∑ r-max.


		public static void Solve()
		{
			GetMaxRemainder_BigInteger(998);
			return;

			int max = 1000;

			Stopwatch sw = new Stopwatch();
			sw.Start();
			int sumOfMax = 0;
			//for (int a = max; a >= 3; a--)
			for (int a = 3; a <= max; a++)
			{
				int thisMax = GetMaxRemainder_BigInteger(a);
				sumOfMax += thisMax;
				//Console.WriteLine("{3}  d: {0,4} = {1,7} {2,10}", d, thisMax, sumOfMax, sw.Elapsed);
			}
			
			Console.WriteLine("sumOfMax: {0}", sumOfMax);
			sw.Stop();
		}

		private static int GetMaxRemainder_BigInteger(int a)
		{
			BigInteger maxR = 0;
			BigInteger maxN = 0;
			int am = a - 1;
			int ap = a + 1;
			int a2 = 10000;
			for (int n = a2 ; n > 0; n--)
			{
				BigInteger num1 = BigInteger.Pow(am, n);
				BigInteger num2 = BigInteger.Pow(ap, n);
				BigInteger div = (num1 / a2) + (num2 / a2);
				BigInteger r = num1 - (a2 * div) + num2; // out of order trying not to overflow

				if (r > maxR)
				{
					maxR = r;
					maxN = n;
				}
				Console.WriteLine("a: {0,4} n: {1,3} r: {2,4}", a, n, r);
			}
			Console.WriteLine("a: {0,4} at n= {1,7} gave r: {2,10}", a, maxN, maxR);
			return Int32.Parse( maxR.ToString( ));
		}

		// this is a bunch of bunk!!!  Doesn't work well!
		//private static int GetMaxRemainder_Double(int a)
		//{
		//    int maxR = 0;
		//    int maxN = 0;
		//    int am = a - 1;
		//    int ap = a + 1;
		//    double a2 = Math.Pow(a, 2);
		//    int ToN = Math.Max(a/10,60);
		//    double nm, np;
		//    double rm, rp, r;
		//    for (int n = ToN; n > 0; n--)
		//    {
		//        nm = Math.Pow(am, n);
		//        if (Double.IsInfinity(nm))
		//        {
		//            throw new Exception("Infinity!!!");
		//        }
		//        np = Math.Pow(ap, n);
		//        if (Double.IsInfinity(np))
		//        {
		//            throw new Exception("Infinity!!!");
		//        }
		//        rm = nm % a2;
		//        rp = np % a2;

		//        r = rm + rp;
				
		//        double rNew = (nm+np) % a2;
		//        if (rNew != r)
		//        {
		//            continue;
		//        }

		//        if (r > maxR)
		//        {
		//            maxR = (int)r;
		//            maxN = n;
		//        }
		//        //Console.WriteLine("d: {0,2} n: {1,3} r: {2,4}", a, n, r);
		//    }
		//    Console.WriteLine("d: {0,4} at n= {1,7} gave r: {2,10}", a, maxN, maxR);
		//    return maxR;
		//}

	}
}

