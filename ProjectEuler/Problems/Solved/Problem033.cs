using System;
using ProjectEuler.Helpers;

namespace ProjectEuler.Problems
{
	class Problem033
	{
		//The fraction 49/98 is d curious fraction, as an inexperienced mathematician 
		//in attempting to simplify it may incorrectly believe that 49/98 = 4/8, which 
		//is correct, is obtained by cancelling the 9s.

		//We shall consider fractions like, 30/50 = 3/5, to be trivial examples.

		//There are exactly four non-trivial examples of this type of fraction, less 
		//than one in value, and containing two digits in the numerator and denominator.

		//If the product of these four fractions is given in its lowest common terms, 
		//find the value of the denominator.


		//Start of Main 3/4/2008 10:52:41 AM
		//16/64 = 0.25     1/4= 0.25
		//19/95 = 0.2      1/5= 0.2
		//26/65 = 0.4      2/5= 0.4
		//49/98 = 0.5      4/8= 0.5
		//387296/38729600
		//1/100
		// 1/100 = 0.01
		//End of Main (00:00:00.0100735) 3/4/2008 10:52:41 AM

		public static void Solve()
		{
			int prodNumer = 1;
			int prodDenom = 1;
			for (int numerator = 10; numerator < 99; numerator++)
			{
				
				for ( int denominator = numerator+1; denominator < 100; denominator++ )
				{
					if (denominator % 11 == 0 && numerator % 11 == 0) continue;

					char? common = FindCommonDigit(numerator, denominator);
					if (common == null) continue;

					decimal fraction1 = numerator / (decimal)denominator;
					int newNumer = RemoveCommonDigit(numerator, common);
					int newDenom = RemoveCommonDigit(denominator, common);
					if(newDenom==0) continue;
					decimal fraction2 = newNumer / (decimal)newDenom;
					if (fraction1 == fraction2)
					{
						prodNumer *= numerator;
						prodDenom *= denominator;
						Console.WriteLine("{0}/{1} = {2} \t {3}/{4}= {5}", numerator, denominator, fraction1, newNumer, newDenom, fraction2);
					}
				}

			}

			MyMath.ReduceFraction(ref prodNumer, ref prodDenom);

			Console.WriteLine(" {0}/{1} = {2}", prodNumer, prodDenom, prodNumer / (decimal)prodDenom);
		}

		private static bool HasCommonDigits(int numerator, int denominator)
		{
			string num = numerator.ToString();
			foreach (char c in num.ToCharArray())
			{
				if (ContainsDigit(denominator, c))
				{
					if (c != '0')
					{
						return true;
					}
				}
			}
			return false;
		}

		private static char? FindCommonDigit(int numerator, int denominator)
		{
			string num = numerator.ToString();
			foreach (char c in num.ToCharArray())
			{
				if (c != '0')
				{
					if ( ContainsDigit( denominator, c ) )
					{
						return c;
					}
				}
			}
			return null;
		}

		private static int RemoveCommonDigit(int either, char? commonDigit)
		{
			if (either % 11 == 0)
				return Int32.Parse(commonDigit.ToString());
			else
				return Int32.Parse(either.ToString().Replace(commonDigit.ToString(), ""));
		}

		private static int RemoveDigit(int number, char? digit)
		{
			return Int32.Parse(number.ToString().Replace(digit.ToString(), ""));
		}

		private static bool ContainsDigit(int number, char digit)
		{
			return number.ToString().Contains( digit.ToString( ));
		}
	}
}
