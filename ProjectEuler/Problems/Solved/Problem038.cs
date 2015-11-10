using System;
using System.Collections.Generic;
using System.Text;
using ProjectEuler.Helpers;

namespace ProjectEuler.Problems
{
	class Problem038
	{

		//Take the number 192 and multiply it by each of 1, 2, and 3:

		//    192 × 1 = 192
		//    192 × 2 = 384
		//    192 × 3 = 576

		//By concatenating each product we get the 1 to 9 pandigital, 192384576. We will call 
		//192384576 the concatenated product of 192 and (1,2,3)

		//The same can be achieved by starting with 9 and multiplying by 1, 2, 3, 4, and 5, 
		//giving the pandigital, 918273645, which is the concatenated product of 9 and (1,2,3,4,5).

		//What is the largest 1 to 9 pandigital 9-digit number that can be formed as the 
		//concatenated product of an integer with (1,2, ... , n) where n > 1?


		//Start of Main 3/4/2008 9:11:26 AM
		//using 9327 by (1..2) made 932718654
		//maxPan: 932718654
		//End of Main (00:00:00.1997389) 3/4/2008 9:11:26 AM


		static List<int> pans = new List<int>( );

		public static void Solve()
		{
			int maxPan = 0;

			for(int integ = 1; integ<10000; integ++)
			{
				bool outOfBounds = false;
				for ( int maxN = 2; maxN < 10 && !outOfBounds; maxN++ )
				{
					string concatenated = "";
					for ( int n = 1; n <= maxN; n++ )
					{
						 concatenated += (integ*n).ToString( );
					}
					if (Pandigital.IsPandigital(concatenated))
					{
						int num = Int32.Parse( concatenated);
						pans.Add( num);
						if (num > maxPan)
						{
							maxPan = num;
						}
						Console.WriteLine("using {0} by (1..{1}) made {2}", integ, maxN, concatenated);
					}
				}
			}

			Console.WriteLine("maxPan: {0}", maxPan);

		}

	}
}
