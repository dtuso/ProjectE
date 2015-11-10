using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectEuler.Problems
{
	class Problem040
	{

		// An irrational decimal fraction is created by concatenating the positive integers:

		// 0.123456789101112131415161718192021...

		// It can be seen that the 12th digit of the fractional part is 1.

		// If dn represents the nth digit of the fractional part, find the value of the following expression.

		// d1 × d10 × d100 × d1000 × d10000 × d100000 × d1000000


		//Start of Main 3/3/2008 4:31:17 PM
		//1
		//1
		//5
		//3
		//7
		//2
		//1
		//prod: 210
		//End of Main (00:00:00.0561892) 3/3/2008 4:31:17 PM

		private static int[] decimalfraction;
		private static char[] charDecimalfraction;
		private static int currentPosition = 0;
		public static void Solve()
		{
			LoadDecimalFraction();
			int prod = 1;
			for (int i = 1; i < charDecimalfraction.Length; i *= 10)
			{
				Console.WriteLine(charDecimalfraction[i-1]);
				prod *= Int32.Parse(charDecimalfraction[i - 1].ToString());

			}

			Console.WriteLine("prod: {0}", prod);

		}

		private static void LoadDecimalFraction()
		{
			StringBuilder sb = new StringBuilder( );
			
			for (int i = 1; i < 185186; i++)
				sb.Append(i);
			charDecimalfraction = sb.ToString().ToCharArray();
			return;

			int num = 1;
			while(StuffThisInt(num++))
			{
			}

		}
		private static bool StuffThisInt(int num)
		{
			string str = num.ToString();
			int thisIndex = 0;
			while (thisIndex<str.Length) 
			{
				if ((currentPosition) < 1000001)
				{
					decimalfraction[currentPosition] = Int32.Parse( str.Substring( thisIndex, 1 ) );
					currentPosition++;
				}
				thisIndex++;
			}
			return (currentPosition < 1000000);
		}
	}
}
