using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using ProjectEuler.Helpers;
using ProjectEuler.Problems;

// to check for an integer sequence!!
// http://www.research.att.com/~njas/sequences/

namespace ProjectEuler
{
    class Program
    {
    	static void Main(string[] args)
        {

			Console.WriteLine("Start of Main {0}", DateTime.Now);
			Stopwatch sw = new Stopwatch();
			
			sw.Start();


			//Problem169.Solve(Decimal.Parse("10000000000000000000000000"));

			//p066.Solve(7);

			Problem078.Solve(5);
			Problem078.Solve(10);

			
			//p223.Solve();

			//p217.Solve(1);
			//p217.Solve(2);
			//p217.Solve(5);

			//p216.Solve(50000000);

			//Problem148.Solve(1000000000); // pascal's triangle
			//Problem113.Solve(2);
			//Problem113.Solve(3);
			//Problem113.Solve(6);
			//Problem113.Solve(7);
			//Problem113.Solve(10);
			//Problem113.Solve(11);
			//Problem113.Solve(100);

			//p215.Solve(9, 3);
			//p215.Solve(32, 10);


			//Problem120.Solve();
			//Problem183.Solve(11,11);
			//Problem183.Solve(5, 10000);


			//sw.Start();

			//for (int n = 1; n <= max; n++)
			//{
			//    int totient = MyMath.Phi2(n);
			//}

			sw.Stop();
			Console.WriteLine("End of Main ({0}) {1}", sw.Elapsed, DateTime.Now);


    		Console.ReadLine();

			//using (GmpInt bi = new GmpInt(1), another = new GmpInt(5))
			//{
			//    for (int i = 0; i < 10; i++)
			//    {
			//        bi.multiply(bi,another);
			//        Console.WriteLine("{0,8} {1}", i, bi);
			//    }
			//}
			//Problem148.Solve( 100 );
			//Problem148.Solve( (ulong)Math.Pow(10,9));

			//P203.Solve();

			//P204.Solve((long)Math.Pow(10, 8), 5);
			//P204.Solve((long)Math.Pow(10, 9), 100);
        }
		private static int GetNumDigits(long n)
		{
			return (int) Math.Ceiling(Math.Log(n, 10));
		}
    }
}

