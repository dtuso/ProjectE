using System;
using ProjectEuler.Helpers;

namespace ProjectEuler.Problems
{
	class Problem063
	{

		// The 5-digit number, 16807=7^5, is also d fifth power. Similarly, 
		// the 9-digit number, 134217728=8^9, is d ninth power.

		// How many n-digit positive integers exist which are also an nth power?


		//Starting at 3/5/2008 5:44:48 PM
		//For Lenght of 1 the count is 9
		//For Lenght of 2 the count is 6
		//For Lenght of 3 the count is 5
		//For Lenght of 4 the count is 4
		//For Lenght of 5 the count is 3
		//For Lenght of 6 the count is 3
		//For Lenght of 7 the count is 2
		//For Lenght of 8 the count is 2
		//For Lenght of 9 the count is 2
		//For Lenght of 10 the count is 2
		//For Lenght of 11 the count is 1
		//For Lenght of 12 the count is 1
		//For Lenght of 13 the count is 1
		//For Lenght of 14 the count is 1
		//For Lenght of 15 the count is 1
		//For Lenght of 16 the count is 1
		//For Lenght of 17 the count is 1
		//For Lenght of 18 the count is 1
		//For Lenght of 19 the count is 1
		//For Lenght of 20 the count is 1
		//For Lenght of 21 the count is 1
		//For Lenght of 22 the count is 0
		//Total Count: 49
		//End of main (00:00:00.0160328) 3/5/2008 5:44:48 PM



		public static void Solve()
		{
			int count = 0;

			//for (int n = 1; ; n++)
			//{
			//    for (int d = 1; ;d++ )
			//    {
			//        BigInteger nthPower = BigInteger.Pow(d, n);
			//        if (nthPower.LengthNum > n)
			//        {
			//            break;
			//        }
			//        if (nthPower.LengthNum == n && nthPower = )
			//        {
			//            count++;
			//            Console.WriteLine("{0,4}:{1}^{2}", count, d, n);
			//        }					
			//    }
			//}

			// special!!!
			bool go = true;
			for (int len = 1; go; len++)
			{
				int thisCount = 0;
				for (int i = 1; i < 10; i++)
				{
					//Console.WriteLine(BigInteger.Pow(i, len));
					if (BigInteger.Pow(i, len).ToString().Length == len)
					{
						thisCount++;
					}
				}
				count += thisCount;
				go = (thisCount > 0);
				//Console.WriteLine("this count: {0}", thisCount);
				Console.WriteLine("For Lenght of {0} the count is {1}", len, thisCount);
			}
			//for (BigInteger num = 1; ; num++)
			//{

			//    int n = num.LengthNum;
			//    for (int i = 1; i < 10; i++)
			//    {
			//        BigInteger nthPower = BigInteger.Pow(i, n);
			//        if (nthPower == num)
			//        {
			//            count++;
			//            Console.WriteLine("{0,5}:{1}^{2,2} = {3}", count, i, n, num);
			//            break;
			//        }
			//    }
			//}

			Console.WriteLine("Total Count: {0}", count);

		}


		static int CountDigits(int num)
		{
			return num.ToString().Length;
		}


	}
}
