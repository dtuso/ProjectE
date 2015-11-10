using System;
using System.Diagnostics;

namespace ProjectEuler.Problems
{
	class Problem018
	{
		///By starting at the top of the triangle below and moving to adjacent numbers on the row below, the maximum total from top to bottom is 23.

		///    3
		///   7 5
		///  2 4 6
		/// 8 5 9 3

		///That is, 3 + 7 + 4 + 9 = 23.

		///Find the maximum total from top to bottom of the triangle below: (answer is 1074)
		///
		///              75
		///             95 64
		///            17 47 82
		///           18 35 87 10
		///          20 04 82 47 65
		///         19 01 23 75 03 34
		///        88 02 77 73 07 63 67
		///       99 65 04 28 06 16 70 92
		///      41 41 26 56 83 40 80 70 33
		///     41 48 72 33 47 32 37 16 94 29
		///    53 71 44 65 25 43 91 52 97 51 14
		///   70 11 33 28 77 73 17 78 39 68 17 57
		///  91 71 52 38 17 14 91 43 58 50 27 29 48
		/// 63 66 04 68 89 53 67 30 73 16 69 87 40 31
		///04 62 98 27 23 09 70 98 73 93 38 53 60 04 23

		///NOTE: As there are only 16384 routes, it is possible to solve this problem by trying every route. However, Problem 67, is the same challenge with d triangle containing one-hundred rows; it cannot be solved by brute force, and requires d clever method! ;o)



		static Stopwatch sw = new Stopwatch();
		private static int[,] nodes;
		private static int maxSum = 0;
		private static int numRows;

		public static void Solve()
		{
			LoadData();
			sw.Start();

			DoTopDownAdd();

			//AppendNext(0, 0, nodes[0, 0]);

			sw.Stop();
			Console.WriteLine("Found {0} in {1}", maxSum, sw.Elapsed);
		}

		private static void DoTopDownAdd()
		{
			int maxUpper = 0;
			for (int y = 1; y < numRows; y++)
			{
				for (int x = 0; x <= y; x++)
				{
					if (x == 0)
					{
						maxUpper = nodes[x, y - 1];
					}
					else if (x == y)
					{
						maxUpper = nodes[x - 1, y - 1];
					}
					else
					{
						maxUpper = Math.Max(nodes[x, y - 1], nodes[x - 1, y - 1]);
					}

					nodes[x, y] += maxUpper;
					if (y == numRows - 1)
					{
						maxSum = (nodes[x, y] > maxSum) ? nodes[x, y] : maxSum;
					}
				}
			}

		}


		private static void AppendNext(int x, int y, int summedValue)
		{
			if (y < numRows - 1)
			{

				int yNew = y + 1;

				AppendNext(x, yNew, summedValue + nodes[x, yNew]);

				AppendNext(x + 1, yNew, summedValue + nodes[x + 1, yNew]);

			}
			else
			{
				// we're at the bottom
				if (summedValue > maxSum)
				{
					maxSum = summedValue;
				}
				Console.WriteLine("{0},{1} = {2} and max of {3} ", x, y, summedValue, maxSum);
			}


		}


		private static void LoadData()
		{
			string[] linesTest = new string[] {
                "3",
                "7,5",
                "2,4,6",
                "8,5,9,3"}; // should find 23 (3 + 7 + 4 + 9)
			string[] lines = new string[] {
                "75",
                "95,64",
                "17,47,82",
                "18,35,87,10",
                "20,04,82,47,65",
                "19,01,23,75,03,34",
                "88,02,77,73,07,63,67",
                "99,65,04,28,06,16,70,92",
                "41,41,26,56,83,40,80,70,33",
                "41,48,72,33,47,32,37,16,94,29",
                "53,71,44,65,25,43,91,52,97,51,14",
                "70,11,33,28,77,73,17,78,39,68,17,57",
                "91,71,52,38,17,14,91,43,58,50,27,29,48",
                "63,66,04,68,89,53,67,30,73,16,69,87,40,31",
                "04,62,98,27,23,09,70,98,73,93,38,53,60,04,23"};

			string[] myDataRows = lines;
			numRows = myDataRows.Length;
			nodes = new int[numRows, numRows];
			for (int y = 0; y < numRows; y++)
			{
				string[] thisRow = myDataRows[y].Split(',');
				for (int x = 0; x <= y; x++)
				{
					nodes[x, y] = Int32.Parse(thisRow[x]);
				}
			}
		}
	}

}
