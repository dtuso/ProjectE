using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectEuler.Problems
{
	//We shall define a square lamina to be a square outline with a square "hole" so 
	//that the shape possesses vertical and horizontal symmetry. For example, using 
	//exactly thirty-two square tiles we can form two different square laminae:

	//With one-hundred tiles, and not necessarily using all of the tiles at one time, 
	//it is possible to form forty-one different square laminae.

	//Using up to one million tiles how many different square laminae can be formed?
	/*
		Start of Main 1/20/2009 10:53:29 AM
		Problem 173 (1000000)
		1572729
		End of Main (00:00:00.0216210) 1/20/2009 10:53:29 AM
	*/
	class Problem173
	{
		public static void Solve(int maxTiles)
		{
			Console.WriteLine("Problem 173 ({0})", maxTiles);
			ulong ways = 2;
			int maxS = FindLargestS(maxTiles);
			for (int s = maxS; s > 2 ; s--)
			{
				FindWays(s, 0, maxTiles, ref ways);
			}
			Console.WriteLine(ways);
		}

		private static void FindWays(int s, int currentTileCount, int maxTiles, ref ulong ways)
		{
			s -= 2;
			if(s<3) return;
			currentTileCount += (4*s - 4);
			if (currentTileCount > maxTiles) return;
			ways++;
			FindWays(s, currentTileCount, maxTiles, ref ways);

		}

		private static int FindLargestS(int numTiles)
		{
			// numTiles = (4*s-4)
			// s = (numTiles + 4)/4
			double s = ((double)numTiles + 4) / 4;
			return (int)Math.Floor(s);
		}

	}
}
