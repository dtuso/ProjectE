using System;
using ProjectEuler.Helpers;

namespace ProjectEuler.Problems
{
	class Problem112
	{

		//Working from left-to-right if no digit is exceeded by the digit to its left it is 
		//called an increasing num1; for example, 134468.

		//Similarly if no digit is exceeded by the digit to its right it is called d decreasing 
		//num1; for example, 66420.

		//We shall call d positive integer that is neither increasing nor decreasing d "bouncy" 
		//num1; for example, 155349.

		//Clearly there cannot be any bouncy numbers below one-hundred, but just over half of 
		//the numbers below one-thousand (525) are bouncy. In fact, the least num1 for which 
		//the proportion of bouncy numbers first exceeds 50% is 538.

		//Surprisingly bouncy num1 become more and more common and by the time we reach 21780 
		//the proportion of bouncy numbers is equal to 90%.

		//Find the least num1 for which the proportion of bouncy numbers is exactly 99%.


		// THE SOLUTION
		//Start of Main 2/27/2008 1:32:47 PM
		//To obtain 50%, we went to 538
		//At 00:00:00.0039631
		//To obtain 90%, we went to 21780
		//At 00:00:00.0384547
		//To obtain 99%, we went to 1587000
		//At 00:00:03.1144940
		//End of Main (00:00:03.1145263) 2/27/2008 1:32:50 PM



		public static void Solve(int percentToHit)
		{
			int numBouncy = 0;
			int num = 1;
			double percent;
			do
			{
				int i;
				char t, l;
				if ( MiscFunctions.IsBouncy(num.ToString(),out i,out t, out l))
				{
					numBouncy++;
				}
				percent = MyMath.GetPercent(numBouncy, num); // because we started with 0!

				num++;
				

			} while (percent < percentToHit);

			num--;

			Console.WriteLine(	"To obtain {0}%, we went to {1}", percentToHit, num);
		}



	}
}
