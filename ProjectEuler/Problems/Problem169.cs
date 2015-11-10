using System;
using System.Collections.Generic;

namespace ProjectEuler.Problems
{
	class Problem169
	{

		// Define f(0)=1 and f(n) to be the number of different ways n can be expressed 
		// as a sum of integer powers of 2 using each power no more than twice.

		// For example, f(10)=5 since there are five different ways to express 10:

		// 1 + 1 + 8
		// 1 + 1 + 4 + 4
		// 1 + 1 + 2 + 2 + 4
		// 2 + 4 + 4
		// 2 + 8

		// What is f(10^25)?

		/* SEE ALSO PROBLEMS 078 and 076 */
		public static decimal MAX_2;
		public static Dictionary<Decimal, int> TwoPowersOf;
		public static void Solve(decimal MAX_SUM)
		{
			Console.Write("Problem 179 to {0,6} ", MAX_SUM);

			int MAX_POS = GetMaxIdx(MAX_SUM);

			int MAX_TWOTOTHEPOWEROF = GetMaxTwoToThePowerOf(MAX_SUM);

			CreateDictionary(MAX_TWOTOTHEPOWEROF);

			int ways = 0;
			decimal[] nums = new decimal[MAX_POS];
			int idx;

			for (idx = 0; idx < MAX_POS; idx++)
				nums[idx] = 0;

			nums[MAX_POS - 1] = 1;
			nums[MAX_POS - 2] = 1;
			idx = MAX_POS - 1;

			while (nums[0] != 1)
			{
				decimal sum = 0;
				while (true)
				{
					if (nums[idx] == 0)
					{
						nums[idx] = 1;
					}
					else if (nums[idx] == 1 && nums[idx - 1] == 1)
					{
						nums[idx - 1] = 2;
						idx--;
					}
					else
					{
						try
						{
							nums[idx] = checked(nums[idx] * 2);

						}
						catch (Exception)
						{
							idx = 0;
							nums[0] = 1;
						}
					}

					decimal numToDuplicate = nums[idx];
					bool previousIsDuplicate = false;
					if (idx > 0)
					{
						if (nums[idx - 1] == nums[idx])
							previousIsDuplicate = true;
					}
					for (int copyIdx = idx + 1; copyIdx < MAX_POS; copyIdx++)
					{
						if (previousIsDuplicate) numToDuplicate *= 2;
						nums[copyIdx] = numToDuplicate;
						previousIsDuplicate = !previousIsDuplicate;
					}

					sum = 0;
					if (numToDuplicate <= MAX_SUM)
					{
						for (int i = 0; i < MAX_POS; i++)
							sum = checked(sum + nums[i]);

						if (sum <= MAX_SUM)
							break;
					}

					//foreach (decimal d in nums)
					//    Console.Write(" {0,8}", d);
					//Console.WriteLine();
					//Console.WriteLine("Too big...");
					idx--;
					if (idx <= 0)
						break;
				}

				//foreach (decimal d in nums)
				//    Console.Write(" {0,8}", d);
				if(sum != MAX_SUM)
				{
					//Console.WriteLine();
					//Console.WriteLine("{0}", sum);
					idx = MAX_POS - 1;
				}
				else
				{
					//foreach (decimal d in nums)
					//    Console.Write(" {0,8}", d);
					//Console.WriteLine();
					//Console.WriteLine("!!! Found !!!");
					ways++;
					idx--;
					if (idx <= 0)
						break;
				}

			}

			Console.WriteLine(" had {0,7} ways.", ways);
		}

		private static void CreateDictionary(int MAX_TWOPOWER)
		{
			TwoPowersOf = new Dictionary<decimal, int>(MAX_TWOPOWER + 1);
			decimal num = 1;
			
			for(int n = 0; n<= MAX_TWOPOWER; n++)
			{
				TwoPowersOf.Add(num, n);
				num *= 2;
			}
		}

		private static int GetMaxTwoToThePowerOf(decimal max_num)
		{
			int pwr = 0;
			decimal num = 1;
			while (num < max_num)
			{
				pwr++;
				num *= 2;
			}
			return pwr;

		}
		private static int GetMaxIdx(decimal num)
		{
			int maxIdx = 0;
			bool lastWasDup = false;
			decimal thisSum = 0;
			decimal thisDigit = 1;
			while (thisSum < num)
			{
				maxIdx++;
				thisSum = checked(thisSum + thisDigit);
				if (lastWasDup)
				{
					thisDigit *= 2;
				}
				lastWasDup = !lastWasDup;
			}
			maxIdx += maxIdx%2; // bump up to the next even number
			return maxIdx;
		}

		private static bool HasNoMoreThanTwoOfEach(decimal[] nums, int max_twoToThePowerOf)
		{
			int[] usedPowersOf = new int[max_twoToThePowerOf];
			foreach (decimal num in nums)
			{
				if(num==0)
					continue;
				int twoToThePowerOf = TwoPowersOf[num];
				if (++usedPowersOf[twoToThePowerOf] > 2) return false;
			}
			return true;
		}

		private static int GetTwoToThePowerOf(decimal num)
		{
			int cntr = 0;
			while(num>1)
			{
				cntr++;
				num /= 2;
			}
			return cntr;
		}
	}
}
