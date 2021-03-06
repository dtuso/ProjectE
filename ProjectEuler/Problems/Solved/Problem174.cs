using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectEuler.Problems
{

	//We shall define a square lamina to be a square outline with a square "hole" so that the 
	//shape possesses vertical and horizontal symmetry.

	//Given eight tiles it is possible to form a lamina in only one way: 3x3 square with a 1x1 
	//hole in the middle. However, using thirty-two tiles it is possible to form two distinct laminae.

	//If t represents the number of tiles used, we shall say that t = 8 is type L(1) and t = 32 is type L(2).

	//Let N(n) be the number of t ≤ 1000000 such that t is type L(n); for example, N(15) = 832.

	//What is ∑ N(n) for 1 ≤ n ≤ 10?


	class Problem174
	{
		public static void Solve()
		{
			Console.WriteLine("Problem 174");

			int MAX = 1000000;

			int[] countByTile = new int[MAX+1];

			for (int s0 = 250001; s0 >= 3; s0 -= 1)
				for (int s1 = s0 - 2; s1 > 0; s1 -= 2)
				{
					int tiles = s0*s0 - s1*s1;
					if(tiles>MAX) break;
					countByTile[tiles]++;
				}

			int maxSolution = 0;
			for (int t = MAX; t >= 8; t -= 4)
				if (countByTile[t] > maxSolution) maxSolution = countByTile[t];
			ulong[] N = new ulong[maxSolution + 1];
			for (int t = MAX; t >= 8; t-=4)
				N[countByTile[t]]++;

			//Console.WriteLine("N[15] = {0}", numTsToMake[15]);
			ulong sum = 0;
			for (int n = 1; n <11 ; n++)
			{
				sum += N[n];
				//Console.WriteLine("N[{0}] = {1}", n, numTsToMake[n]);
			}
			Console.WriteLine("sum = {0}", sum);
		}

	}
}
