using System;
using System.Collections.Generic;
using System.Text;
using ProjectEuler.Helpers;

namespace ProjectEuler.Problems.Solved
{
	class Problem062
	{
		// The cube, 41063625 (345^3), can be permuted to produce two other cubes: 
		// 56623104 (384^3) and 66430125 (405^3). In fact, 41063625 is the smallest 
		// cube which has exactly three permutations of its digits which are also cube.

		// Find the smallest cube for which exactly five permutations of its digits are cube.

		//Starting at 3/19/2008 4:03:55 PM
		//at 1 giving 1, has 1 matches

		//	Match 5 8 125 512
		//at 5 giving 125, has 2 matches

		//	Match 345 384 41063625 56623104
		//	Match 345 405 41063625 66430125
		//at 345 giving 41063625, has 3 matches

		//	Match 1002 1020 1006012008 1061208000
		//	Match 1002 2001 1006012008 8012006001
		//	Match 1002 2010 1006012008 8120601000
		//at 1002 giving 1006012008, has 4 matches

		//	Match 5027 7061 127035954683 352045367981
		//	Match 5027 7202 127035954683 373559126408
		//	Match 5027 8288 127035954683 569310543872
		//	Match 5027 8384 127035954683 589323567104
		//at 5027 giving 127035954683, has 5 matches

		//End of main (00:01:08.4928160) 3/19/2008 4:05:03 PM



		public static void Solve()
		{
			int maxCnt = 0;
			for (int n = 1; n < 10000000 && maxCnt < 5; n++)
			{
				double cube = Math.Pow(n, 3);
				string toMatch = Histogram.NumericHistogram(cube);

				int cntMatches = 0;
				double min = Math.Pow(10, cube.ToString().Length - 1);
				double max = min * 10;
				min = Math.Round(Math.Pow(min, 1d / 3), 0);
				max = Math.Round(Math.Pow(max, 1d / 3), 0);


				for (int t = (int)min; t <= max; t++)
				{
					double thisT3 = Math.Pow(t, 3);
					if (Histogram.NumericHistogram(thisT3) == toMatch)
					{
						//Console.WriteLine("Match {0} {1} {2} {3}", n, t, cube, thisT3);
						cntMatches++;
					}

				}

				if (cntMatches > maxCnt)
				{
					for (int t = (int)min; t <= max; t++)
					{
						if (n == t)
							continue;
						double thisT3 = Math.Pow(t, 3);
						if (Histogram.NumericHistogram(thisT3) == toMatch)
						{
							Console.WriteLine("Match {0} {1} {2} {3}", n, t, cube, thisT3);
						}
					}

					maxCnt = cntMatches;
					Console.WriteLine("at {0} giving {1}, has {2} matches", n, Math.Pow(n, 3), maxCnt);
				}
			}
		}
	}
}
