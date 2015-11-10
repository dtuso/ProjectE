using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectEuler.Problems.Solved
{
	class Problem100
	{
		// If a box contains twenty-one coloured discs, composed of fifteen blue discs and six red discs, 
		// and two discs were taken at random, it can be seen that the probability of taking two blue discs, 
		// P(BB) = (15/21)×(14/20) = 1/2.

		// The next such arrangement, for which there is exactly 50% chance of taking two blue discs at 
		// random, is a box containing eighty-five blue discs and thirty-five red discs.

		// By finding the first arrangement to contain over 10^(12) = 1,000,000,000,000 discs in total, 
		// determine the number of blue discs that the box would contain.

		static decimal GetBBB(decimal blue)
		{
			return checked(2*checked(blue*blue - blue));
		}
		static decimal GetDDD(decimal discs)
		{
			return checked(discs * discs - discs);
		}
		public static void Solve()
		{
			//    b        b-1        1
			// ------  X  -----  =  -----
			//    d        d-1        2

			// 2(b)(b-1) = d(d-1)
			// 2(bb - b) = dd - d

			// 2bb - 2b = dd - d
			// 2*15*15 - 2*15 = 21*21 - 21
			// 450 - 30 = 441 - 21
			// 420 = 420

			// 2bb - 2b = (dd - d) 

			// trying to go in lock step increment d, then raise b until matches or goes over (lower b by 1, then raise d).

			//Starting at 1/12/2009 3:52:23 PM
			//Problem 100
			//FOUND!!!         Discs: 1070379110497 Blue: 756872327473


			Console.WriteLine("Problem 100");

			//var discs = new BigInteger("1000000000000", 10);
			decimal discs = 1000000000000;
			decimal blue = checked( discs * 707106781175 / 1000000000000 );
			decimal bbb2, ddd;
			bbb2 = GetBBB(blue);
			while (true)
			{
				ddd = GetDDD(discs);
				while (bbb2 >= ddd)
				{
					blue--;
					bbb2 = GetBBB(blue);
				}
				do
				{
					blue++;
					bbb2 = GetBBB(blue);

					if (bbb2 == ddd)
					{
						Console.WriteLine();
						Console.WriteLine();
						Console.WriteLine("FOUND!!! \t Discs: {0} Blue: {1}", discs, blue);
						Console.WriteLine();
						return;
					}


				} while (bbb2 < ddd);
				discs++;
			}

			//Console.WriteLine("Problem 100");

			////var discs = new BigInteger("1000000000000", 10);
			//BigInteger discs = new BigInteger("1000000000000", 10);
			//BigInteger blue = discs * 707106781175/1000000000000;
			//BigInteger bbb2, ddd;
			//bbb2 = 2 * (blue * blue - blue);
			//while(true)
			//{
			//    ddd = discs*discs - discs;
			//    while (bbb2 >= ddd)
			//    {
			//        blue--;
			//        bbb2 = 2 * (blue * blue - blue);
			//    }
			//    do
			//    {
			//        blue++;
			//        bbb2 = 2 * (blue * blue - blue);

			//        if(bbb2==ddd)
			//        {
			//            Console.WriteLine();
			//            Console.WriteLine();
			//            Console.WriteLine("FOUND!!! \t Discs: {0} Blue: {1}", discs, blue);
			//            Console.WriteLine();
			//            break;
			//        }


			//    } while (bbb2 < ddd);
			//    discs++;
			//}

		}
		/*
		
		public static void Solve()
		{
			//    b        b-1        1
			// ------  X  -----  =  -----
			//    d        d-1        2

			// 2(b)(b-1) = d(d-1)
			// 2(bb - b) = dd - d

			// 2bb - 2b = dd - d
			// 2*15*15 - 2*15 = 21*21 - 21
			// 450 - 30 = 441 - 21
			// 420 = 420

			// 2bb - 2b - (dd - d) = 0


			Console.WriteLine("Problem 100");

			var discs = new BigInteger("1000000000000", 10);
			BigInteger bbb, ddd, blue;
			while (true)
			{
				blue = SolveForBlueCount(discs);
				bbb = blue * blue - blue;
				ddd = discs * discs - discs;
				if (2 * bbb == ddd)
					break;
				discs ++;
			}
			Console.WriteLine("blue: {0}", blue);
			Console.WriteLine("discs: {0}", discs);
			Console.WriteLine("2 * bbb: {0}", 2 * bbb);
			Console.WriteLine("    ddd: {0}", ddd);



		}
		public static BigInteger SolveForBlueCount(BigInteger discs)
		{
			BigInteger c = discs * discs - discs;
			BigInteger c8 = 8 * c;
			BigInteger c8p4 = c8 + 4;
			BigInteger sqrt = c8p4.sqrt();
			return ((2 + sqrt) / 4);
		}

		*/


		//public static void Solve()
		//{
		//    //    b        b-1        1
		//    // ------  X  -----  =  -----
		//    //    d        d-1        2

		//    // 2(b)(b-1) = d(d-1)
		//    // 2(bb - b) = dd - d

		//    // 2bb - 2b = dd - d
		//    // 2*15*15 - 2*15 = 21*21 - 21
		//    // 450 - 30 = 441 - 21
		//    // 420 = 420

		//    // 2bb - 2b - (dd - d) = 0


		//    Console.WriteLine("Problem 100");

		//    //decimal discs = Convert.ToDecimal("1000000000000");
		//    //decimal d2 = checked(discs * discs);
		//    double discs = Convert.ToDouble("1000000000000");
		//    decimal bbb, ddd;
		//    double blue;
		//    while(true)
		//    {
		//        blue = SolveForBlueCount(discs);
		//        bbb = checked((decimal)blue * (decimal)blue) - (decimal)blue;
		//        ddd = checked((decimal)discs * (decimal)discs) - (decimal)discs;
		//        if( 2*bbb==ddd)
		//            break;
		//        discs +=2;
		//    }
		//    Console.WriteLine("blue: {0}", blue);
		//    Console.WriteLine("discs: {0}", discs);
		//    Console.WriteLine("2 * bbb: {0}", 2*bbb);
		//    Console.WriteLine("    ddd: {0}", ddd);
			

			
		//}
		//public static double SolveForBlueCount(double discs)
		//{
		//    double c = checked(discs * discs) - discs;
		//    double c8 = checked(8 * c);
		//    double c8p4 = c8 + 4;
		//    double sqrt = Math.Sqrt(c8p4);
		//    return ((2 + sqrt) / 4);
		//}

		//public static void Quadratic(double a, double b, double c, out double x1, out double x2)
		//{
		//    double aa = 2 * a;
		//    double sqrt = Math.Sqrt(b * b - 4 * a * c);
		//    x1 = ((-b + sqrt) / (aa));
		//    x2 = ((-b - sqrt)/(aa));
		//}
	}

}
