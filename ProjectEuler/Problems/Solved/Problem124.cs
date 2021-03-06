using System;
using System.Collections.Generic;
using System.Text;
using ProjectEuler.Helpers;
namespace ProjectEuler.Problems
{
	class Problem124
	{
		//The radical of n, rad(n), is the product of distinct prime factors of n. For example, 
		//504 = 23 × 32 × 7, so rad(504) = 2 × 3 × 7 = 42.
		//If we calculate rad(n) for 1 ≤ n ≤ 10, then sort them on rad(n), and sorting on n if 
		//the radical values are equal, we get:

		//    Unsorted	 		Sorted
		 
		//    n	rad(n)	 	n	rad(n)	 k
		//    1	1			1	1		1
		//    2	2			2	2		2
		//    3	3			4	2		3
		//    4	2			8	2		4
		//    5	5			3	3		5
		//    6	6			9	3		6
		//    7	7			5	5		7
		//    8	2			6	6		8
		//    9	3			7	7		9
		//    10	10		10	10		10

		//Let E(k) be the kth element in the sorted n column; for example, E(4) = 8 and E(6) = 9.
		//If rad(n) is sorted for 1 ≤ n ≤ 100000, find E(10000).


		//Start of Main 3/12/2008 11:11:02 AM
		//At 10000 rad= 1947 original number=21417
		//End of Main (00:00:57.1377011) 3/12/2008 11:12:00 AM


		public static void Solve()
		{

			int maxVal = 100000;
			int find = 10000;
			//int maxVal = 10;
			int[] n = new int[maxVal];
			int[] r = new int[maxVal];
			for (int i = 1; i <= maxVal; i++)
			{
				n[i-1] = i;
				r[i-1] = GetProductPrimeDivisors(i);
			}
			int swapN;
			int swapR;
			for (int j = 0; j <= maxVal - 2; j++)
			{
				for (int k = j+1; k <= maxVal - 1; k++)
				{
					if (r[k] < r[j])
					{
						swapN = n[k];
						swapR = r[k];
						n[k] = n[j];
						r[k] = r[j];
						n[j] = swapN;
						r[j] = swapR;
					}
					else if (r[k] == r[j] && n[k] < n[j])
					{
						swapN = n[k];
						swapR = r[k];
						n[k] = n[j];
						r[k] = r[j];
						n[j] = swapN;
						r[j] = swapR;
					}
				}
			}

			Console.WriteLine("At {0,5} rad={1,5} original number={2,5}", find, r[find - 1], n[find - 1]);

		}


		public static int GetProductPrimeDivisors(int number)
		{
			List<int> divs = Divisors.GetPrimeDivisors(number);
			int product = 1;
			foreach (int i in divs)
			{
				if (MyMath.IsPrime(i))
				{
					product *= i;
				}
			}
			return product;
		}


	}
}
