using System;
using System.Collections.Generic;
using ProjectEuler.Helpers;

namespace ProjectEuler.Problems
{
	class p088
	{
		/*
		A natural number, N, that can be written as the sum and product of a given set of 
		at least two natural numbers, {a_(1), a_(2), ... , a_(k)} is called a product-sum number: 
		N = a_(1) + a_(2) + ... + a_(k) = a_(1) × a_(2) × ... × a_(k).

		For example, 6 = 1 + 2 + 3 = 1 × 2 × 3.

		For a given set of size, k, we shall call the smallest N with this property a minimal 
		product-sum number. The minimal product-sum numbers for sets of size, k = 2, 3, 4, 5, 
		and 6 are as follows.

		k=2: 4 = 2 × 2 = 2 + 2
		k=3: 6 = 1 × 2 × 3 = 1 + 2 + 3
		k=4: 8 = 1 × 1 × 2 × 4 = 1 + 1 + 2 + 4
		k=5: 8 = 1 × 1 × 2 × 2 × 2 = 1 + 1 + 2 + 2 + 2
		k=6: 12 = 1 × 1 × 1 × 1 × 2 × 6 = 1 + 1 + 1 + 1 + 2 + 6

		Hence for 2 ≤ k ≤ 6, the sum of all the minimal product-sum numbers is 4 + 6 + 8 + 12 = 30; 
		(8 is only counted once in the sum).

		In fact, as the complete set of minimal product-sum numbers for 2 ≤ k ≤ 12 is 
		{4, 6, 8, 12, 15, 16}, the sum is 61.

		What is the sum of all the minimal product-sum numbers for 2 ≤ k ≤ 12000 ?
 
		 * MAX  6 : 30
		 * MAX 12 : 61

		 * k=12000: 12096  100.80
			Total: 7587457
			End of Main (1.05:09:40.7313419) 1/3/2009 5:55:42 PM

		 */
		public static ulong round = 0;
		public static bool debug;

		public static void Solve(int MAX, bool printUpdates, bool printEachKSolution)
		{
			debug = printUpdates;
			Console.WriteLine("Problem 088 (solving to {0})", MAX);

			//Console.WriteLine(GetSmallest(MAX));
			//return;

			List<ulong> Ns = new List<ulong>();

			for (int k = 2; k <= MAX; k++)
			{
				ulong N = GetSmallest(k);
				if (printEachKSolution) Console.WriteLine("k={0}: {1} \t{2:F2}", k, N, (N/(decimal)k*100));
				AddUnique(Ns, N);
			}


			ulong sumNs = 0;
			int cnt = 0;
			foreach (ulong N in Ns)
			{
				cnt++;
				if (debug) Console.WriteLine("#{0}: {1}", cnt, N);
				sumNs = sumNs + N;
			}
			Console.WriteLine("Total: {0}", sumNs);
		}

		private static ulong GetSmallest(int k)
		{

			if (debug) Console.WriteLine("k={0}", k);
			int[] nums = new int[k];

			SetAllTo(nums, 1);
			round = 0;
			nums[k - 1] = 0;
			ulong N = Int32.MaxValue;
			ulong product = 0, sum = 0;
			bool canContinue;
			do
			{
				canContinue = Increment(nums, ref product, ref sum);
				if (sum == product)
				{
					//Console.WriteLine("\t: {0} \t{1:F2}", N, (N / (decimal)k * 100));
					if(sum<N)
						N = sum;
				}
			} while (canContinue);

			return N;
		}

		private static void SetAllTo(int[] nums, int num)
		{
			for (int idx = 0; idx < nums.Length; idx++)
			{
				nums[idx] = num;
			}
		}

		private static bool Increment(int[] nums, ref ulong product, ref ulong sum)
		{
			/* A Sample sequence
					1 1 1
					1 1 2
					1 1 3
					1 2 2
					1 2 3
					1 3 3
					2 2 2
					2 2 3
					2 3 3
					3 3 3
			*/
			bool lastSkipAll2s = false;
			bool thisSkipAll2s = false;

			int k = nums.Length;
			for (int pos = k - 1; pos >= 0; pos--)
			{
				if (nums[pos] != k)
				{
					nums[pos]++;
					for (int copyPos = pos + 1; copyPos < k; copyPos++)
						nums[copyPos] = nums[pos];


					product = Product(nums);
					sum = Sum(nums);

					if (debug)
					{
						Console.Write("Round {0,5}: ", round++);
						foreach (int i in nums)
						{
							Console.Write(" {0}", i);
						}
						if (sum == product)
							Console.Write("\t<-- Equal {0}", sum);
						else
							Console.Write("\ts: {0,6} p: {1,6}", sum, product);

						if (product <= sum)
							Console.WriteLine();
						else
							Console.WriteLine("\tSKIP");
					}
					if (product <= sum) return true;
					thisSkipAll2s = (nums[pos] == 2);
					if (thisSkipAll2s && lastSkipAll2s)
						return false;
					lastSkipAll2s = thisSkipAll2s;

				}
			}
			return false;
		}

