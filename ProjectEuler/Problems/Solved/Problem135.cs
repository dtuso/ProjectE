using System;
using System.Collections.Generic;
using System.Text;
using ProjectEuler.Helpers;

namespace ProjectEuler.Problems
{

	//Given the positive integers, x, y, and z, are consecutive terms of an arithmetic progression, 
	//the least value of the positive integer, n, for which the equation, x^(2) − y^(2) − z^(2) = n, 
	//has exactly two solutions is n = 27:
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           
	//34^(2) − 27^(2) − 20^(2) = 12^(2) − 9^(2) − 6^(2) = 27

	//It turns out that n = 1155 is the least value which has exactly ten solutions.

	//How many values of n less than one million have exactly ten distinct solutions?


//Start of Main 1/20/2009 4:39:26 PM
//2
//10
//4989
//End of Main (00:00:01.4189720) 1/20/2009 4:39:28 PM


	class Problem135
	{

		public static void Solve()
		{
			ulong MAX_N = 1000000;
			ulong[] solutions = new ulong[MAX_N + 1];

			//For[a = 2, a <= max, a++, 
			//    For[b = Ceiling[a/4], (b < a) && ((n = a(4b - a)) <= max), b++, 
			//            C135[n] = C135[n] + 1]];
			//Start of Main 1/21/2009 8:28:13 AM
			//2
			//10
			//4989
			//End of Main (00:00:00.6563170) 1/21/2009 8:28:14 AM
			for(ulong a = 2; a<= MAX_N; a++)
			{
				ulong n = 0;
				for(ulong b= (ulong)Math.Ceiling((decimal)a/4.0m) ; (b<a) && ((n = a * (4*b-a) )<= MAX_N) ; b++)
				{
					solutions[n]++;
				}
			}


			//Start of Main 1/20/2009 4:39:26 PM
			//2
			//10
			//4989
			//End of Main (00:00:01.4189720) 1/20/2009 4:39:28 PM
			//ulong MAX_X = MAX_N * 3 / 2;
			//for (ulong x = MAX_X; x >= 3; x--)
			//{
			//    ulong MAX_PROGRESSION = (ulong)Math.Floor((x - 1) / 2.0);
			//    ulong MIN_PROGRESSION = (ulong)Math.Floor(x / 5.0);
			//    for (ulong p = MIN_PROGRESSION; p <= MAX_PROGRESSION; p++)
			//    {
			//        double num = checked(6 * (double)x * p) - checked(5 * (double)p * p) - checked((double)x * x) ;
			//        if (num > MAX_N) break;
			//        if (num > 0)
			//        {
			//            solutions[(ulong)num]++;
			//        }
			//    }
			//}

			//Start of Main 1/20/2009 4:27:08 PM
			//2
			//10
			//4989
			//End of Main (00:00:02.6968362) 1/20/2009 4:27:10 PM
			//for (ulong x = MAX_X; x >= 3; x--)
			//{
			//    ulong MAX_PROGRESSION = (ulong)Math.Floor((x - 1) / 2.0);
			//    ulong MIN_PROGRESSION = (ulong)Math.Floor(x / 5.0);
			//    for (ulong progression = MIN_PROGRESSION; progression <= MAX_PROGRESSION; progression++)
			//    {
			//        ulong y = x - progression;
			//        ulong z = y - progression;
			//        double num = checked((double)x * x) - checked((double)y * y) - checked((double)z * z);
			//        if (num > MAX_N) break;
			//        if (num > 0 )
			//        {
			//            solutions[(ulong) num]++;
			//        }
			//    }
			//}
			ulong maxSolution = 0;
			for (ulong n = 1; n < MAX_N; n++)
				if (solutions[n] > maxSolution) maxSolution = solutions[n];


			ulong[] distinctCounts = new ulong[maxSolution + 1];
			for (ulong n = 1; n < MAX_N; n++)
			{
				distinctCounts[ solutions[n] ]++;
			}
			Console.WriteLine(solutions[27]);
			Console.WriteLine(solutions[1155]);
			Console.WriteLine(distinctCounts[10]);

		}
	}
}
