using System;
using System.Collections.Generic;
using System.Text;
using ProjectEuler.Helpers;

namespace ProjectEuler.Problems
{
	class Problem078
	{

		//Let p(n) represent the number of different ways in which n coins can be separated into piles. 
		// For example, five coins can separated into piles in exactly seven different ways, so p(5)=7.
		//OOOOO
		//OOOO   O
		//OOO   OO
		//OOO   O   O
		//OO   OO   O
		//OO   O   O   O
		//O   O   O   O   O

		//Find the least value of n for which p(n) is divisible by one million.
		// 8.13 8.01
		// 6.80 6.80

		// http://mathworld.wolfram.com/PartitionFunctionP.html (see function #11)
		// P(n)=sum_(k=1)^n  (-1)^(k+1) [P(n-1/2k(3k-1))+P(n-1/2k(3k+1))] 


		
		// http://www.lacim.uqam.ca/~plouffe/OEIS/A000041_450k_to_500k.txt
		// p(488324) = 8743102668699029569878781699190264628888276471930143469364522077091560878658801558661016962019064133290955053675259439153926389856837508431381060820940741524744611414116833564102677758780729620211048449596132660797570004915187075299693373976794632987512416327426951610439271448162498664359850688597549778123297494616555286157853387794543346377243812061000969370564394674558846327016858471135935026121404286034645190453196747961885876944284626479594362401712169300418862892741843708556544396640496428603977441029858042986623044123334834018043156475359055345274852680274440086950617824272831370383961688131682459084339127876556305291981616901998724739193852720413343807056863987958181882938028349616437427539149183079255335978573596662462741591753460159194127843174172000000


		public static void Solve(uint max)
		{
			Console.WriteLine("Problem 78 for {0}", max);
			BigInteger[] partitions = new BigInteger[max + 1];
			partitions[1] = 1;
			for (uint k = 2; k <= max; k++)
				partitions[k] = 0;
			Console.WriteLine(Partitions(max, partitions));

		}
		// http://en.wikipedia.org/wiki/Integer_partition

		
		// http://mathworld.wolfram.com/PartitionFunctionP.html (see function #11)
		// P(n)=sum_(k=1)^n  (-1)^(k+1) [P(n-1/2k(3k-1))+P(n-1/2k(3k+1))] 

		//public static BigInteger Partitions(uint n, BigInteger[] partitions)
		//{
		//    if (n==0) return 0;
		//    if (partitions[n] != 0) return partitions[n];
		//    BigInteger sum = 0;
		//    for(uint k= 1; k<=n; k++)
		//    {
		//        uint idx1 = checked(n - k*(3*k - 1)/2);
		//        uint idx2 = checked(n - k*(3*k + 1)/2);

		//        if(k%2==0)
		//        {
		//            sum -= (Partitions(idx1, partitions) + Partitions(idx2, partitions));
		//        }
		//        else
		//        {
		//            sum += (Partitions(idx1, partitions) + Partitions(idx2, partitions));
		//        }
		//    }
		//    partitions[n] = sum;
		//    return sum;
		//}
	}


}

