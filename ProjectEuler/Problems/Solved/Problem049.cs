using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectEuler.Problems
{
	class Problem049
	{

		//The arithmetic sequence, 1487, 4817, 8147, in which each of the terms increases by 3330, 
		//is unusual in two ways: (i) each of the three terms are prime, and, (ii) each of the 4-digit 
		//numbers are permutations of one another.

		//There are no arithmetic sequences made up of three 1-, 2-, or 3-digit primes, exhibiting 
		//this property, but there is one other 4-digit increasing sequence.

		//What 12-digit number do you form by concatenating the three terms in this sequence?


		//Starting at 3/5/2008 4:45:18 PM
		//148748178147
		//296962999629
		//done.
		//End of main (00:00:03.2331665) 3/5/2008 4:45:22 PM

		public static void Solve()
		{
			// Problem049
			int primeBase = 999;

			while (primeBase <= 9999)
			{
				GetNextPrime(ref primeBase);
				for (int step = 2; step < 3333; step++)
				{
					if (IsPrime(primeBase + step) && IsPrime(primeBase + step + step))
					{
						//check to see if contains same digits
						string baseStr = GetCountDigits(primeBase);
						string nextStr = GetCountDigits(primeBase + step);
						string lastStr = GetCountDigits(primeBase + step + step);
						if (baseStr == nextStr && baseStr == lastStr)
						{
							Console.WriteLine("{0}{1}{2}", primeBase, primeBase + step, primeBase + step + step);
						}
					}
				}

			}

			Console.WriteLine("done.");

		}


		static string GetCountDigits(int num)
		{
			StringBuilder sb = new StringBuilder();
			string strNum = num.ToString();
			int[] digitCount = new int[10];

			for (int i = 0; i < strNum.Length; i++)
			{
				digitCount[Int32.Parse(strNum[i].ToString())]++;
			}
			for (int i = 0; i < 10; i++)
			{
				sb.AppendFormat("{0,2}", digitCount[i]);
			}

			return sb.ToString();
		}


		public static bool IsPrime(int number)
		{
			number = Math.Abs(number);
			if (number <= 1)
				return false;
			if (number <= 3)
				return true;
			double max = Math.Sqrt(number);
			for (double den = 2; den <= max; den++)
			{
				if (IsInteger(number / den))
				{
					return false;
				}
			}
			return true;

		}

		public static bool IsInteger(double num)
		{
			return (num == (int)num);
		}

		public static void GetNextPrime(ref int number)
		{
			do { } while (!IsPrime(++number));
		}


	}
}
