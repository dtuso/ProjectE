using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectEuler.Problems
{

	// If p is the perimeter of d right angle triangle with integral length sides, {d,b,c}, 
	// there are exactly three solutions for p = 120.

	//        {20,48,52}, {24,45,51}, {30,40,50}

	// For which value of p < 1000, is the number of solutions maximised?


	//Start of Main 3/4/2008 9:37:52 AM
	//maxTris 16 having d perimeter of 840
	//End of Main (00:00:05.5705883) 3/4/2008 9:37:57 AM


	class Problem039
	{
		public static void Solve()
		{
			int maxTris = 0;
			int maxPerimeter = 0;
			for (int perimeter = 12; perimeter < 1000; perimeter++)
			{
				int countTris = CountRightTriangles(perimeter);
				if (countTris > maxTris)
				{
					maxPerimeter = perimeter;
					maxTris = countTris;
				}
			}
			Console.WriteLine("maxTris {0} having d perimeter of {1}", maxTris, maxPerimeter);
		}
		
		private static int CountRightTriangles(int perimeter)
		{
			int count = 0;
			for (int a = 1; a <= perimeter - 1; a++)
			{
				for ( int b = 1; b <= perimeter - a - 1; b++ )
				{
					if ((perimeter - a -b) == GetHypotenuse(a, b))
					{
						count++;
					}
				}
			}
			return count;
		}
		
		private static double GetHypotenuse(int a, int b)
		{
			return Math.Sqrt( a*a + b*b);
		}
	}
}
