using System;
using ProjectEuler.Helpers;

namespace ProjectEuler.Problems
{

	//The number 3797 has an interesting property. Being prime itself, it is 
	//possible to continuously remove digits from left to right, and remain 
	//prime at each stage: 3797, 797, 97, and 7. Similarly we can work from 
	//right to left: 3797, 379, 37, and 3.

	//Find the sum of the only eleven primes that are both truncatable from 
	//left to right and right to left.

	//NOTE: 2, 3, 5, and 7 are not considered to be truncatable primes.


	//Start of Main 3/3/2008 3:35:13 PM
	//23
	//37
	//53
	//73
	//313
	//317
	//373
	//797
	//3137
	//3797
	//739397
	//Sum: 748317
	//End of Main (00:00:17.2781812) 3/3/2008 3:35:31 PM



	class Problem037
	{


		public static void Solve()
		{

			int sum = 0;
			int countFound = 0;
			int thisVal = 7;
			do
			{
				MyMath.GetNextPrime(ref thisVal);
				if (TruncateTheLeftChecks(thisVal) && TruncateTheRightChecks(thisVal))
				{
					Console.WriteLine(thisVal);
					sum += thisVal;
					countFound++;
				}
			} while (countFound<11);
			Console.WriteLine("Sum: {0}",sum);
			
		}

		private static bool TruncateTheLeftChecks(int val)
		{
			bool isLeft = false;
			if (MyMath.IsPrime(val))
			{
				if (val < 10)
				{
					isLeft = true;
				}
				else
				{
					string strVal = val.ToString();

					int left = Int32.Parse( strVal.Substring( 1 ) );
					isLeft = TruncateTheLeftChecks(left);
				}
			}
			else
			{
				isLeft = false;
			}
			return isLeft;
		}

		private static bool TruncateTheRightChecks(int val)
		{
			bool isRight = false;
			if (MyMath.IsPrime(val))
			{
				if (val < 10)
				{
					isRight = true;
				}
				else
				{
					string strVal = val.ToString();
					int right = Int32.Parse( strVal.Substring( 0, strVal.Length - 1 ) );
					isRight = TruncateTheRightChecks(right);
				}
			}
			else
			{
				isRight = false;
			}
			return isRight;
		}

	}
}
