using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace ProjectEuler.Problems
{
	class Problem107
	{
		static readonly Stopwatch sw = new Stopwatch();

		private const string FILE_NAME = @"..\..\Data\Problem107.txt";
		private const int NUM_NODES = 40;
		//private const string FILE_NAME = @"..\..\Data\Problem107Test.txt";
		//private const int NUM_NODES = 7;

		private static int[,] weights;

		public static void Solve()
		{
			ReadData();

			int sumLowest = 0;
			int sumCurrent = 0;
			for (int y = 1; y < NUM_NODES; y++)
			{
				int minForThisRow = Int32.MaxValue;
				for (int x = 0; x < y; x++)
				{
					int weight = weights[x, y];
					if(weight==-1)
						continue;
					sumCurrent += weight;
					if (weight < minForThisRow)
						minForThisRow = weight;
				}
				if(minForThisRow != Int32.MaxValue)
				{
					sumLowest += minForThisRow;
				}
			}
			Console.WriteLine("sumCurrent = {0}", sumCurrent);
			Console.WriteLine("sumLowest = {0}", sumLowest);
			Console.WriteLine("Savings = {0}", sumCurrent - sumLowest);
		}

		private static void ReadData()
		{
			weights = new int[NUM_NODES, NUM_NODES];
			string[] lines = File.ReadAllLines(FILE_NAME);
			for (int y = 0; y < NUM_NODES; y++)
			{
				string[] vals = lines[y].Split(',');
				for (int x = 0; x < NUM_NODES; x++)
				{
					weights[x, y] = Int32.Parse(vals[x]);
					if(x<=y)
					Console.Write("[{0,3}] ", weights[x,y]);
				}
				Console.WriteLine();
			}

		}
	}
}
