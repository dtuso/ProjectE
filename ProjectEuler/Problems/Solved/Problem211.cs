using System;
using System.Collections.Generic;
using System.Text;
using ProjectEuler.Helpers;

namespace ProjectEuler.Problems
{
	class p211
	{
		/*
			For a positive integer n, let σ2(n) be the sum of the 
		 	squares of its divisors. For example,
			
		 		σ2(10) = 1 + 4 + 25 + 100 = 130.

			Find the sum of all n, 0 < n < 64,000,000 such that σ2(n) is a perfect square. 
		 */

		

		public static void Solve()
		{
			long result = 0;
			int maxNum = 64000000;
			for (int num = 1; num < maxNum; num++)
			{
				List<int> divisors = Divisors.GetDivisors(num);

				long sumSqrDivs = GetSumSquares(divisors);

				if (MyMath.IsSquareNumber(sumSqrDivs))
				{
					result = checked(result + num);
					Console.WriteLine("{0,9}\t{1,13}", num, sumSqrDivs);
				}
			}
			Console.WriteLine(result);
		}

		public static long GetSumSquares(List<int> nums)
		{
			long sum = 0;
			foreach (int num in nums)
			{
				sum = checked(sum + (long)Math.Pow(num,2));
			}
			return sum;
		}
	}
}

/*
Start of Main 10/30/2008 11:41:54 AM
1922364685
End of Main (01:20:53.4922626) 10/30/2008 3:19:58 PM
*/