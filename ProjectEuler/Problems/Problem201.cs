using System;
using System.Collections.Generic;
using System.Text;
using ProjectEuler.Helpers;

namespace ProjectEuler.Problems
{

	//For any set A of numbers, let sum(A) be the sum of the elements of A.
	//Consider the set B = {1,3,6,8,10,11}.
	//There are 20 subsets of B containing three elements, and their sums are:

	//        sum({1,3,6}) = 10,
	//        sum({1,3,8}) = 12,
	//        sum({1,3,10}) = 14,
	//        sum({1,3,11}) = 15,
	//        sum({1,6,8}) = 15,
	//        sum({1,6,10}) = 17,
	//        sum({1,6,11}) = 18,
	//        sum({1,8,10}) = 19,
	//        sum({1,8,11}) = 20,
	//        sum({1,10,11}) = 22,
	//        sum({3,6,8}) = 17,
	//        sum({3,6,10}) = 19,
	//        sum({3,6,11}) = 20,
	//        sum({3,8,10}) = 21,
	//        sum({3,8,11}) = 22,
	//        sum({3,10,11}) = 24,
	//        sum({6,8,10}) = 24,
	//        sum({6,8,11}) = 25,
	//        sum({6,10,11}) = 27,
	//        sum({8,10,11}) = 29.

	//Some of these sums occur more than once, others are unique.
	//For a set A, let U(A,k) be the set of unique sums of k-element subsets of A, in our example we find 
	//U(B,3) = {10,12,14,18,21,25,27,29} and sum(U(B,3)) = 156.

	//Now consider the 100-element set S = {1^2, 2^2, ... , 100^2}.
	//S has 100,891,344,545,564,193,334,812,497,256 50-element subsets.

	//Determine the sum of all integers which are the sum of exactly one of the 50-element subsets of S, 
	//i.e. find sum(U(S,50)).
	//    Start of Main 8/21/2008 3:18:04 PM
	//10 : 1
	//12 : 1
	//14 : 1
	//15 : 1
	//15 : 2
	//17 : 1
	//18 : 1
	//19 : 1
	//20 : 1
	//22 : 1
	//17 : 2
	//19 : 2
	//20 : 2
	//21 : 1
	//22 : 2
	//24 : 1
	//24 : 2
	//25 : 1
	//27 : 1
	//29 : 1
	//156
	//End of Main (00:00:14.2732935) 8/21/2008 3:18:18 PM



	class Problem201
	{
		private static int[] data;
		private static long[] sumCounts;
		private static int numDigitsToUse;
		private static long maxSum = 0;
		private static bool useExampleNumbers = false;
		public static void Solve()
		{

			LoadData(useExampleNumbers);

			CountThemUp( numDigitsToUse,0,0 ); // start the recursion!

			long sum = FindSumHavingOneSolution();

			Console.WriteLine(sum);

		}



		private static void CountThemUp( int howManyToCount, int currentIdx, long currentSum)
		{
			howManyToCount--;
			for (int idx = currentIdx; idx < (data.Length - howManyToCount); idx++)
			{
				long newSum = currentSum + data[idx];
				if (howManyToCount>0)
				{
					// recurse into next loop
					CountThemUp(howManyToCount, idx + 1, newSum);
				}
				else
				{
					sumCounts[newSum]++;
					//Console.WriteLine("{0} : {1}", newSum, sumCounts[newSum]);
				}
			}
			
			//NumericPermutations np = new NumericPermutations(data.Length);
			//while (!np.EOF)
			//{
			//    int[] lookers = np.Current;
			//    long sum = 0;
			//    for (int i = 0; i < lookers.Length; i++) 
			//    {
			//        if(lookers[i]<numDigitsToUse)
			//        {
			//            sum += data[i];
			//        }
			//    }
			//    // store the value
			//    sumCounts[sum]++;
			//    np.MoveNext();
			//}
		}


		private static long FindSumHavingOneSolution()
		{
			long sum = 0;
			for (int i = 1; i <= maxSum; i++)
			{
				if (sumCounts[i] == 1)
				{
					sum += i;
				}
			}

			return sum;
		}

		private static void LoadData(bool useExample)
		{
			if (useExample)
			{
				numDigitsToUse = 3;
				data = new int[] { 1, 3, 6, 8, 10, 11 };
			}
			else
			{
				numDigitsToUse = 50;
				data = new int[100];

				for (int i = 1; i <= 100; i++)
				{
					data[i - 1] = (int)Math.Pow(i, 2);
				}
			}

			// sum up the highest n values
			// and make this the Lenth-1 of our array of longs
			for (int i = data.Length - 1; i > data.Length - 1 - numDigitsToUse; i--)
			{
				maxSum += data[i];
			}
			sumCounts = new long[maxSum+1];

			return;
		}
	}
}
