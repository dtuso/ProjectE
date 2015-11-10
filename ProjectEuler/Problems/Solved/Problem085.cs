using System;
using System.Collections.Generic;
using System.Text;

using ProjectEuler.Helpers;


namespace ProjectEuler.Problems
{
	class Problem085
	{

		// By counting carefully it can be seen that a rectangular grid 
		// measuring 3 by 2 contains eighteen rectangles:

		// Although there exists no rectangular grid that contains exactly two 
		// million rectangles, find the area of the grid with the nearest solution.


		//3 815 1995120 4880
		//3 816 2000016 16
		//36 77 1999998 2
		//2772
		//End of Main (00:00:04.9591342) 4/8/2008 10:49:15 AM

		public static void Solve()
		{
			int toFind = 2000000;
			long minDistance = 2000000;
			int maxX = 1000;
			int maxY = 1000;
			long minDistAtArea = 0;

			for (int x = 3; x < maxX; x++)
			{
				for (int y = x; y < maxY; y++)
				{
					long thisCount = CountRectangles(x, y);
					long diff = Math.Abs(thisCount - toFind);
					if (diff<minDistance)
					{
						Console.WriteLine("{0} {1} {2} {3}", x,y,thisCount,diff);
						minDistance = diff;
						minDistAtArea = x*y;
					}
				}
			}
			Console.WriteLine(minDistAtArea);

		}

		private static long CountRectangles(int numColumns, int numRows)
		{
			long rectangles = 0;
			long oneRowCount = MyMath.SumNToZero(numColumns);
			for (int i = numRows; i > 0; i--)
				rectangles = checked(rectangles + i * oneRowCount);
			return rectangles;
		}

	}
}
