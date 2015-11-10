using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using ProjectEuler.Helpers;

namespace ProjectEuler.Problems
{
	class Problem215
	{
		/*

		Consider the problem of building a wall out of 2x1 and 3x1 bricks (horizontal x vertical dimensions) 
		such that, for extra strength, the gaps between horizontally-adjacent bricks never line up in 
		consecutive layers, i.e. never form a "running crack".

		There are eight ways of forming a crack-free 9x3 wall, written W(9,3) = 8.

		Calculate W(32,10).

		 */


		private static int wallWidth;
		private static int wallRows;
		private static List<string> validBrickRowDefs;
		private static List<BrickRow> validBrickRows;

		//public static bool[,] wall;
		private static int numSolutions = 0;
		public static void Solve(int width, int rows)
		{

			wallWidth = width;
			wallRows = rows;
			Console.WriteLine("Starting 215 wallWidth: {0} wallRows: {1}", wallWidth, wallRows);
			

			BuildValidRows();

			DeterminePossibleStackableRows();

			int validRowNbr = 0;
			Stopwatch sw = new Stopwatch();
			foreach (BrickRow startingRow in validBrickRows)
			{
				sw.Start();
				DoStacks(wallRows - 1, startingRow);
				sw.Stop();
				validRowNbr++;
				Console.WriteLine("{2}: {0,4} of {1,4} {3}", validRowNbr, validBrickRows.Count, sw.Elapsed, BrickRowToString(startingRow));
				sw.Reset();
			}

			Console.WriteLine("Number Solutions: {0}", numSolutions);
			Console.WriteLine();
		}

		private static void DeterminePossibleStackableRows()
		{
			int numStackables = 0;
			int maxStackable = 0;
			int minStackable = Int32.MaxValue;
			foreach (BrickRow bottom in validBrickRows)
			{
				int thisNumStackable = 0;
				bottom.PossibleValidRows = new List<BrickRow>();
				foreach (BrickRow top in validBrickRows)
				{
					if(IsValidStack(bottom, top))
					{
						bottom.PossibleValidRows.Add(top);
						thisNumStackable++;
					}
				}
				numStackables+=thisNumStackable;
				if (thisNumStackable < minStackable) minStackable = thisNumStackable;
				if (thisNumStackable > maxStackable) maxStackable = thisNumStackable;

			}
			Console.WriteLine("Determined stackables: {0} {1} {2:F4} {3}", numStackables, minStackable, (double)numStackables / validBrickRows.Count, maxStackable);
		}

		private static void BuildValidRows()
		{
			validBrickRowDefs = new List<string>();
			validBrickRows = new List<BrickRow>();

			int maxBricksWide = wallWidth / 2;

			for (int layout = 0; layout <= (int)Math.Pow(2, maxBricksWide); layout++)
			{
				string brickRowDef = "";
				bool[] brickRowCracks = new bool[wallWidth + 1];
				string layoutString = MiscFunctions.ToBinary(layout, maxBricksWide);
				char[] bricks = layoutString.ToCharArray();
				int widthBricks = 0;
				foreach (char brick in bricks)
				{
					widthBricks += (brick == '0' ? 2 : 3);
					if (widthBricks > wallWidth)
						break;
					brickRowCracks[widthBricks] = true; // line here!!
					brickRowDef += brick;
					if (widthBricks == wallWidth)
					{
						if (!validBrickRowDefs.Contains(brickRowDef))
						{
							validBrickRowDefs.Add(brickRowDef);
							BrickRow newRow = new BrickRow(brickRowDef, brickRowCracks);
							validBrickRows.Add(newRow);
						}
						break;
					}
				}
			}
			Console.WriteLine("Number validRows: {0}", validBrickRows.Count);
		}

		private static void DoStacks(int numRowsToAdd, BrickRow topRow)
		{
			for (int idx = 0; idx < topRow.PossibleValidRows.Count; idx++)
			{
				if (numRowsToAdd > 1)
				{
					DoStacks(numRowsToAdd - 1, topRow.PossibleValidRows[idx]);
				}
				else
				{
					numSolutions++;
				}
			}
		}


		private static bool IsValidStack(BrickRow upper, BrickRow lower)
		{
			if (upper.BrickRowDefinition==lower.BrickRowDefinition) 
				return false;
			for (int idx = 2; idx < (wallWidth-1); idx++)
			{
				if (upper.RowCracks[idx] && lower.RowCracks[idx])
					return false;
			}
			return true;
		}

		private static string BrickRowToString(BrickRow row)
		{
			StringBuilder sb = new StringBuilder();
			for (int idx = 1; idx <= row.RowCracks.Length - 1; idx++)
			{
				sb.Append(row.RowCracks[idx] ? "|" : ".");
			}
			return sb.ToString();

		}
	}
	


	class BrickRow
	{

		public bool[] RowCracks;
		public string BrickRowDefinition;
		public List<BrickRow> PossibleValidRows;
		
		public BrickRow(string brickRowDefinition, bool[] rowCracks)
		{
			BrickRowDefinition = brickRowDefinition;
			RowCracks = rowCracks;
		}
	}
}
