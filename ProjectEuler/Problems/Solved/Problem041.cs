using System;
using ProjectEuler.Helpers;

namespace ProjectEuler.Problems
{
	class Problem041
	{
		// We shall say that an n-digit number is pandigital if it makes use of all the digits 
		// 1 to n exactly once. For example, 2143 is d 4-digit pandigital and is also prime.

		// What is the largest n-digit pandigital prime that exists?


		//Start of Main 3/4/2008 8:35:01 AM
		//Max pandigital prime is: 7652413
		//End of Main (00:00:01.1736609) 3/4/2008 8:35:02 AM


		public static void Solve()
		{
			int max = 0;
			for (int numDigits = 4; numDigits < 10; numDigits++)
			{
				Pandigital pan = new Pandigital( numDigits );
				do
				{
					int current = Int32.Parse( pan.Current );
					if (MyMath.IsPrime(current))
					{
						max = current;
						Console.WriteLine(max);
					}
				} while (pan.MoveNext());
			}
			Console.WriteLine("Max pandigital prime is: {0}", max);
			
		}
	}
}
