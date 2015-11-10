using System;
using System.Collections.Generic;
using System.Text;
using ProjectEuler.Helpers;

namespace ProjectEuler.Problems
{
	class p077
	{

		//It is possible to write ten as the sum of primes in exactly five different ways:

		//7 + 3
		//5 + 5
		//5 + 3 + 2
		//3 + 3 + 2 + 2
		//2 + 2 + 2 + 2 + 2

		//What is the first value which can be written as the sum of primes in over five thousand different ways?

		//Start of Main 1/6/2009 11:07:50 AM
		//Problem 77
		//    10 : 5
		//    11 : 5
		//    12 : 7
		//    13 : 8
		//    14 : 10
		//    15 : 11
		//    16 : 14
		//    17 : 16
		//    18 : 19
		//    19 : 22
		//    20 : 26
		//    21 : 29
		//    22 : 35
		//    23 : 39
		//    24 : 46
		//    25 : 51
		//    26 : 60
		//    27 : 66
		//    28 : 77
		//    29 : 86
		//    30 : 98
		//    31 : 110
		//    32 : 124
		//    33 : 139
		//    34 : 157
		//    35 : 174
		//    36 : 197
		//    37 : 218
		//    38 : 244
		//    39 : 271
		//    40 : 302
		//    41 : 335
		//    42 : 372
		//    43 : 412
		//    44 : 456
		//    45 : 503
		//    46 : 557
		//    47 : 613
		//    48 : 677
		//    49 : 743
		//    50 : 819
		//    51 : 898
		//    52 : 987
		//    53 : 1082
		//    54 : 1186
		//    55 : 1297
		//    56 : 1420
		//    57 : 1551
		//    58 : 1695
		//    59 : 1849
		//    60 : 2018
		//    61 : 2197
		//    62 : 2394
		//    63 : 2604
		//    64 : 2833
		//    65 : 3078
		//    66 : 3344
		//    67 : 3629
		//    68 : 3936
		//    69 : 4267
		//    70 : 4624
		//    71 : 5006
		//End of Main (00:00:00.1784409) 1/6/2009 11:07:50 AM

		private static int NUM_PRIMES_TO_CALC = 5000;
		
		private static Dictionary<int, int> nextPrimeLookup;
		private static int round = 0;
		public static void Solve()
		{
			Console.WriteLine("Problem 77");

			SetUpPrimeArray();
			for (int i = 10; ; i++)
			{
				int numWays = CountWays(i);
				Console.WriteLine("{0,6} : {1}", i, numWays);
				if(numWays>5000) break;
			}
		}

		private static void SetUpPrimeArray()
		{
			nextPrimeLookup = new Dictionary<int, int>();
			int prime = 0;
			int lastPrime = 0;
			for(int i=0; i<NUM_PRIMES_TO_CALC;i++)
			{
				lastPrime = prime;
				MyMath.GetNextPrime(ref prime);
				nextPrimeLookup.Add(lastPrime, prime);
			}
		}

		private static int CountWays(int p)
		{
			int numWays = 0;

			// find max number of 2's that can be added together to know the maximum length of the
			int[] nums = new int[p/2];
			SetAllTo(nums, 0);

			int sum = 0;
			while (nums[0]!=2)
			{
				Increment(nums, p, ref sum);
				if (sum == p)
				{
					//Console.WriteLine("\t <-- Equal {0}", sum);
					numWays++;
				}
				//else
				//{
				//    Console.WriteLine("\t sum: {0}", sum);
				//}

			}

			return numWays;
			
		}

		private static void Increment(int[] nums, int p, ref int sum)
		{
			/* A Sample sequence for 8
					0 0 0 2 
					0 0 0 3
					0 0 0 5
					0 0 0 7  skip 0 0 0 11 because > 8
					0 0 2 2 
					0 0 2 3
					0 0 2 5  skip 0 0 2 7 because > 8
					0 0 3 3  
					0 0 3 5  skip 0 0 3 7 because > 8
					0 2 2 2
					0 2 2 3  skip 0 2 2 5 because > 8
					0 2 3 3  skip 0 2 2 5 because > 8
					2 2 2 2
			*/

			int pos = nums.Length - 1;

			while (true)
			{
				//Console.Write("Round {0,5}: ", round++);

				nums[pos] = nextPrimeLookup[nums[pos]];
				for (int copyPos = pos + 1; copyPos < nums.Length; copyPos++)
					nums[copyPos] = nums[pos];
				sum = Sum(nums);

				//foreach (int i in nums)
				//    Console.Write(" {0}", i);
				if(sum<=p)
					return;

				//if (sum > p)
				//    Console.WriteLine("\tSKIP");

				pos--;
				if(pos<0)
					return;
			}
		}

		public static int Sum(int[] nums)
		{
			int sum = 0;
			foreach (int i in nums)
			{
				sum = checked(sum + i);
			}
			return sum;
		}

		private static void SetAllTo(int[] nums, int num)
		{
			for (int idx = 0; idx < nums.Length; idx++)
			{
				nums[idx] = num;
			}
		}

	}
}
