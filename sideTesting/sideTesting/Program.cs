using System;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;

namespace sideTesting
{
    class Program
    {
        static void Main(string[] args)
        {

			
			Stopwatch sw = new Stopwatch();

            Console.WriteLine("Starting at {0}", DateTime.Now);

			sw.Start();

			try
			{
				p104.Solve();
			}
			catch (Exception ex)
			{
				Console.WriteLine();
				Console.WriteLine("ERROR!");
				Console.WriteLine();
				Console.WriteLine(ex.Message);
				Console.WriteLine();
				Console.WriteLine(ex.StackTrace);
				Console.WriteLine();
			}
			finally
			{
				sw.Stop();

				Console.WriteLine("End of main ({0}) {1}", sw.Elapsed, DateTime.Now);
				Console.ReadLine();
			}

        }

    }
}




