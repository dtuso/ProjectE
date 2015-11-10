using System;
using System.Collections.Generic;
using System.Text;
using ProjectEuler.Helpers;

namespace ProjectEuler.Problems
{
	class Problem035
	{

		//The number, 197, is called d circular prime because all rotations of the 
		//digits: 197, 971, and 719, are themselves prime.

		//There are thirteen such primes below 100: 2, 3, 5, 7, 11, 13, 17, 31, 
		//37, 71, 73, 79, and 97.

		//How many circular primes are there below one million?
		
		
		//Start of Main 3/3/2008 3:56:55 PM
		//2
		//3
		//5
		//7
		//11
		//13
		//17
		//31
		//37
		//71
		//73
		//79
		//97
		//113
		//131
		//197
		//199
		//311
		//337
		//373
		//719
		//733
		//919
		//971
		//991
		//1193
		//1931
		//3119
		//3779
		//7793
		//7937
		//9311
		//9377
		//11939
		//19391
		//19937
		//37199
		//39119
		//71993
		//91193
		//93719
		//93911
		//99371
		//193939
		//199933
		//319993
		//331999
		//391939
		//393919
		//919393
		//933199
		//939193
		//939391
		//993319
		//999331
		//Count under 1000000 = 55
		//End of Main (00:00:28.3947364) 3/3/2008 3:57:23 PM


		private static int cntCycles;
		public static void Solve()
		{

			int maxVal = 1000000;
			int countFound=0;
			int thisVal = 2; //first prime number
			do
			{
				cntCycles = 0;
				if (CycleChecks(thisVal, thisVal.ToString( )))
				{
					Console.WriteLine(thisVal);
					countFound++;
				}
				MyMath.GetNextPrime(ref thisVal);
			} while (thisVal < maxVal);
			Console.WriteLine("Count under {0} = {1}", maxVal, countFound);

		}

		private static bool CycleChecks(int originalVal, string thisCycleString)
		{

			bool isCycle = false;
			cntCycles++;
			if (MyMath.IsPrime(Int32.Parse(thisCycleString)))
			{
				if (originalVal.ToString().Length == cntCycles)
				{
					isCycle = true;
				}
				else
				{
					string newCycleString = thisCycleString.Substring(1, thisCycleString.Length - 1) + thisCycleString.Substring(0, 1);

					isCycle = CycleChecks(originalVal, newCycleString);
				}
			}
			else
			{
				isCycle = false;
			}

			return isCycle;
		}

	}
}
