using System;
using System.Collections.Generic;
using System.Text;
using ProjectEuler.Helpers;


namespace ProjectEuler.Problems
{

	// The product 7254 is unusual, as the identity, 39 × 186 = 7254, containing multiplicand, multiplier, 
	// and product is 1 through 9 pandigital.

	// Find the sum of all products whose multiplicand/multiplier/product identity can be written as d 1 
	// through 9 pandigital.

	// HINT: Some products can be obtained in more than one way so be sure to only include it once in your sum.



	//Start of Main 3/3/2008 2:09:45 PM
	//12 x 483 = 5796
	//138 x 42 = 5796
	//157 x 28 = 4396
	//159 x 48 = 7632
	//1738 x 4 = 6952
	//18 x 297 = 5346
	//186 x 39 = 7254
	//1963 x 4 = 7852
	//198 x 27 = 5346
	//27 x 198 = 5346
	//28 x 157 = 4396
	//297 x 18 = 5346
	//39 x 186 = 7254
	//4 x 1738 = 6952
	//4 x 1963 = 7852
	//42 x 138 = 5796
	//48 x 159 = 7632
	//483 x 12 = 5796
	//sum: 45228
	//End of Main (00:00:11.8312842) 3/3/2008 2:09:56 PM

	class Problem032
	{

		public static List<int> products = new List<int>();

		public static void Solve()
		{
			Pandigital pan = new Pandigital( 9 );
			
			do
			{
				string thisPan = pan.Current;
				//bool o = Pandigital.IsPandigital(thisPan);
				for(int lenMultiplcand=1;lenMultiplcand<=7;lenMultiplcand++)
				{
					for (int lenmultiplier = 1; lenmultiplier <= 8 - lenMultiplcand; lenmultiplier++)
					{
						CheckForPandigitalMultplier(thisPan, lenMultiplcand, lenmultiplier);
					}
				}
			} while (pan.MoveNext());



			int sum = 0;
			foreach (int prod in products)
			{
				sum += prod;
			}

			Console.WriteLine("sum: {0}", sum);

		}




		private static void CheckForPandigitalMultplier(string thisNumber, int lenMultiplcand, int lenmultiplier)
		{
			string multiplicand, multiplier, product;

			multiplicand = thisNumber.Substring(0, lenMultiplcand);
			multiplier = thisNumber.Substring(lenMultiplcand, lenmultiplier);
			product = thisNumber.Substring(lenMultiplcand + lenmultiplier);
			if (Int32.Parse(multiplicand) * Int32.Parse(multiplier) == Int32.Parse(product))
			{
				Console.WriteLine("{0} x {1} = {2}", multiplicand, multiplier, product);
				AddToListOfProducts(Int32.Parse(product));

			}
		}

		private static void AddToListOfProducts( int prod)
		{
			if(!products.Contains( prod))
				products.Add( prod);
		}


	}
}
