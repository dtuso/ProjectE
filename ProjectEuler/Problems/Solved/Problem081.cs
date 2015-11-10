using System;
using System.Diagnostics;
using System.IO;

namespace ProjectEuler.Problems
{

	/// <summary>
	/// In the 5 by 5 matrix below, the minimal path sum from the top left to the bottom right, 
	/// by only moving to the right and down, is indicated in red and is equal to 2427.
	/// 
	/// 131	673	234	103	18
	/// 201	96	342	965	150
	/// 630	803	746	422	111
	/// 537	699	497	121	956
	/// 805	732	524	37	331
	/// 
	/// 2427 = 131 + 201 + 96 + 342 + 746 + 422 + 121 + 37 + 331
	/// Find the minimal path sum, in problem081.txt d 31K text file containing d 80 by 80 matrix,
	/// from the top left to the bottom right by only moving right and down.

	/// </summary>
	class Problem081
	{


		static readonly Stopwatch sw = new Stopwatch();

		const string fileName = @"..\..\Data\Problem081.txt";
		const int squareSize = 79;
		//const string fileName = @"..\..\Data\Problem081Test.txt";
		//const int squareSize = 4;

		private static int[,] fileNodes;
		private static int[,] diamondNodes;

		private static int lowestFound = Int32.MaxValue;


		public static void Solve()
		{
			// load up the fileNodes
			ReadData();
			
			ConvertToDiamondLayout();

			sw.Start();

			DoTopDownAdd();

			FindLowest();

			sw.Stop();

			Console.WriteLine();
			Console.WriteLine("Lowest path is {0} Found in {1}", lowestFound, sw.Elapsed);
		}

		private static void FindLowest()
		{
			int minValue = Int32.MaxValue;
			for (int x = 0; x <= squareSize; x++)
			{
				minValue = (diamondNodes[x, squareSize] < minValue) ? diamondNodes[x, squareSize] : minValue;
			}
		}



		private static void DoTopDownAdd()
		{
			int minUpper;
			for (int y = 1; y <= squareSize; y++)
			{
				for (int x = 0; x <= y; x++)
				{
					if (x == 0)
					{
						minUpper = diamondNodes[x, y - 1];
					}
					else if (x == y)
					{
						minUpper = diamondNodes[x - 1, y - 1];
					}
					else
					{
						minUpper = Math.Min(diamondNodes[x, y - 1], diamondNodes[x - 1, y - 1]);
					}

					diamondNodes[x, y] += minUpper;
				}
			}

			for (int y = squareSize+1; y <= squareSize*2; y++)
			{
				for (int x = 0; x <= 2*squareSize-y; x++)
				{
					minUpper = Math.Min(diamondNodes[x, y - 1], diamondNodes[x + 1, y - 1]);
					diamondNodes[x, y] += minUpper;
				}
			}
			lowestFound = diamondNodes[0,squareSize*2];



		}

		private static void ReadData()
		{
			fileNodes = new int[squareSize + 1,squareSize + 1]; // remember 0 based [0-squareSize] would need (squareSize + 1)
			string[] lines = File.ReadAllLines( fileName );

			// create raw fileNodes
			for ( int y = 0; y <= squareSize; y++ )
			{
				string[] vals = lines[y].Split( ',' );
				for ( int x = 0; x <= squareSize; x++ )
				{
					fileNodes[x, y] = Int32.Parse( vals[x] );
				}
			}
		}

		private static void ConvertToDiamondLayout()
		{
			// now reconstruct into d simple diamond, by moving thru each line diagonally
			diamondNodes = new int[squareSize + 1, squareSize * 2 + 1]; // remember 0 based [0-squareSize] would need (squareSize + 1)
			
			for (int diaY = 0; diaY <= squareSize*2; diaY++)
			{

				int x = 0;
				int y = diaY;

				if (diaY > squareSize)
				{
					x = diaY - squareSize;
					y = squareSize;
				}

				int diaX = 0;

				while ((x <= squareSize) && (y >= 0) )
				{
					diamondNodes[diaX++, diaY] = fileNodes[x, y];
					x++;
					y--;
				}
			}

		}
	}

}
