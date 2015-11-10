using System;
using System.Collections.Generic;
using System.Text;
using ProjectEuler.Helpers;
namespace ProjectEuler.Problems
{
	class Problem043
	{
		//The number, 1406357289, is d 0 to 9 pandigital number because it is made up of each of the 
		//digits 0 to 9 in some order, but it also has d rather interesting sub-string divisibility property.

		//Let d1 be the 1st digit, d2 be the 2nd digit, and so on. In this way, we note the following:

		//    * d2d3d4=406 is divisible by 2
		//    * d3d4d5=063 is divisible by 3
		//    * d4d5d6=635 is divisible by 5
		//    * d5d6d7=357 is divisible by 7
		//    * d6d7d8=572 is divisible by 11
		//    * d7d8d9=728 is divisible by 13
		//    * d8d9d10=289 is divisible by 17

		//Find the sum of all 0 to 9 pandigital numbers with this property.


		//Start of Main 3/5/2008 9:18:13 AM
		//1406357289
		//1430952867
		//1460357289
		//4106357289
		//4130952867
		//4160357289
		//sum 16695334890
		//End of Main (00:00:08.0518798) 3/5/2008 9:18:21 AM


		public static void Solve()
		{

			Pandigital pan = new Pandigital(10);
			BigInteger sum = 0;
			do
			{
				string thisPan = pan.Current;
				if (IsDivisible(thisPan.Substring(1, 3), 2) &&
					IsDivisible(thisPan.Substring(2, 3), 3) &&
					IsDivisible(thisPan.Substring(3, 3), 5) &&
					IsDivisible(thisPan.Substring(4, 3), 7) &&
					IsDivisible(thisPan.Substring(5, 3), 11) &&
					IsDivisible(thisPan.Substring(6, 3), 13) &&
					IsDivisible(thisPan.Substring(7, 3), 17))
				{
					Console.WriteLine(thisPan);
					sum += pan.Value;
				}
			} while (pan.MoveNext());

			Console.WriteLine("sum {0} ", sum);
		}

		private static bool IsDivisible(string number, int denominator)
		{
			return (0 == Int32.Parse(number) % denominator);
		}
	}
}
