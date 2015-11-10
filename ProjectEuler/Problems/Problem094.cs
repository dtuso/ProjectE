using System;
using System.Collections.Generic;
using System.Text;
using ProjectEuler.Helpers;


/* http://projecteuler.net/index.php?section=forum&id=100 */
/* http://projecteuler.net/index.php?section=forum&id=100 */
/* http://projecteuler.net/index.php?section=forum&id=100 */
/* http://projecteuler.net/index.php?section=forum&id=100 */
/* http://projecteuler.net/index.php?section=forum&id=100 */
/* http://projecteuler.net/index.php?section=forum&id=100 */
/* http://projecteuler.net/index.php?section=forum&id=100 */
/* http://projecteuler.net/index.php?section=forum&id=100 */
/* http://projecteuler.net/index.php?section=forum&id=100 */

/* aka Pythagorean triples (see http://en.wikipedia.org/wiki/Pell_number) */


namespace ProjectEuler.Problems
{
	class Problem094
	{
		//It is easily proved that no equilateral triangle exists with integral length sides and integral area. 
		//However, the almost equilateral triangle 5-5-6 has an area of 12 square units.

		//We shall define an almost equilateral triangle to be a triangle for which two sides are equal and the 
		//third differs by no more than one unit.

		//Find the sum of the perimeters of every almost equilateral triangle with integral side lengths and area 
		//and whose perimeters do not exceed one billion (1,000,000,000).

		//perimeter = (s1 + s2 + s3)
		//perimeter = 2s + (s[+-]1)


		//Start of Main 4/2/2008 7:45:40 AM
		// 329874525  329874524   989623574
		// 311709883  311709884   935129650
		// 286896519  286896518   860689556
		// 253000835  253000836   759002506
		// 194291787  194291788   582875362
		// 185209465  185209464   555628394
		// 126500417  126500416   379501250
		//  92604733   92604734   277814200
		//  33895685   33895686   101687056
		//   9082321    9082320    27246962
		//   2433601    2433602     7300804
		//    652081     652080     1956242
		//    174725     174726      524176
		//     46817      46816      140450
		//     12545      12546       37636
		//      3361       3360       10082
		//       901        902        2704
		//       241        240         722
		//        65         66         196
		//        17         16          50
		//         5          6          16
		//5479171588
		//End of Main (00:00:48.3999611) 4/2/2008 7:46:28 AM

		//Start of Main 10/6/2008 2:22:21 PM
		//3589376509122
		//End of Main (00:03:43.9232576) 10/6/2008 2:26:05 PM


		static ulong maxPerimeter = 1000000000;
		public static void Solve()
		{
			ulong sum = 0;
			// -=2 beause odd perimiter lengths mean fractional areas
			for (ulong s = maxPerimeter/3; s >= 3; s-=2)
			{
				sum = checked(sum + PerimiterOfIntegralArea(s, s + 1));
				sum = checked(sum + PerimiterOfIntegralArea(s, s - 1));
			}
			Console.WriteLine(sum);
		}

		static ulong PerimiterOfIntegralArea(ulong twinsLen, ulong soloLen)
		{
			ulong perimeter = checked(twinsLen + twinsLen + soloLen);
			ulong halfPer = perimeter / 2;

			/* don't need the two below becuase the sqrt of x^2 is x and x is already an integer*/
			ulong inside = checked(halfPer * (halfPer - soloLen));
			double quasiArea = Math.Sqrt(inside);
			decimal area = checked((decimal)quasiArea * (halfPer - twinsLen));
			if ((ulong) area == area)
			{
				//Console.WriteLine("{0,10} {1,10} {2,11} {3}", twinsLen, soloLen, perimeter, area);
				//Console.WriteLine("{0,10} & {1,10} p={2,10}", sideLen, baseLen, perimeter);
				return perimeter;
			}
			else
			{
				//Console.WriteLine(area);
				return 0;
			}
		}

		public static bool IsInteger(double num)
		{
			return (num == (ulong)num);
		}
		

	}

}
