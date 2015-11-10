using System;
using ProjectEuler.Helpers;

namespace ProjectEuler.Problems
{
	class Problem052
	{

		// It can be seen that the number, 125874, and its double, 251748, contain exactly the same digits, 
		// but in d different order.

		// Find the smallest positive integer, x, such that 2x, 3x, 4x, 5x, and 6x, contain the same digits.


		//Starting at 3/5/2008 1:36:15 PM
		//142857 =  0 1 1 0 1 1 0 1 1 0
		//End of main (00:00:01.5236128) 3/5/2008 1:36:17 PM


		public static void Solve()
		{
			bool found = false;

			for (int i = 1; !found; i++)
			{
				string thisI = Histogram.NumericHistogram(i);

				if (Histogram.NumericHistogram(i * 2) == thisI &&
					Histogram.NumericHistogram(i * 3) == thisI &&
					Histogram.NumericHistogram(i * 4) == thisI &&
					Histogram.NumericHistogram(i * 5) == thisI &&
					Histogram.NumericHistogram(i * 6) == thisI)
				{
					Console.WriteLine("{0} = {1}", i, thisI);
					found = true;
					break;
				}
			}

		}

	}
}
