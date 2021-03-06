using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectEuler.Problems
{
	class Problem150
	{

		//In a triangular array of positive and negative integers, we wish to find a sub-triangle such that the sum of the numbers it contains is the smallest possible.

		//In the example below, it can be easily verified that the marked triangle satisfies this condition having a sum of −42.

		//We wish to make such a triangular array with one thousand rows, so we generate 500500 pseudo-random numbers sk in the range ±219, using a type of random number generator (known as a Linear Congruential Generator) as follows:

		//t := 0
		//for k = 1 up to k = 500500:
		//    t := (615949*t + 797807) modulo 220
		//    sk := t−219

		//Thus: s1 = 273519, s2 = −153582, s3 = 450905 etc

		//Our triangular array is then formed using the pseudo-random numbers thus:
		//s1
		//s2  s3
		//s4  s5  s6 
		//s7  s8  s9  s10
		//...

		//Sub-triangles can start at any element of the array and extend down as far as we like (taking-in the two elements directly below it from the next row, the three elements directly below from the row after that, and so on).
		//The "sum of a sub-triangle" is defined as the sum of all the elements it contains.
		//Find the smallest possible sub-triangle sum.

		//Start of Main 4/25/2008 6:15:15 PM
		//1048576
		//524288
		//      1 -248606100
		//      3 -263987857
		//     36 -271248680
		//-271248680
		//End of Main (00:14:23.3423742) 4/25/2008 6:29:38 PM


		private static int MAX = 500500;
		private static int[] s;
		private static int[] rowNums;
		private static int[] posInRow;


		public static void Solve()
		{
			long minSum = long.MaxValue;
			s = new int[MAX+1];
			rowNums = new int[MAX + 1];
			posInRow = new int[MAX + 1];
			int mod = checked((int)Math.Pow(2, 20));
			int min = checked((int)Math.Pow(2, 19));
			Console.WriteLine(mod);
			Console.WriteLine(min);
			long t = 0;
			int xRow = 1;
			int rowNum = 1;
			for (int k = 1; k < MAX; k++)
			{
				t = checked((615949L*t + 797807) % mod);
				s[k] = checked((int)(t - min));
				rowNums[k] = rowNum;
				posInRow[k] = xRow;
				if (xRow == rowNum)
				{
					xRow = 1;
					rowNum++;
				}
				else
				{
					xRow++;
				}
			}

			for (int top = 1; top < MAX; top++)
			{
				long sum = GetTriangleSum(top);
				if (sum < minSum)
				{
					minSum = sum;
					Console.WriteLine("{0,7} {1}",top,minSum);
				}
			}
			Console.WriteLine(minSum);
		}

		private static long GetTriangleSum(int top)
		{
			long minSum = long.MaxValue;
			int lastRowNum = rowNums[top];


			int xPos = posInRow[top];
			int posRowLeft = xPos;
			int posRowRight = xPos + 1;
			long thisSum = s[top];
			int idx = top;
			while (idx < MAX)
			{
				idx++;
				// make sure we're between the points before adding to the sum
				if (lastRowNum != rowNums[idx] && posInRow[idx] >= posRowLeft && posInRow[idx] <= posRowRight )
				{
					thisSum += s[idx];


					// check for end of row!
					if (posInRow[idx] == posRowRight)
					{
						posRowRight++; // increase the row width!!
						lastRowNum++;
						if (thisSum < minSum)
						{
							minSum = thisSum;
						}		
					}
					
				}


			}
			return minSum;

		}


	}
}