/*
Start of Main 1/26/2009 10:53:15 AM
Problem 78 to 70
     1 : 1
     2 : 2
     3 : 3
     4 : 5
     5 : 7
    70 : 4087968
End of Main (00:00:16.7292485) 1/26/2009 10:53:32 AM
 * 
 		public static void Solve(int max)
		{
			uint MAX_SUM = (uint)max;
			int MAX_IDX = (int)MAX_SUM - 1;

			Console.WriteLine("Problem 78 to {0}", max);
			uint[] counts = new uint[MAX_SUM + 1];
			uint[] nums = new uint[MAX_SUM];
			int idx, pos, copyPos;

			for (idx = 0; idx <= MAX_IDX; idx++)
				nums[idx] = 0;
			bool haveMoreCounting = true;
			while (haveMoreCounting)
			{
				uint sum = 0;
				pos = MAX_IDX;
				while (true)
				{
					nums[pos]++;
					for (copyPos = pos + 1; copyPos <= MAX_IDX; copyPos++)
						nums[copyPos] = nums[pos];

					sum = Sum(nums);

					if (sum <= MAX_SUM)
						break;

					pos--;
					if (pos < 0)
						break;
				}
				counts[sum]++;
				haveMoreCounting = (nums[0] != 1);
			}

			for (int i = 1; i <= MAX_SUM; i++)
			{
				Console.WriteLine("{0,6} : {1}", i, counts[i]);
			}
		}

		public static uint Sum(uint[] nums)
		{
			uint sum = 0;
			foreach (uint i in nums)
			{
				sum = checked(sum + i);
			}
			return sum;
		}
 * 
 * 

		private static int round = 0;
		public static void Solve()
		{
			Console.WriteLine("Problem 78");

			for (int i = 5; ; i++)
			{
				int numWays = CountWays(i);
				Console.WriteLine("{0,6} : {1}", i, numWays);
				if (numWays % 1000000 == 0) break;
			}
		}


		private static int CountWays(int p)
		{
			int numWays = 0;

			// find max number of 2's that can be added together to know the maximum length of the
			int[] nums = new int[p];
			SetAllTo(nums, 0);

			int sum = 0;
			while (nums[0] != 1)
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
			int pos = nums.Length - 1;

			while (true)
			{
				//Console.Write("Round {0,5}: ", round++);

				nums[pos]++;
				for (int copyPos = pos + 1; copyPos < nums.Length; copyPos++)
					nums[copyPos] = nums[pos];
				sum = Sum(nums);

				//foreach (int i in nums)
				//    Console.Write(" {0}", i);
				if (sum <= p)
					return;

				//if (sum > p)
				//    Console.WriteLine("\tSKIP");

				pos--;
				if (pos < 0)
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
 * 
Start of Main 1/6/2009 2:06:08 PM
Problem 78
     5 : 7
     6 : 11
     7 : 15
     8 : 22
     9 : 30
    10 : 42
    11 : 56
    12 : 77
    13 : 101
    14 : 135
    15 : 176
    16 : 231
    17 : 297
    18 : 385
    19 : 490
    20 : 627
    21 : 792
    22 : 1002
    23 : 1255
    24 : 1575
    25 : 1958
    26 : 2436
    27 : 3010
    28 : 3718
    29 : 4565
    30 : 5604
    31 : 6842
    32 : 8349
    33 : 10143
    34 : 12310
    35 : 14883
    36 : 17977
    37 : 21637
    38 : 26015
    39 : 31185
    40 : 37338
    41 : 44583
    42 : 53174
    43 : 63261
    44 : 75175
    45 : 89134
    46 : 105558
    47 : 124754
    48 : 147273
    49 : 173525
    50 : 204226
    51 : 239943
    52 : 281589
    53 : 329931
    54 : 386155
    55 : 451276
    56 : 526823
    57 : 614154
    58 : 715220
    59 : 831820
    60 : 966467
    61 : 1121505
    62 : 1300156
    63 : 1505499
    64 : 1741630
    65 : 2012558
    66 : 2323520
    67 : 2679689
    68 : 3087735
    69 : 3554345
    70 : 4087968
    71 : 4697205
    72 : 5392783
    73 : 6185689
    74 : 7089500
    75 : 8118264
    76 : 9289091
    77 : 10619863
    78 : 12132164
    79 : 13848650
    80 : 15796476
    81 : 18004327
    82 : 20506255
    83 : 23338469
    84 : 26543660
    85 : 30167357
    86 : 34262962
    87 : 38887673
    88 : 44108109
    89 : 49995925
    90 : 56634173
    91 : 64112359
    92 : 72533807
    93 : 82010177

 * 
 Start of Main 1/8/2009 4:01:20 PM
Problem 78
     1 : 1
     2 : 2
     3 : 3
     4 : 5
     5 : 7
     6 : 11
     7 : 15
     8 : 22
     9 : 30
    10 : 42
    11 : 56
    12 : 77
    13 : 101
    14 : 135
    15 : 176
    16 : 231
    17 : 297
    18 : 385
    19 : 490
    20 : 627
    21 : 792
    22 : 1002
    23 : 1255
    24 : 1575
    25 : 1958
    26 : 2436
    27 : 3010
    28 : 3718
    29 : 4565
    30 : 5604
    31 : 6842
    32 : 8349
    33 : 10143
    34 : 12310
    35 : 14883
    36 : 17977
    37 : 21637
    38 : 26015
    39 : 31185
    40 : 37338
    41 : 44583
    42 : 53174
    43 : 63261
    44 : 75175
    45 : 89134
    46 : 105558
    47 : 124754
    48 : 147273
    49 : 173525
    50 : 204226
    51 : 239943
    52 : 281589
    53 : 329931
    54 : 386155
    55 : 451276
    56 : 526823
    57 : 614154
    58 : 715220
    59 : 831820
    60 : 966467
    61 : 1121505
    62 : 1300156
    63 : 1505499
    64 : 1741630
    65 : 2012558
    66 : 2323520
    67 : 2679689
    68 : 3087735
    69 : 3554345
    70 : 4087968
    71 : 4697205
    72 : 5392783
    73 : 6185689
    74 : 7089500
    75 : 8118264
    76 : 9289091
    77 : 10619863
    78 : 12132164
    79 : 13848650
    80 : 15796476
    81 : 18004327
    82 : 20506255
    83 : 23338469
    84 : 26543660
    85 : 30167357
    86 : 34262962
    87 : 38887673
    88 : 44108109
    89 : 49995925
    90 : 56634173
    91 : 64112359
    92 : 72533807
    93 : 82010177
    94 : 92669720
    95 : 104651419
    96 : 118114304
    97 : 133230930
    98 : 150198136
    99 : 169229875
   100 : 190569292
   101 : 214481126
   102 : 241265379
   103 : 271248950
   104 : 304801365
   105 : 342325709
   106 : 384276336
   107 : 431149389
   108 : 483502844
   109 : 541946240
   110 : 607163746
   111 : 679903203
   112 : 761002156
   113 : 851376628
   114 : 952050665
   115 : 1064144451
   116 : 1188908248
   117 : 1327710076
   118 : 1482074143
   119 : 1653668665
   120 : 1844349560
   121 : 2056148051
   122 : 2291320912
   123 : 2552338241
   124 : 2841940500
   125 : 3163127352
   126 : 3519222692
   127 : 3913864295
   128 : 4351078600
   129 : 4835271870
   130 : 5371315400
End of Main (17:29:47.7731376) 1/9/2009 9:31:10 AM

 * 
*/