		public static void AddUnique<T>(List<T> nums, T num)
		{
			if (!nums.Contains(num))
			{
				nums.Add(num);
			}
		}

		public static ulong Sum(int[] nums)
		{
			ulong sum = 0;
			foreach (int i in nums)
			{
				sum = checked(sum + (ulong)i);
			}
			return sum;
		}

		public static ulong Product(int[] nums)
		{
			ulong product = 1;
			foreach (int i in nums)
			{
				product = checked(product * (ulong)i);
			}
			return product;
		}

		//public static void Solve(int MAX, bool printUpdates, bool printEachKSolution)
		//{
		//    debug = printUpdates;
		//    Console.WriteLine("Problem 088 (solving to {0})", MAX);

		//    BigInteger test = BigInteger.Pow(2, MAX);
		//    Console.WriteLine(GetSmallest(MAX));
		//    return;

		//    List<BigInteger> Ns = new List<BigInteger>();

		//    for (int k = 2; k <= MAX; k++)
		//    {
		//        BigInteger N = GetSmallest(k);
		//        if (printEachKSolution) Console.WriteLine("k={0}: {1}", k, N);
		//        AddUnique(Ns, N);
		//    }


		//    BigInteger sumNs = 0;
		//    int cnt = 0;
		//    foreach (BigInteger N in Ns)
		//    {
		//        cnt++;
		//        if (debug) Console.WriteLine("#{0}: {1}", cnt, N);
		//        sumNs = sumNs + N;
		//    }
		//    Console.WriteLine("Total: {0}", sumNs);
		//}

		//private static BigInteger GetSmallest(int k)
		//{

		//    if (debug) Console.WriteLine("k={0}", k);
		//    int[] nums = new int[k];

		//    SetAllTo(nums, 1);
		//    round = 0;
		//    nums[k - 1] = 0; 
		//    BigInteger N = Int32.MaxValue;
		//    BigInteger product = 0, sum = 0;
		//    bool canContinue;
		//    do
		//    {
		//        canContinue = Increment(nums, ref product, ref sum);
		//        if(sum==product && sum < N)
		//        {
		//            N = sum;
		//        }
		//    } while (canContinue);

		//    return N;
		//}

		//private static void SetAllTo(int[] nums, int num)
		//{
		//    for (int idx = 0; idx < nums.Length; idx++ )
		//    {
		//        nums[idx] = num;
		//    }
		//}

		//private static bool Increment(int[] nums, ref BigInteger product, ref BigInteger sum)
		//{
		//    /* A Sample sequence
		//            1 1 1
		//            1 1 2
		//            1 1 3
		//            1 2 2
		//            1 2 3
		//            1 3 3
		//            2 2 2
		//            2 2 3
		//            2 3 3
		//            3 3 3
		//    */
		//    bool lastSkipAll2s = false;
		//    bool thisSkipAll2s = false;

		//    int k = nums.Length;
		//    for (int pos = k - 1; pos >= 0; pos--)
		//    {
		//        if (nums[pos] != k)
		//        {
		//            nums[pos]++;
		//            for (int copyPos = pos + 1; copyPos < k; copyPos++)
		//                nums[copyPos] = nums[pos];


		//            product = Product(nums);
		//            sum = Sum(nums);

		//            if (debug)
		//            {
		//                Console.Write("Round {0}: ", round++);
		//                foreach (int i in nums)
		//                {
		//                    Console.Write(" {0}", i);
		//                }
		//                if (sum == product)
		//                    Console.Write("\t<-- Equal {0}", sum);
		//                else 
		//                    Console.Write("\ts: {0,6} p: {1,6}", sum, product);
						
		//                if (product <= sum)
		//                    Console.WriteLine();
		//                else 
		//                    Console.WriteLine("\tSKIP");
		//            }
		//            if (product <= sum) return true;
		//            thisSkipAll2s = (nums[pos] == 2);
		//            if(thisSkipAll2s && lastSkipAll2s)
		//                return false;
		//            lastSkipAll2s = thisSkipAll2s;

		//        }
		//    }
		//    return false;
		//}

		//public static void AddUnique<T>(List<T> nums, T num)
		//{
		//    if(!nums.Contains(num))
		//    {
		//        nums.Add(num);
		//    }
		//}

		//public static BigInteger Sum(int[] nums)
		//{
		//    BigInteger sum = 0;
		//    foreach (int i in nums)
		//    {
		//        sum += i;
		//    }
		//    return sum;
		//}

		//public static BigInteger Product(int[] nums)
		//{
		//    BigInteger product = 1;
		//    foreach (int i in nums)
		//    {
		//        product *= i;
		//    }
		//    return product;
		//}
	}
}
