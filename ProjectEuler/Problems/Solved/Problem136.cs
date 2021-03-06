using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectEuler.Problems
{
	// The positive integers, x, y, and z, are consecutive terms of an arithmetic 
	// progression. Given that n is a positive integer, the equation, 
	// x^(2) − y^(2) − z^(2) = n, has exactly one solution when n = 20:

	// 13^(2) − 10^(2) − 7^(2) = 20

	// In fact there are twenty-five values of n below one hundred for which 
	// the equation has a unique solution.

	// How many values of n less than fifty million have exactly one solution?

	//Start of Main 1/21/2009 8:38:04 AM
	//countUnarySolutions = 2544559
	//End of Main (00:03:32.3010746) 1/21/2009 8:41:36 AM


	class Problem136
	{
		public static void Solve()
		{
			decimal MAX_N = 50000000;
			decimal[] solutions = new decimal[(int)MAX_N + 1];

			for (decimal a = 2; a <= MAX_N; a++)
			{
				decimal n = 0;
				for (decimal b = Math.Ceiling(a / 4.0m); (b < a) && ((n = a * (4 * b - a)) <= MAX_N); b++)
				{
					solutions[(int)n]++;
				}
			}

			int countUnarySolutions = 0;
			for (decimal n = 1; n < MAX_N; n++)
			{
				//Console.Write("{0,4} {1,5} ", n, solutions[n]);
				if (solutions[(int)n] == 1)
				{
					countUnarySolutions++;
					//Console.Write(n + " ");
				}
				//else
					//Console.WriteLine();
			}
			Console.WriteLine();
			Console.WriteLine("countUnarySolutions = {0}",countUnarySolutions);
		}
	}
}
