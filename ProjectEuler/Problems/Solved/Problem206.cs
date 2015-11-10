using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectEuler.Problems
{

	//Find the unique positive integer whose square has the form 1_2_3_4_5_6_7_8_9_0,
	//where each “_” is a single digit.


	//Start of Main 9/23/2008 4:01:59 PM
	//0
	//1389019170 : 1929374254627488900
	//Done.
	//End of Main (00:01:06.3814750) 9/23/2008 4:03:05 PM


	class P206
	{
		public static void Solve()
		{
			long start = (long)Math.Sqrt(1020304050607080900);
			long rampedUpTo10 = GetNextTen(start);

			long max = 1 + (long)Math.Sqrt(1929394959697989990);
			for (long posInt = start; posInt < max; posInt += 10)
			{
				long sqr = checked(posInt*posInt);
				char[] chr = sqr.ToString().ToCharArray();

				double pctDone = 100 * (double) (posInt - start)/(max - start);
				if (pctDone%1==0)
				{
					Console.WriteLine(pctDone);
				}

				if(chr.Length!=19)
					continue;
				if(
					chr[0]=='1' && 
					chr[2]=='2' && 
					chr[4]=='3' && 
					chr[6]=='4' && 
					chr[8]=='5' && 
					chr[10]=='6' && 
					chr[12]=='7' && 
					chr[14]=='8' && 
					chr[16]=='9' && 
					chr[18]=='0')
				{
					Console.WriteLine("{0} : {1} ",posInt, sqr);
				}
			}
			Console.WriteLine("Done.");
		}

		private static long GetNextTen(long start)
		{
			long result = start;
			while(result%10!=0)
			{
				result++;
			}
			return result;
		}
	}
}
