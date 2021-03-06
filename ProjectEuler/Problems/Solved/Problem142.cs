using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using ProjectEuler.Helpers;

namespace ProjectEuler.Problems
{
	class Problem142
	{
		// Find the smallest x + y + z with integers x > y > z > 0 
		// such that x + y, x − y, x + z, x − z, y + z, y − z are all perfect squares.


		//Start of Main 3/24/2008 8:25:27 AM
		//did 300000-310000 in 00:02:44.7533932
		//did 310000-320000 in 00:02:49.1621488
		//did 320000-330000 in 00:02:54.3592291
		//did 330000-340000 in 00:02:59.5355406
		//did 340000-350000 in 00:03:04.9656267
		//did 350000-360000 in 00:03:10.0885339
		//did 360000-370000 in 00:03:15.5449070
		//did 370000-380000 in 00:03:20.6555825
		//did 380000-390000 in 00:03:25.9925664
		//did 390000-400000 in 00:03:31.4701593
		//did 400000-410000 in 00:03:36.4653786
		//733025 488000 418304  == 1,639,329
		//did 410000-420000 in 00:03:05.2320594

		//Start of Main 3/24/2008 10:20:53 AM
		//..........
		//FOUND! 434657 420968 150568== 1006193


		public static void Solve()
		{
			int step = 1639329;
			bool found = false;
			int min = 0;
			int max = step;
			int oldMinSum = 1639329;
			Stopwatch sw = new Stopwatch();
			while (min < oldMinSum)
			{
				sw.Reset();
				sw.Start();

				for (int z = min; (z < max); z++)
				{
					if(z % (step/100)==0)
						Console.Write(".");
					for (int y = z + 1; (y < z + max); y++)
					{
						if (z + y > oldMinSum)
							break;
						if (!MiscFunctions.IsInteger(Math.Sqrt(y + z)))
							continue;
						if (!MiscFunctions.IsInteger(Math.Sqrt(y - z)))
							continue;
						for (int x = y + 1; (x < y + max); x++)
						{
							if (z + y + x > oldMinSum)
								break;
							if (!MiscFunctions.IsInteger(Math.Sqrt(x + y)))
								continue;
							if (!MiscFunctions.IsInteger(Math.Sqrt(x - y)))
								continue;
							if (!MiscFunctions.IsInteger(Math.Sqrt(x + z)))
								continue;
							if (!MiscFunctions.IsInteger(Math.Sqrt(x - z)))
								continue;
							//found = true;
							int thisSum = x + y + z;
							Console.WriteLine();
							Console.WriteLine("FOUND! {0} {1} {2}== {3}", x, y, z,thisSum);
							if(thisSum<oldMinSum)
							{
								oldMinSum = thisSum;
							}
						}
					}
				}
				Console.WriteLine();
				Console.WriteLine("did {0}-{1} in {2}", min, max, sw.Elapsed);
				min += step;
				max += step;

			}


			//Stopwatch sw = new Stopwatch();
			//sw.Start();
			//int steps = 10000;
			//int MaxRange = steps;
			//int zStart = 0;
			//bool found = false;
			//while (!found)
			//{
			//    for (int z = zStart; !found && (z < MaxRange); z++)
			//    {
			//        for (int y = z + 1; !found && (y < z + MaxRange); y++)
			//        {
			//            for (int x = y + 1; !found && (x < y + MaxRange); x++)
			//            {
			//                if (MiscFunctions.IsInteger(Math.Sqrt(x + y))
			//                    && MiscFunctions.IsInteger(Math.Sqrt(x - y))
			//                    && MiscFunctions.IsInteger(Math.Sqrt(x + z))
			//                    && MiscFunctions.IsInteger(Math.Sqrt(x - z))
			//                    && MiscFunctions.IsInteger(Math.Sqrt(y + z))
			//                    && MiscFunctions.IsInteger(Math.Sqrt(y - z)))
			//                {
			//                    found = true;
			//                    Console.WriteLine("{0} {1} {2}", x, y, z);
			//                }
			//            }
			//        }
			//    }
			//    zStart += steps;
			//    MaxRange += steps;
			//    Console.WriteLine("At {2} New Maxes {0} {1} ", zStart, MaxRange, sw.Elapsed);
			//}
		}
	}
}
