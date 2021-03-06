using System;
using System.Collections.Generic;
using System.Text;
using ProjectEuler.Helpers;

namespace ProjectEuler.Problems
{
	class Problem223
	{

		/*
			Let us call an integer sided triangle with sides a ≤ b ≤ c barely 
			acute if the sides satisfy
					 
			a^2 + b^2 = c^2 + 1.

			How many barely acute triangles are there with perimeter ≤ 25,000,000?
		*/
		public const int MAX = 25000000;
		public static void Solve()
		{
			Console.WriteLine("Problem 223");
			ulong count = 1; // 1, 1, 1 = perimeter 3 is barely acute.
			ulong a2, b2, a2b2;
			
			//for (int a = MAX; a > 3; a--)
			for (ulong a = 1; a <= MAX; a++)
			{
				ulong bmax = MAX - a;
				a2 = checked(a * a);
				for (ulong b = a; b <= bmax; b++)
				{
					b2 = checked(b * b);
					a2b2 = checked(a2 + b2);

					double c = Math.Sqrt(a2b2 - 1);
					if(MiscFunctions.IsInteger(c))
					{
						if(a+b+c <= MAX)
						{
							if (b <= c)
							{
								count++;
								//if(count%100==0)
								//	Console.WriteLine("{0,11}: {1,9} {2,9} {3,9}", count, a, b, c);
							}
						}
					}
				}
			}
			Console.WriteLine(count);
		}

		public static bool IsTriangleBarelyAcute(int a2, int b2, int c2)
		{
			return ((a2 + b2) == (c2 + 1));
		}
	}
}
