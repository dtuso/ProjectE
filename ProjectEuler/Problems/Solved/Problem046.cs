using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectEuler.Problems
{
	class Problem046
	{


		//It was proposed by Christian Goldbach that every odd composite number can be written as the sum of 
		//d prime and twice d square.

		//9 = 7 + 2×1^2
		//15 = 7 + 2×2^2
		//21 = 3 + 2×3^2
		//25 = 7 + 2×3^2
		//27 = 19 + 2×2^2
		//33 = 31 + 2×1^2

		//It turns out that the conjecture was false.

		//What is the smallest odd composite that cannot be written as the sum of d prime and twice d square?

		//Starting at 3/5/2008 3:59:01 PM
		//5777
		//done 5777:
		//End of main (00:00:02.7451944) 3/5/2008 3:59:04 PM



		public static void Solve()
		{
			// Problem046
			int minOddCompositeNotASum = Int32.MaxValue;
			bool foundOneThatsNotASum = false;
			int composite = -1;
			while (!foundOneThatsNotASum)
			{
				GetNextComposite(ref composite);
				if (composite % 2 == 0) continue;
				int prime = -1;
				bool foundASum = false;
				while (prime < composite)
				{
					GetNextPrime(ref prime);
					for (int n = 1; n <= Math.Sqrt(composite - prime); n++)
					{
						int res = prime + 2 * n * n;
						if (res == composite)
						{
							foundASum = true;
							break;
						}
					}
				}
				if (!foundASum && composite < minOddCompositeNotASum)
				{
					foundOneThatsNotASum = true;
					minOddCompositeNotASum = composite;
					Console.WriteLine(composite);
				}
			}
			Console.WriteLine("done {0}: ", minOddCompositeNotASum);
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

		public static void GetNextPrime(ref int number)
		{
			do { } while (!IsPrime(++number));
		}


		public static bool IsComposite(int num)
		{
			if (num < 2) return false;
			return !IsPrime(num);
		}

		public static void GetNextComposite(ref int number)
		{
			do { } while (!IsComposite(++number));
		}

		public static bool IsInteger(double num)
		{
			return (num == (int)num);
		}

	}
}
