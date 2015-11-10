using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectEuler.Problems
{
	class Problem205
	{
		// 9 x [1,2,3,4]
		//     vs
		// 6 x [1,2,3,4,5,6]

		//Peter has nine four-sided (pyramidal) dice, each with faces numbered 1, 2, 3, 4.
		//Colin has six six-sided (cubic) dice, each with faces numbered 1, 2, 3, 4, 5, 6.

		//Peter and Colin roll their dice and compare totals: the highest total wins. 
		//The result is a draw if the totals are equal.

		//What is the probability that Pyramidal Pete beats Cubic Colin? Give your answer 
		//rounded to seven decimal places in the form 0.abcdefg
		public static Random rnd = new Random();
		public static void Solve()
		{
			Console.WriteLine("Starting 205.");
			long numberOfThrows = long.MaxValue;
			decimal PeterProb = 0;
			decimal wins = 0;
			decimal losses = 0;
			for (long i = 1; i <= numberOfThrows; i++)
			{
				int peter = Roll(9, 4);
				int colin = Roll(6, 6);

				if(peter > colin)
				{
					wins++;
				}
				else if (peter < colin)
				{
					losses++;
				}
				if(i%1000000==0)
				{
					Console.WriteLine("{0,10} : {1:F7}", i, wins/i);
				}
			}
			Console.WriteLine("Probability Pete will win : {0:f7}", wins / numberOfThrows);

		}

		public static int Roll(int numDice, int numSides)
		{
			int result = 0;
			for(int roll = 0; roll<numDice; roll++)
			{
				result += rnd.Next(1, numSides);
			}
			return result;
		}

		public static void SolveStats()
		{
			Console.WriteLine("Starting 205.");
			
			int numCubicRolls = 6;
			int[] cubic = { 1, 2, 3, 4, 5, 6 };
			int[] cubicResults = new int[37];
			Console.WriteLine("Sides: {0} Rolls: {1}", cubic.Length, numCubicRolls);
			CycleEach(numCubicRolls, cubic, ref cubicResults,0);
			PrintSumCount(cubicResults);

			int numPyramRolls = 9;
			int[] pyram = {1,2,3,4};
			int[] pyramResults = new int[37];
			Console.WriteLine("Sides: {0} Rolls: {1}", pyram.Length, numCubicRolls);
			CycleEach(numPyramRolls, pyram, ref pyramResults,0);
			PrintSumCount(pyramResults);


		}

		public static void CycleEach(int numTimes, int[] possibleValues, ref int[] cntUniqueSum, int currentSum)
		{
			int timesLeft = numTimes - 1;
			foreach( int roll in possibleValues)
			{
				int nextSum = currentSum + roll;

				if (timesLeft > 0)
					CycleEach(timesLeft, possibleValues, ref cntUniqueSum, nextSum);
				else
					cntUniqueSum[nextSum]++;
			}

		}

		public static void PrintSumCount(int[] sumCount)
		{
			for(int i=0; i<sumCount.Length; i++)
			{
				Console.WriteLine("Rolled a {0,2} {1,2} times.", i, sumCount[i]);
			}
			Console.WriteLine();
		}
	}
}
