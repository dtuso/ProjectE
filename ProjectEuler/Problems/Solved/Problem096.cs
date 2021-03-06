using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectEuler.Problems.Solved
{
	public enum CellGroupingType
	{
		Horizontal, Vertical, Square
	}
	class Problem096
	{

		public static bool useExample = false;
		public static void Solve()
		{
			//Su Doku (Japanese meaning number place) is the name given to a popular puzzle concept. Its origin is unclear, but credit must be 
			//attributed to Leonhard Euler who invented a similar, and much more difficult, puzzle idea called Latin Squares. The objective of 
			//Su Doku puzzles, however, is to replace the blanks (or zeros) in a 9 by 9 grid in such that each row, column, and 3 by 3 box contains 
			//each of the digits 1 to 9. Below is an example of a typical starting puzzle grid and its solution grid.

			//003020600	483921657
			//900305001	967345821
			//001806400	251876493
			//008102900	548132976
			//700000008	729564138
			//006708200	136798245
			//002609500	372689514
			//800203009	814253769
			//005010300	695417382

			//A well constructed Su Doku puzzle has a unique solution and can be solved by logic, although it may be necessary to employ "guess and test" 
			//methods in order to eliminate options (there is much contested opinion over this). The complexity of the search determines the difficulty 
			//of the puzzle; the example above is considered easy because it can be solved by straight forward direct deduction.

			//The 6K text file, sudoku.txt (right click and 'Save Link/Target As...'), contains fifty different Su Doku puzzles ranging in difficulty, 
			//but all with unique solutions (the first puzzle in the file is the example above).

			//By solving all fifty puzzles find the sum of the 3-digit numbers found in the top left corner of each solution grid; for example, 
			//483 is the 3-digit number found in the top left corner of the solution grid above.
			List<Sudoku> games = LoadSudokuData();
			int sum = 0;
			int gameNum = 1;
			foreach (Sudoku game in games)
			{
				//if (gameNum == 7)
				{
					Console.WriteLine();
					Console.WriteLine();
					Console.WriteLine("***************");
					Console.WriteLine("*** Game {0,2} ***", gameNum);
					//Console.WriteLine("***************");
					game.SolveSudoku(gameNum, false);
					int gameSum = 0;
					gameSum += (game.solvedData[0, 0].Number * 100);
					gameSum += (game.solvedData[1, 0].Number * 10);
					gameSum += (game.solvedData[2, 0].Number);
					Console.WriteLine("Game Sum {0}", gameSum);
					sum += gameSum;
				}
				gameNum++;
			}
			Console.WriteLine("Total Game Sums: {0}", sum);

		}

		private static List<Sudoku> LoadSudokuData()
		{
			List<Sudoku> games = new List<Sudoku>();
			if (useExample)
			{
				Sudoku game = new Sudoku("003020600900305001001806400008102900700000008006708200002609500800203009005010300");
				games.Add(game);
			}
			else
			{
				string fileText = System.IO.File.ReadAllText("..\\..\\data096.txt");
				string[] lines = fileText.Split('\n');
				string gameData = "";
				int countlines = 0;
				foreach (string line in lines)
				{
					if (!line.Contains("Grid"))
					{
						gameData += line.Substring(0, 9); // strip off any trailing characters!
						countlines++;
					}
					if (countlines == 9)
					{
						Sudoku game = new Sudoku(gameData);
						games.Add(game);
						countlines = 0;
						gameData = "";
					}
				}

			}
			return games;

		}

	}


	class SudokuCell
	{
		public int Col;
		public int Row;
		private int _number;
		public int Number
		{
			get
			{
				return _number;
			}
			set
			{
				_number = value;
				for (int num = 1; num < 10; num++)
				{
					PossibleNumbers[num] = (_number == 0);
				}
				PossibleNumbers[_number] = true;
			}

		}
		public bool[] PossibleNumbers = new bool[10];

		public SudokuCell(string number, int x, int y)
		{
			Number = Int32.Parse(number);
			Col = x;
			Row = y;
		}

		public SudokuCell(int number, int x, int y)
		{
			Number = number;
			Col = x;
			Row = y;
		}

		public bool IsSolved()
		{
			if (Number > 0) return true;
			if (CountPossible == 1)
			{
				for (int num = 1; num < 10; num++)
				{
					if (PossibleNumbers[num])
					{
						Number = num;
						return true;
					}
				}
			}
			return false;

		}
		public int CountPossible
		{
			get
			{
				if (Number > 0) return 1;
				int cntPossible = 0;
				for (int num = 1; num < 10; num++)
				{
					if (PossibleNumbers[num])
					{
						cntPossible++;
					}
				}
				return cntPossible;
			}
		}
		/// <summary>
		/// Returns a 10 character string. Pos 1 = CountPossible, pos 2-10 are either a '-' if not possible, or the number if it is left possible.
		/// </summary>
		public string Possibilities
		{
			get
			{
				string possibilities = CountPossible.ToString();
				for (int num = 1; num < 10; num++)
				{
					possibilities += (PossibleNumbers[num]) ? num.ToString() : "-";
				}
				return possibilities;
			}
		}

		public override string ToString()
		{
			return String.Format("[{0},{1}] = {2}", Col, Row, Number);
			//return Number.ToString();
		}
		public override bool Equals(object obj)
		{
			return (this == (SudokuCell)obj);
		}
		public static bool operator ==(SudokuCell s1, SudokuCell s2)
		{
			return ((s1.Row == s2.Row) && (s1.Col == s2.Col));
		}
		public static bool operator !=(SudokuCell s1, SudokuCell s2)
		{
			return !((s1.Row == s2.Row) && (s1.Col == s2.Col));
		}



	}


	class Sudoku
	{

		public SudokuCell[,] originalData = new SudokuCell[9, 9];
		public SudokuCell[,] solvedData = new SudokuCell[9, 9];

		public bool SolveSudoku(int thisProblemNumber, bool debug)
		{

			PrintBig(originalData);

			// we'll start at the beginning and
			// use solvedData as our work area.
			for (int x = 0; x < 9; x++)
			{
				for (int y = 0; y < 9; y++)
				{
					solvedData[x, y] = new SudokuCell(originalData[x, y].Number, x, y);
				}
			}
			int round = 1;
			int thisRoundNumLeft = -1;
			int lastRoundNumLeft = -1;

			do
			{

				//Check Horizontals
				for (int row = 0; row < 9; row++)
				{
					// get list of cells for this column
					List<SudokuCell> cells = GetCellGroupingFor(solvedData[0, row], CellGroupingType.Horizontal);
					// apply logic to this independant grouping
					NarrowDownIndependantCellGrouping(cells, CellGroupingType.Horizontal);
				}

				//Check Verticals
				for (int col = 0; col < 9; col++)
				{
					// get list of cells for this column
					List<SudokuCell> cells = GetCellGroupingFor(solvedData[col, 0], CellGroupingType.Vertical);
					// apply logic to this independant grouping
					NarrowDownIndependantCellGrouping(cells, CellGroupingType.Vertical);
				}

				// Check Square Groups
				for (int gridRow = 0; gridRow < 3; gridRow++)
				{
					for (int gridCol = 0; gridCol < 3; gridCol++)
					{
						// create a list of cells for this group
						List<SudokuCell> cells = GetCellGroupingFor(solvedData[gridCol * 3, gridRow * 3], CellGroupingType.Square);
						// apply logic to this independant grouping
						NarrowDownIndependantCellGrouping(cells, CellGroupingType.Square);
					}
				}


				// **** NOTE ****
				// SEE: http://www.sudokuessentials.com/sudoku_tips.html
				// **** NOTE ****

				if (debug)
				{
					Console.WriteLine("After Round {0}:", round);
					PrintBig(solvedData);
				}

				thisRoundNumLeft = CountNumberLeft(solvedData);

				round++;
				if (thisRoundNumLeft == lastRoundNumLeft)
				{
					Console.WriteLine("!!!!!!!!!!!!!!!!! Not making any headway on {0} !!!!!!!!", thisProblemNumber);
					if (debug) PrintBig(solvedData);
					if (CheckValid())
					{
						bool guessedCorrect = false;
						// now make guesses and solve each guess
						for (int row = 0; row < 10; row++)
						{
							if (guessedCorrect) break;
							for (int col = 0; col < 10; col++)
							{
								if (guessedCorrect) break;
								if (solvedData[col, row].Number == 0)
								{
									for (int i = 1; i < 10; i++)
									{
										string theseUsed = solvedData[col, row].Possibilities;
										if (theseUsed.Substring(i, 1) != "-")
										{
											Sudoku newPuzzle = new Sudoku(solvedData);
											newPuzzle.originalData[col, row].Number = i;
											guessedCorrect = newPuzzle.SolveSudoku(thisProblemNumber, debug);
											if (guessedCorrect)
											{
												solvedData = newPuzzle.solvedData;
											}

										}
										if (guessedCorrect) break;
									}
								}
							}
						}
					}
					break;
				}
				lastRoundNumLeft = thisRoundNumLeft;
			} while (!IsCompletelySolved);

			if (!debug) PrintBig(solvedData);
			return CheckValid();
		}

		public bool IsCompletelySolved
		{
			get
			{

				for (int x = 0; x < 9; x++)
				{
					for (int y = 0; y < 9; y++)
					{
						if (solvedData[x, y].Number == 0)
							return false;
					}
				}
				return true;
				//return (solvedData[0, 0].Number != 0 && solvedData[1, 0].Number != 0 && solvedData[2, 0].Number != 0);
			}
		}

		public Sudoku(string rawData)
		{
			LoadStringData(rawData);
		}

		public Sudoku(SudokuCell[,] original_data)
		{
			for (int x = 0; x < 9; x++)
			{
				for (int y = 0; y < 9; y++)
				{
					int idx = y * 9 + x;
					originalData[x, y] = new SudokuCell(original_data[x, y].Number, x, y);
				}
			}
		}

		private void LoadStringData(string raw)
		{
			for (int x = 0; x < 9; x++)
			{
				for (int y = 0; y < 9; y++)
				{
					int idx = y * 9 + x;
					originalData[x, y] = new SudokuCell(raw.Substring(idx, 1), x, y);
				}
			}
		}

		public void Print(bool printOriginal, bool printSolution)
		{
			if (printOriginal)
				Print(originalData);

			if (printSolution)
				Print(solvedData);

		}

		private int CountNumberLeft(SudokuCell[,] data)
		{
			int numLeft = 0;
			for (int row = 0; row < 9; row++)
			{
				for (int col = 0; col < 9; col++)
				{
					if (data[col, row].Number == 0)
					{
						numLeft++;
					}
				}
			}
			return numLeft;
		}

		private void PrintBig(SudokuCell[,] data)
		{
			string longdash = new string('─', 11);
			Console.WriteLine("┌{0}┬{0}┬{0}┐", "───┬───┬───");
			for (int row = 0; row < 9; row++)
			{
				for (int subrow = 1; subrow <= 3; subrow++)
				{
					Console.Write("│");
					for (int col = 0; col < 9; col++)
					{
						for (int possibleNbr = (1 + (subrow - 1) * 3); possibleNbr <= (subrow * 3); possibleNbr++)
						{
							if (data[col, row].Number == 0)
								Console.Write((data[col, row].PossibleNumbers[possibleNbr]) ? (possibleNbr).ToString() : " ");
							else
								Console.Write(data[col, row].Number.ToString());
						}
						Console.Write("│");
					}
					Console.WriteLine();
					//if (subrow < 3)
					//   Console.WriteLine("│{0}│{0}│{0}│", longdash);
				}

				if ((row + 1) % 3 == 0)
				{
					if (row < 8)
						Console.WriteLine("├{0}┼{0}┼{0}┤", "───┼───┼───");
				}
				else
				{
					Console.WriteLine("├{0}┼{0}┼{0}┤", "───┼───┼───");
				}

			}
			Console.WriteLine("└{0}┴{0}┴{0}┘", "───┴───┴───");
			Console.WriteLine();
		}
		private void Print(SudokuCell[,] data)
		{
			for (int row = 0; row < 9; row++)
			{
				for (int col = 0; col < 9; col++)
				{
					string print;
					if (data[col, row].Number == 0)
					{
						print = "- ";
					}
					else
					{
						print = String.Format("{0} ", data[col, row].Number);
					}
					Console.Write(print);
				}
				Console.WriteLine();
			}
			Console.WriteLine("\t Number left: {0}", CountNumberLeft(data));
		}

		private List<SudokuCell> GetCellGroupingFor(SudokuCell cell, CellGroupingType type)
		{
			int col = cell.Col;
			int row = cell.Row;
			List<SudokuCell> cells = new List<SudokuCell>();
			switch (type)
			{
				case CellGroupingType.Horizontal:
					for (int i = 0; i < 9; i++)
					{
						cells.Add(solvedData[i, row]);
					}
					break;
				case CellGroupingType.Vertical:
					for (int i = 0; i < 9; i++)
					{
						cells.Add(solvedData[col, i]);
					}
					break;
				case CellGroupingType.Square:
					int gridCol = col / 3;
					int gridRow = row / 3;
					int gridX = 3 * gridCol;
					int gridY = 3 * gridRow;
					for (int x = gridX; x - gridX < 3; x++)
					{
						for (int y = gridY; y - gridY < 3; y++)
						{
							cells.Add(solvedData[x, y]);
						}
					}
					break;
			}
			return cells;
		}


		private void NarrowDownIndependantCellGrouping(List<SudokuCell> cells, CellGroupingType type)
		{
			bool overallFoundOne;
			bool innerfoundOne;
			do
			{
				overallFoundOne = false;
				do
				{
					innerfoundOne = false;
					CheckForSolitarySolutionInGroup(cells, ref innerfoundOne);
					overallFoundOne |= innerfoundOne;
					if (!CheckValid()) break;
				} while (innerfoundOne);
				if (!CheckValid()) break;
				do
				{
					innerfoundOne = false;
					CheckForMultiplesInGroup(cells, type, ref innerfoundOne);
					if (!CheckValid()) break;
					overallFoundOne |= innerfoundOne;
				} while (innerfoundOne);
				if (!CheckValid()) break;
				do
				{
					innerfoundOne = false;
					CheckForLockingSituations(cells, type, ref innerfoundOne);
					if (!CheckValid()) break;
					overallFoundOne |= innerfoundOne;
				} while (innerfoundOne);
				if (!CheckValid()) break;
			} while (overallFoundOne);
		}

		private void CheckForLockingSituations(List<SudokuCell> cells, CellGroupingType type, ref bool foundOne)
		{
			for (int checkNum = 1; checkNum < 10; checkNum++)
			{
				List<SudokuCell> matches = new List<SudokuCell>();
				foreach (SudokuCell cell in cells)
				{
					if (cell.Number == 0)
					{
						if (cell.PossibleNumbers[checkNum])
							matches.Add(cell);
					}
				}
				ClearOutLockingSituations(matches, checkNum, type, ref foundOne);

			}
		}

		private void ClearOutLockingSituations(List<SudokuCell> matches, int checkNum, CellGroupingType type, ref bool foundOne)
		{
			if (matches.Count > 0)
			{
				switch (type)
				{
					case CellGroupingType.Horizontal:
					case CellGroupingType.Vertical:
						// if type is a col/row, then see if the poss falls in same square
						int sqRow = matches[0].Row / 3;
						int sqCol = matches[0].Col / 3;
						bool sameSquare = true;
						foreach (SudokuCell match in matches)
						{
							if (sqRow != (match.Row / 3) || sqCol != (match.Col / 3))
								sameSquare = false;
						}
						if (sameSquare)
						{
							List<SudokuCell> cellsInSquare = GetCellGroupingFor(matches[0], CellGroupingType.Square);
							foreach (SudokuCell cell in cellsInSquare)
							{
								if (matches.Contains(cell))
									continue; // don't update one of the matches by accident
								foundOne |= SetCellPossibilityToFalse(cell, checkNum);

							}
						}
						break;
					case CellGroupingType.Square:
						// if type is group, then see if they are all possible in same row
						int rowMatch = matches[0].Row;
						bool sameRow = true;
						foreach (SudokuCell match in matches)
						{
							if (rowMatch != match.Row) sameRow = false;
						}
						if (sameRow)
						{
							for (int col = 0; col < 9; col++)
							{
								SudokuCell updateThisCell = solvedData[col, rowMatch];
								if (matches.Contains(updateThisCell))
									continue; // don't update one of the matches by accident
								foundOne |= SetCellPossibilityToFalse(updateThisCell, checkNum);
							}
						}
						// if type is group, then see if they are all possible in same column
						int colMatch = matches[0].Col;
						bool sameCol = true;
						foreach (SudokuCell matchedCell in matches)
						{
							if (colMatch != matchedCell.Col) sameCol = false;
						}
						if (sameCol)
						{
							for (int row = 0; row < 9; row++)
							{
								SudokuCell updateThisCell = solvedData[colMatch, row];
								if (matches.Contains(updateThisCell))
									continue; // don't update one of the matches by accident
								foundOne |= SetCellPossibilityToFalse(updateThisCell, checkNum);
							}
						}
						break;
				}
			}
		}

		private void CheckForMultiplesInGroup(List<SudokuCell> cells, CellGroupingType type, ref bool foundSome)
		{
			WeedOutExistingFoundNumbers(cells);

			int numCellsUnsolved = 0;
			foreach (SudokuCell cell in cells)
			{
				numCellsUnsolved += (cell.Number == 0) ? 1 : 0;
			}

			if (numCellsUnsolved == 0) return;

			// check to see if there's a multiple of cells each with the same set of numbers
			// then that set of numbers should be removed from all the other cells 
			// in this cell group, which aren't one of the multiples of cells.
			// such as a two cells have 4/6 left each.  All the other cells in that line cannot have 4/6 then.

			for (int numMultiples = 2; numMultiples < numCellsUnsolved; numMultiples++)
			{
				List<SudokuCell> multiplesCells = new List<SudokuCell>();
				foreach (SudokuCell cell in cells)
				{
					if (cell.Possibilities.StartsWith(numMultiples.ToString()))
					{
						multiplesCells.Add(cell);
					}
				}
				foreach (SudokuCell checkCell in multiplesCells) // multuplesCells is like 4/6, 1/3, 4/6, and 7/8
				{
					List<SudokuCell> matchingCells = new List<SudokuCell>();
					matchingCells.Add(checkCell);
					string theseUsed = checkCell.Possibilities;
					foreach (SudokuCell possibleMatchingCell in multiplesCells)
					{
						if (checkCell == possibleMatchingCell)
							continue;
						if (theseUsed == possibleMatchingCell.Possibilities)
						{
							matchingCells.Add(possibleMatchingCell);
						}
					}

					if (numMultiples == matchingCells.Count)
					{
						// this set of 4/6
						foundSome |= CleanOtherCells(cells, theseUsed);

						// loop thru each individual number and see if they match up in a row/col/square lock based upon the type they're not.						
						for (int i = 1; i < 10; i++)
						{
							if (theseUsed.Substring(i, 1) != "-")
							{
								ClearOutLockingSituations(matchingCells, i, type, ref foundSome);
							}
						}
					}
				}
			}
		}

		private bool CleanOtherCells(List<SudokuCell> cells, string values)
		{
			bool anyCleaned = false;
			for (int i = 1; i < 10; i++)
			{
				if (values.Substring(i, 1) != "-")
				{
					foreach (SudokuCell cell in cells)
					{
						if (cell.Number > 0)
							continue;
						if (cell.Possibilities == values)
							continue;
						if (cell.PossibleNumbers[i] == false)
							continue;

						anyCleaned |= SetCellPossibilityToFalse(cell, i);

					}
				}
			}
			return anyCleaned;
		}

		private void CheckForSolitarySolutionInGroup(List<SudokuCell> cells, ref bool foundSome)
		{
			WeedOutExistingFoundNumbers(cells);

			// check for only one position left for a specific number
			int[] cntLeft = new int[10];
			foreach (SudokuCell cell in cells)
			{
				for (int numToCheckFor = 1; numToCheckFor < 10; numToCheckFor++)
				{
					if (cell.PossibleNumbers[numToCheckFor])
					{
						cntLeft[numToCheckFor]++;
					}
				}
			}
			for (int numToCheckFor = 1; numToCheckFor < 10; numToCheckFor++)
			{
				if (cntLeft[numToCheckFor] == 1)
				{
					//now find this cell and set it!
					foreach (SudokuCell cell in cells)
					{
						// ensure this place hasn't been solved before :)
						if (cell.PossibleNumbers[numToCheckFor] && cell.Number == 0)
						{
							cell.Number = numToCheckFor;
							foundSome = true;
							if (!CheckValid())
							{
								int thisIsTheSpotToCheck = 10;
							}

							SolvedCellCleanup(cell);
							break;
						}
					}
				}
			}
		}

		private void WeedOutExistingFoundNumbers(List<SudokuCell> cells)
		{
			// (1) - Singles
			// weed out existing found numbers in grouping
			bool[] numExists = new bool[10];
			foreach (SudokuCell cell in cells)
			{
				int num = cell.Number;
				if (num > 0)
				{
					numExists[num] = true;
				}
			}
			foreach (SudokuCell cell in cells)
			{
				if (cell.Number == 0)
				{
					for (int foundNum = 1; foundNum < 10; foundNum++)
					{
						if (numExists[foundNum])
						{
							SetCellPossibilityToFalse(cell, foundNum);
						}
					}
				}
			}
		}

		private bool SetCellPossibilityToFalse(SudokuCell cell, int number)
		{
			if (cell.Number > 0) return false;
			if (cell.PossibleNumbers[number])
			{
				cell.PossibleNumbers[number] = false;
				bool isSolved = cell.IsSolved();
				if (!CheckValid())
				{
					int thisIsTheSpotToCheck = 10;
				}

				if (isSolved) SolvedCellCleanup(cell);
				return isSolved;
			}
			return false;
		}

		private bool CheckValid()
		{
			bool isValid = true;
			//Check Horizontals
			for (int row = 0; row < 9; row++)
			{
				List<SudokuCell> cells = GetCellGroupingFor(solvedData[0, row], CellGroupingType.Horizontal);
				isValid &= EnsureValidGroup(cells);
			}

			//Check Verticals
			for (int col = 0; col < 9; col++)
			{
				List<SudokuCell> cells = GetCellGroupingFor(solvedData[col, 0], CellGroupingType.Vertical);
				isValid &= EnsureValidGroup(cells);
			}

			// Check Square Groups
			for (int gridRow = 0; gridRow < 3; gridRow++)
			{
				for (int gridCol = 0; gridCol < 3; gridCol++)
				{
					List<SudokuCell> cells = GetCellGroupingFor(solvedData[gridCol * 3, gridRow * 3], CellGroupingType.Square);
					isValid &= EnsureValidGroup(cells);
				}
			}
			return isValid;
		}

		private bool EnsureValidGroup(List<SudokuCell> cells)
		{
			List<int> solved = new List<int>();
			foreach (SudokuCell cell in cells)
			{
				int value = cell.Number;
				if (value > 0)
				{
					if (solved.Contains(value))
					{
						//Console.WriteLine("Bad group contains {0} more than once!", value);

						//foreach (SudokuCell badCell in cells)
						//{
						//    Console.WriteLine(badCell);
						//}
						//PrintBig(solvedData);
						return false;
					}
					solved.Add(value);
				}
			}
			return true;
		}

		private void SolvedCellCleanup(SudokuCell solvedCell)
		{
			string values = solvedCell.Possibilities;

			List<SudokuCell> horiz = GetCellGroupingFor(solvedCell, CellGroupingType.Horizontal);
			CleanOtherCells(horiz, values);

			List<SudokuCell> verts = GetCellGroupingFor(solvedCell, CellGroupingType.Vertical);
			CleanOtherCells(verts, values);

			List<SudokuCell> sqrs = GetCellGroupingFor(solvedCell, CellGroupingType.Square);
			CleanOtherCells(sqrs, values);
		}



	}
	
}
