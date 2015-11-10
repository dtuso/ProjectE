using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using ProjectEuler.Helpers;
namespace ProjectEuler.Problems
{
	class Problem148
	{

		//We can easily verify that none of the entries in the first seven rows of Pascal's 
		//triangle are divisible by 7:
		//                         1
		//                     1 	  	 1
		//                 1 	  	 2 	  	 1
		//             1 	  	 3 	  	 3 	  	 1
		//         1 	  	 4 	  	 6 	  	 4 	  	 1
		//     1 	  	 5 	  	10 	  	10 	  	 5 	  	 1
		//1 	  	 6 	  	15 	  	20 	  	15 	  	 6 	  	 1

		//However, if we check the first one hundred rows, we will find that only 2361 of 
		//the 5050 entries are not divisible by 7.

		//Find the number of entries which are not divisible by 7 in the first one billion 
		//(10^9) rows of Pascal's triangle.


//Start of Main 12/24/2008 9:56:14 AM
//For 100 rows, numEntriesNotDivBy7: 2361
//End of Main (00:00:01.0630646) 12/24/2008 9:56:15 AM



		public static void Solve(ulong numRows)
		{

			Console.WriteLine(CountNotDivisibleOnPascalRow(numRows/10000));
			return;

			BigInteger numEntriesNotDivBy7 = 3; // don't count row 0 & 1
			for (ulong rowIdx = 2; rowIdx < numRows; rowIdx++)
			{
				numEntriesNotDivBy7 += CountNotDivisibleOnPascalRow(rowIdx);
			}
			Console.WriteLine("For {1} rows, numEntriesNotDivBy7: {0}", numEntriesNotDivBy7, numRows);
		}

		//http://en.wikipedia.org/wiki/Pascal%27s_triangle See Calculating an individual row
		private static ulong CountNotDivisibleOnPascalRow(ulong rowIdx)
		{
			//Console.Write("RowIdx {0,2}\t\t", rowIdx);
			//Console.Write("{0,4} ", 1); 
			
			ulong row = rowIdx + 1;
			ulong notDivisible = 2;
			ulong midPoint = rowIdx/2;
			bool lastNotDiv = false;
			
			// v(c) = (r-c)/c where c=column and r=row. and multiply this to the value on the left
			BigInteger numberToTheLeft = 1; // all pascal rows start with a 1!!
			for (ulong c = 1; c <= midPoint; c++)
			{
				BigInteger thisNumber = numberToTheLeft;
				thisNumber = thisNumber * (row - c);
				thisNumber = thisNumber / c;
				BigInteger mod = thisNumber % 7;
				if (mod > 0)
				{
					lastNotDiv = true;
					notDivisible += 2;
				}
				else
				{
					lastNotDiv = false;
				}
				//Console.Write("{0,4} ", thisNumber);
				numberToTheLeft = thisNumber;
			}

			if (lastNotDiv && rowIdx % 2 == 0)
			{
				notDivisible--; // don't count double for the mid-poing of the even row idxs, because they only have 1 middle number
			}
			//Console.WriteLine();
			return notDivisible;
		}


		public static void Solve_old(ulong numRows)
		{
			/*    Mi  0  1  2  3  4
			 * ---------------------
			 *0:  0   1
			 *1:  0   1  * 
			 *2:  1   1  2  
			 *3:  1   1  3  *
			 *4:  2   1  4  6 
			 *5:  2   1  5 10 **
			 *6:  3   1  6 15 20
			 *7:  3   1  7 21 35 ** 
			*/

			BigInteger[] prevRow;
			BigInteger[] thisRow = new BigInteger[] { 1, 3 };

			long numEntriesNotDivBy7 = 10; // rows 0 and 1 and 2 and 3
			for (ulong rowIdx = 4; rowIdx < numRows; rowIdx++)
			{

				Stopwatch sw = new Stopwatch();
				sw.Start();
				bool isDoubleColumn = (rowIdx % 2 == 1);
				ulong midPointIdx = rowIdx / 2;

				// build the new row
				prevRow = thisRow;
				thisRow = new BigInteger[midPointIdx + 1];
				thisRow[0] = 1;
				for (ulong col = 1; col <= midPointIdx; col++)
				{
					if (col < midPointIdx)
					{
						thisRow[col] = prevRow[col - 1] + prevRow[col];
					}
					else
					{
						if (isDoubleColumn)
						{
							thisRow[col] = prevRow[col - 1] + prevRow[col];
						}
						else
						{
							thisRow[col] = prevRow[col - 1] + prevRow[col - 1];
						}

					}
				}
				sw.Stop();
				Console.WriteLine("{0}a: {1}", rowIdx, sw.Elapsed);

				sw.Reset();
				sw.Start();

				// count not Div by 7's
				numEntriesNotDivBy7 += 2; // account for the one's on each end
				for (ulong col = 1; col <= midPointIdx; col++)
				{
					BigInteger div = thisRow[col] / 7;
					if (div * 7 != thisRow[col])
					{
						numEntriesNotDivBy7 += 2;
						if (col == midPointIdx && !isDoubleColumn)
						{
							// decrease by one if the ending value is not divisible and only occurs once.
							numEntriesNotDivBy7--;
						}
					}
				}

				sw.Stop();
				Console.WriteLine("{0}b: {1}", rowIdx, sw.Elapsed);
			}
			Console.WriteLine("For {1} rows, numEntriesNotDivBy7: {0}", numEntriesNotDivBy7, numRows);
		}

	}
}
