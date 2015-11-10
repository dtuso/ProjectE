using System;
using System.Collections.Generic;
using System.Text;
using ProjectEuler.Helpers;

namespace ProjectEuler.Problems
{
	//The binomial coefficients nCk can be arranged in triangular form, Pascal's triangle, like this:
	//    1	
	//    1		1	
	//    1		2		1	
	//    1		3		3		1	
	//    1		4		6		4		1	
	//    1		5		10		10		5		1	
	//    1		6		15		20		15		6		1	
	//1		7		21		35		35		21		7		1
	//.........

	//It can be seen that the first eight rows of Pascal's triangle 
	//contain twelve distinct numbers: 1, 2, 3, 4, 5, 6, 7, 10, 15, 20, 21 and 35.

	//A positive integer n is called squarefree if no square of a prime divides n. 
	//Of the twelve distinct numbers in the first eight rows of Pascal's triangle, 
	//all except 4 and 20 are squarefree. The sum of the distinct squarefree numbers 
	//in the first eight rows is 105.

	//Find the sum of the distinct squarefree numbers in the first 51 rows of Pascal's triangle.

	//Start of Main 9/25/2008 9:22:25 AM
	//highValueLong: 126410606437752
	//highestPrime: 11243248
	//34029210557338 sum of square Free values
	//End of Main (00:00:18.0044228) 9/25/2008 9:22:43 AM



	class P203
	{
		public static int Num_Pascal_Rows = 51;
		public static PascalsTriangle pt;
		public static void Solve()
		{
			pt = new PascalsTriangle(Num_Pascal_Rows);
			//pt.ConsoleWrite();

			List<long> distinctValues = new List<long>();
			BigInteger highValue = 0;
			distinctValues.Add(1);

			//for(int rowIdx=0;rowIdx<numRows;rowIdx++)
			for (int row = 0; row <= Num_Pascal_Rows; row++)
			{
				//for (int colIdx = 1; colIdx <= (rowIdx/2); colIdx++)
				for (int col = 2; (col - 1) <= ((row - 1)/2); col++)
				{
					BigInteger pascal = pt.GetPascalNumber(row, col);
					long thisVal;
					bool converted = Int64.TryParse(pascal.ToString(), out thisVal);
					if (!converted)
					{
						Console.WriteLine("{0} is too big for a Int64", pascal);
						return;
					}

					if (!distinctValues.Contains(thisVal))
					{
						distinctValues.Add(thisVal);
					}


					if (thisVal > highValue)
						highValue = thisVal;
				}
			}

			long highValueLong = 0;
			bool isOK = Int64.TryParse(highValue.ToString(), out highValueLong);
			if (!isOK)
			{
				Console.WriteLine("{0} is too big for a Int64", highValue);
				return;
			}

			Console.WriteLine("highValueLong: {0}", highValueLong);

			long highestPrime = 1 + (long)(Math.Sqrt(highValueLong));
			Console.WriteLine("highestPrime: {0} ", highestPrime);

			List<long> squaredPrimes = new List<long>();
			long prime = 1;
			do
			{
				MyMath.GetNextPrime(ref prime);
				squaredPrimes.Add(prime * prime);
			} while (prime <= highestPrime);


			BigInteger sumSquareFree = 0;

			foreach (long value in distinctValues)
			{

				bool squareFree = true;
				foreach (long psqrd in squaredPrimes)
				{
					if (psqrd > value)
						continue;
					if (value % psqrd == 0)
					{
						squareFree = false;
						break;
					}

				}
				if (squareFree)
				{
					sumSquareFree += value;

				}
			}
			Console.WriteLine("{0} sum of square Free values", sumSquareFree);



		}

		//private static void AddDistinct<T>(T bi, ref List<T> bigs)
		//{
		//    if(!bigs.Contains(bi))
		//    {
		//        bigs.Add(bi);
		//    }
		//}
	}
}
