using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectEuler.Problems
{
	class Problem076
	{
		//It is possible to write five as a sum in exactly six different ways:

		// 4 + 1
		// 3 + 2
		// 3 + 1 + 1
		// 2 + 2 + 1
		// 2 + 1 + 1 + 1
		// 1 + 1 + 1 + 1 + 1

		//How many different ways can one hundred be written as a sum of at least two positive integers?
		
		//Start of Main 4/4/2008 2:03:40 PM
		//100 had 190569291 ways to count up.
		//End of Main (00:00:19.4126758) 4/4/2008 2:04:00 PM

		static int ways = 0;
		static int max;

		public static int Solve(int maximum)
		{
			max = maximum;
			for (int first = max - 1; first > 0; first--)
			{
				int[] nums = new int[max];
				nums[0] = first;
				CheckNext(nums, first, 0);
				//Console.WriteLine("{0}, {1}", first, ways);
			}
			//Console.WriteLine("{0} had {1} ways to count up.", max,ways);
			return ways;
		}

		public static void CheckNext(int[] nums, int currentSum, int position)
		{
			for (int nextNum = nums[position]; nextNum > 0; nextNum--)
			{
				int thisSum = currentSum + nextNum;
				nums[position+1] = nextNum;
				if (thisSum == max)
				{
					ways++;
					//foreach (int i in nums)
					//{
					//    if(i!=0) Console.Write("{0,3}+", i);
					//}
					//Console.WriteLine(" = {0}", thisSum);
					continue;
				}
				if (thisSum > max)
					continue;
				CheckNext(nums, thisSum, position + 1);
			}

		}
	}
}
