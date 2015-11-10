using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectEuler.Problems
{
	class Problem125
	{

		//The palindromic number 595 is interesting because it can be written as 
		//the sum of consecutive squares: 6^2 + 7^2 + 8^2 + 9^2 + 10^2 + 11^2 + 122.

		//There are exactly eleven palindromes below one-thousand that can be written 
		//as consecutive square sums, and the sum of these palindromes is 4164. 
		//Nte that 1 = 0^2 + 1^2 has not been included as this problem is concerned 
		//with the squares of positive integers.

		//Find the sum of all the numbers less than 10^8 that are both palindromic 
		//and can be written as the sum of consecutive squares.

		//Start of Main 4/21/2008 1:24:11 PM
		//        5 start: 1 to: 2
		//       55 start: 1 to: 5
		//       77 start: 4 to: 6
		//      181 start: 9 to: 10
		//      313 start: 12 to: 13
		//      434 start: 11 to: 13
		//      505 start: 2 to: 11
		//      545 start: 16 to: 17
		//      595 start: 6 to: 12
		//      636 start: 4 to: 12
		//      818 start: 2 to: 13
		//     1001 start: 4 to: 14
		//     1111 start: 11 to: 16
		//     1441 start: 6 to: 16
		//     1771 start: 4 to: 17
		//     4334 start: 37 to: 39
		//     6446 start: 19 to: 29
		//    17371 start: 9 to: 37
		//    17871 start: 29 to: 42
		//    19691 start: 50 to: 56
		//    21712 start: 19 to: 41
		//    41214 start: 100 to: 103
		//    42924 start: 2 to: 50
		//    44444 start: 71 to: 78
		//    46564 start: 32 to: 55
		//    51015 start: 99 to: 103
		//    65756 start: 54 to: 70
		//    81818 start: 77 to: 88
		//    97679 start: 31 to: 68
		//    99199 start: 22 to: 67
		//   108801 start: 50 to: 76
		//   127721 start: 44 to: 77
		//   137731 start: 149 to: 154
		//   138831 start: 93 to: 106
		//   139931 start: 69 to: 90
		//   148841 start: 11 to: 76
		//   161161 start: 116 to: 126
		//   166661 start: 14 to: 79
		//   171171 start: 104 to: 117
		//   188881 start: 126 to: 136
		//   191191 start: 176 to: 181
		//   363363 start: 27 to: 103
		//   435534 start: 92 to: 127
		//   444444 start: 51 to: 113
		//   485584 start: 18 to: 113
		//   494494 start: 207 to: 217
		//   525525 start: 77 to: 126
		//   554455 start: 9 to: 118
		//   629926 start: 173 to: 191
		//   635536 start: 29 to: 124
		//   646646 start: 85 to: 136
		//   656656 start: 106 to: 146
		//   904409 start: 83 to: 148
		//   923329 start: 151 to: 183
		//   944449 start: 288 to: 298
		//   964469 start: 566 to: 568
		//   972279 start: 257 to: 270
		//   981189 start: 23 to: 143
		//   982289 start: 88 to: 153
		//  1077701 start: 63 to: 151
		//  1224221 start: 173 to: 206
		//  1365631 start: 34 to: 160
		//  1681861 start: 156 to: 206
		//  1690961 start: 919 to: 920
		//  1949491 start: 106 to: 191
		//  1972791 start: 164 to: 217
		//  1992991 start: 1 to: 181
		//  2176712 start: 189 to: 236
		//  2904092 start: 599 to: 606
		//  3015103 start: 27 to: 208
		//  3162613 start: 1257 to: 1258
		//  3187813 start: 1262 to: 1263
		//  3242423 start: 17 to: 213
		//  3628263 start: 102 to: 228
		//  4211124 start: 172 to: 260
		//  4338334 start: 623 to: 633
		//  4424244 start: 128 to: 248
		//  4776774 start: 101 to: 248
		//  5090905 start: 709 to: 718
		//  5258525 start: 1621 to: 1622
		//  5276725 start: 210 to: 292
		//  5367635 start: 73 to: 254
		//  5479745 start: 36 to: 254
		//  5536355 start: 415 to: 444
		//  5588855 start: 226 to: 304
		//  5603065 start: 261 to: 325
		//  5718175 start: 146 to: 272
		//  5824285 start: 1706 to: 1707
		//  6106016 start: 74 to: 265
		//  6277726 start: 46 to: 266
		//  6523256 start: 631 to: 646
		//  6546456 start: 54 to: 270
		//  6780876 start: 864 to: 872
		//  6831386 start: 749 to: 760
		//  6843486 start: 54 to: 274
		//  6844486 start: 159 to: 290
		//  7355537 start: 224 to: 321
		//  8424248 start: 22 to: 293
		//  9051509 start: 1736 to: 1738
		//  9072709 start: 682 to: 700
		//  9105019 start: 72 to: 302
		//  9313139 start: 26 to: 303
		//  9334339 start: 477 to: 514
		//  9343439 start: 102 to: 307
		//  9435349 start: 167 to: 320
		//  9563659 start: 820 to: 833
		//  9793979 start: 482 to: 520
		//  9814189 start: 172 to: 325
		//  9838389 start: 223 to: 343
		//  9940499 start: 136 to: 318
		// 10711701 start: 491 to: 531
		// 11122111 start: 1359 to: 1364
		// 11600611 start: 16 to: 326
		// 11922911 start: 160 to: 341
		// 12888821 start: 409 to: 474
		// 13922931 start: 785 to: 806
		// 15822851 start: 479 to: 539
		// 16399361 start: 1216 to: 1226
		// 16755761 start: 471 to: 536
		// 16955961 start: 226 to: 396
		// 17488471 start: 215 to: 396
		// 18244281 start: 93 to: 381
		// 18422481 start: 589 to: 637
		// 18699681 start: 158 to: 391
		// 26744762 start: 304 to: 476
		// 32344323 start: 1202 to: 1223
		// 32611623 start: 1207 to: 1228
		// 34277243 start: 7 to: 468
		// 37533573 start: 197 to: 493
		// 40211204 start: 632 to: 719
		// 41577514 start: 742 to: 810
		// 43699634 start: 975 to: 1018
		// 44366344 start: 412 to: 587
		// 45555554 start: 182 to: 522
		// 45755754 start: 192 to: 524
		// 46433464 start: 46 to: 518
		// 47622674 start: 261 to: 543
		// 49066094 start: 2107 to: 2117
		// 50244205 start: 275 to: 555
		// 51488415 start: 3207 to: 3211
		// 52155125 start: 83 to: 539
		// 52344325 start: 292 to: 566
		// 52722725 start: 341 to: 582
		// 53166135 start: 315 to: 575
		// 53211235 start: 793 to: 869
		// 53933935 start: 645 to: 754
		// 55344355 start: 976 to: 1030
		// 56722765 start: 239 to: 568
		// 56800865 start: 895 to: 960
		// 57488475 start: 1606 to: 1627
		// 58366385 start: 421 to: 629
		// 62988926 start: 1075 to: 1126
		// 63844836 start: 41 to: 576
		// 63866836 start: 8 to: 576
		// 64633646 start: 2419 to: 2429
		// 66999966 start: 539 to: 709
		// 67233276 start: 320 to: 616
		// 68688686 start: 194 to: 597
		// 69388396 start: 429 to: 659
		// 69722796 start: 1284 to: 1324
		// 69933996 start: 331 to: 626
		// 72299227 start: 924 to: 1001
		// 92800829 start: 577 to: 777
		// 95177159 start: 21 to: 658
		// 95544559 start: 3988 to: 3993
		// 97299279 start: 312 to: 685
		//sum:   2906969179 pals: 19997 sqrs 166
		//End of Main (00:00:46.3829652) 4/21/2008 1:24:58 PM



		public static void Solve()
		{
			const int MAX = 100000000;
			long sum = 0;
			int numPalidroms = 0;
			int numSqrs = 0;
			for(int n=2; n<MAX;n++)
			{
				
				if(ProjectEuler.Helpers.MiscFunctions.IsPalindrome(n.ToString()))
				{
					numPalidroms++;
					if(IsSumOfSquares(n))
					{
						numSqrs++;
						sum += n;
					}
				}
			}
			Console.WriteLine("sum: {0,12} pals: {1,3} sqrs {2,3}",sum,numPalidroms, numSqrs);
		}

		private static bool IsSumOfSquares(int num)
		{
			int maxStartingPoint = (int) Math.Round(Math.Sqrt(num));
			for(int startingPoint = 1; startingPoint <= maxStartingPoint; startingPoint++)
			{
				int sumSqrs = startingPoint * startingPoint;

				for(int n1 = startingPoint+1;;n1++)
				{
					sumSqrs += (n1*n1);
					if (sumSqrs> num)
						break;
					if (sumSqrs==num)
					{
						Console.WriteLine("{0,9} start: {1} to: {2}", num, startingPoint, n1);
						return true;
					}
				}

			}
			return false;

		}
	}
}
