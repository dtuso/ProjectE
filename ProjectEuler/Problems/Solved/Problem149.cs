using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectEuler.Problems
{
	class Problem149
	{

		//Looking at the table below, it is easy to verify that the maximum possible sum of adjacent numbers in any 
		//    direction (horizontal, vertical, diagonal or anti-diagonal) is 16 (= 8 + 7 + 1).
		//		−2	+5	+3	+2
		//		+9	−6	+5	+1
		//		+3	+2	+7	+3
		//		−1	+8	−4	+8

		//Now, let us repeat the search, but on a much larger scale:

		//First, generate four million pseudo-random numbers using a specific form of what is known as a 
		//"Lagged Fibonacci Generator":

		//For 1 ≤ k ≤ 55, sk = [100003 − 200003*k + 300007*k^3] (modulo 1000000) − 500000.
		//For 56 ≤ k ≤ 4000000, sk = [s(k−24) + s(k−55) + 1000000] (modulo 1000000) − 500000.

		//Thus, s10 = −393027 and s100 = 86613.

		//The terms of s are then arranged in a 2000×2000 table, using the first 2000 numbers to fill the first row 
		//(sequentially), the next 2000 numbers to fill the second row, and so on.

		//Finally, find the greatest sum of (any number of) adjacent entries in any direction (horizontal, vertical, 
		//diagonal or anti-diagonal).


		//Start of Main 4/25/2008 3:40:42 PM
		//  1: -299993
		//  2: -399947
		//  3: 100183
		//  4: 439
		//  5: 100863
		//  6: 201497
		//  7: 102383
		//  8: -396437
		//  9: -494921
		// 10: -393027
		// 11: -290713
		// 12: -387937
		// 13: 115343
		// 14: 19169
		// 15: 123583
		// 16: 228627
		// 17: 134343
		// 18: -359227
		// 19: -452041
		// 20: -344057
		// 21: -235233
		// 22: -325527
		// 23: 185103
		// 24: 96699
		// 25: 209303
		// 26: 322957
		// 27: 237703
		// 28: -246417
		// 29: -329361
		// 30: -211087
		// 31: -91553
		// 32: -170717
		// 33: 351463
		// 34: 275029
		// 35: 400023
		// 36: -473513
		// 37: 454463
		// 38: -16007
		// 39: -84881
		// 40: 47883
		// 41: 182327
		// 42: 118493
		// 43: -343577
		// 44: -403841
		// 45: -262257
		// 46: -118783
		// 47: -173377
		// 48: 374003
		// 49: 323399
		// 50: 474853
		// 51: -371593
		// 52: -415897
		// 53: 141983
		// 54: 102089
		// 55: 264463
		//s[10]  = -393027
		//s[100] = 86613
		//  1,  1 = 1116200
		//  1,  2 = 20459937
		//  1, 25 = 23744951
		//  1, 32 = 29797027
		//  1, 39 = 31614706
		//  1, 42 = 35126805
		//  1,195 = 39985329
		//  1,223 = 41078532
		//  1,511 = 44540336
		//  2,511 = 44980728
		// 16,511 = 45020794
		// 17,511 = 45415269
		// 19,511 = 45423769
		// 68,281 = 45682881
		// 68,282 = 45750134
		// 68,283 = 45878106
		// 68,284 = 46293334
		// 68,285 = 46481637
		// 68,302 = 46677116
		// 68,303 = 46963863
		// 68,304 = 47001333
		// 68,305 = 47369590
		// 68,306 = 47761704
		// 68,307 = 48127337
		// 68,310 = 48398604
		// 68,322 = 48620910
		//270,  4 = 48881942
		//270,  5 = 48921911
		//270, 17 = 49362480
		//270, 18 = 49373566
		//270, 19 = 49713431
		//270, 40 = 49751313
		//270, 42 = 50111588
		//270, 43 = 50499185
		//270, 44 = 50625781
		//270, 65 = 50803190
		//270, 73 = 50834993
		//270, 76 = 51045978
		//270, 77 = 51462409
		//270, 78 = 51838074
		//270,138 = 51963485
		//270,141 = 52013338
		//270,144 = 52076003
		//270,146 = 52181124
		//270,171 = 52513847
		//270,173 = 52852124
		//52852124
		//End of Main (00:02:03.7837949) 4/25/2008 3:42:45 PM



		private static readonly int SIZE = 2000;
		private static int[] s;

		public static void Solve()
		{
			int maxSize = SIZE*SIZE;
			long maxSum = 0;
			s = new int[maxSize+1];
			for (int k = 1; k <= maxSize; k++)
			{
				Fibonacci(k, ref s);
			}
			Console.WriteLine("s[10]  = {0} ", s[10]);
			Console.WriteLine("s[100] = {0}", s[100]);

			for (int x = 1; x <= SIZE; x++)

			{
				for (int y = 1; y <= SIZE; y++)
				{
					long max = GetMaxValFrom(x, y);
					if(max > maxSum)
					{
						maxSum = max;
						Console.WriteLine("{0,3},{1,3} = {2}",x, y, maxSum);
					}
					//maxSum = (max > maxSum) ? max : maxSum;

				}
			}
			Console.WriteLine(maxSum);
		}

		private static void Fibonacci(int k, ref int[] s)
		{
			if(k<56)
			{
				// s(k) = [100003 − 200003*k + 300007*k^3] (modulo 1000000) − 500000.
				long sum = checked(300007 * (long)Math.Pow(k, 3));
				sum = checked(sum - (200003 * k));
				sum = checked(sum + 100003);
				s[k] = checked((int)(sum % 1000000) - 500000);
				Console.WriteLine("{0,3}: {1}", k, s[k]);
			}
			else
			{
				// s(k) = [s(k−24) + s(k−55) + 1000000] (modulo 1000000) − 500000.
				s[k] = checked((((s[k - 24]) + (s[k - 55]) + (1000000)) % 1000000) - 500000);
				//Console.WriteLine("{0,3}: {1}, {2}, {3}", k, s[k], s[k-24],s[k-55]);
			}
		}
		private static int GetS(int x, int y)
		{
			return s[x + (y - 1)*SIZE];
		}

		private static long GetMaxValFrom(int x, int y)
		{
			long maxSum = 0;

			// check RIGHT			
			int thisX = x;
			int thisY = y;
			int thisSum = 0;
			while(thisX <= SIZE)
			{
				thisSum += GetS(thisX, thisY);
				thisX++;
			}
			maxSum = (thisSum>maxSum) ? thisSum : maxSum;


			// check DOWN
			thisX = x;
			thisY = y;
			thisSum = 0;
			while (thisY <= SIZE)
			{
				thisSum += GetS(thisX, thisY);
				thisY++;
			}
			maxSum = (thisSum > maxSum) ? thisSum : maxSum;


			// check DOWN-RIGHT
			thisX = x;
			thisY = y;
			thisSum = 0;
			while (thisX <= SIZE && thisY <= SIZE)
			{
				thisSum += GetS(thisX, thisY);
				thisX++;
				thisY++;
			}
			maxSum = (thisSum > maxSum) ? thisSum : maxSum;

			// check UP-RIGHT
			thisX = x;
			thisY = y;
			thisSum = 0;
			while (thisX <= SIZE && thisY >= 1)
			{
				thisSum += GetS(thisX, thisY);
				thisX++;
				thisY--;
			}
			maxSum = (thisSum > maxSum) ? thisSum : maxSum;

			return maxSum;


			
		}
	}
}
