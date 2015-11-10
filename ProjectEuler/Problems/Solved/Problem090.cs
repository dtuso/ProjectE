using System;
using System.Collections.Generic;
using System.Text;
using ProjectEuler.Helpers;

namespace ProjectEuler.Problems
{
	class p090
	{

		// Each of the six faces on a cube has a different digit (0 to 9) written on it; the same 
		// is done to a second cube. By placing the two cubes side-by-side in different positions 
		// we can form a variety of 2-digit numbers.

		// For example, the square number 64 could be formed:

		// In fact, by carefully choosing the digits on both cubes it is possible to display 
		// all of the square numbers below one-hundred: 01, 04, 09, 16, 25, 36, 49, 64, and 81.

		// For example, one way this can be achieved is by placing {0, 5, 6, 7, 8, 9} on one 
		// cube and {1, 2, 3, 4, 8, 9} on the other cube.

		// However, for this problem we shall allow the 6 or 9 to be turned upside-down so that 
		// an arrangement like {0, 5, 6, 7, 8, 9} and {1, 2, 3, 4, 6, 7} allows for all nine 
		// square numbers to be displayed; otherwise it would be impossible to obtain 09.

		// In determining a distinct arrangement we are interested in the digits on each cube, 
		// not the order.

		//   {1, 2, 3, 4, 5, 6} is equivalent to {3, 6, 4, 1, 2, 5}
		//   {1, 2, 3, 4, 5, 6} is distinct from {1, 2, 3, 4, 5, 9}

		// But because we are allowing 6 and 9 to be reversed, the two distinct sets in the last 
		// example both represent the extended set {1, 2, 3, 4, 5, 6, 9} for the purpose of forming 
		// 2-digit numbers.

		// How many distinct arrangements of the two cubes allow for all of the square numbers 
		// to be displayed?

		private static int[] needed = new int[] { 1, 4, 6, 16, 25, 36, 46, 64, 81 };
		private static List<Die> diePossibilies;
		
		public static void Solve()
		{
			Console.WriteLine("Problem 090");

			GetDiePossibilities();

			int countDistinct = 0;
			List<TwoDiceRoll> distinctRolls = new List<TwoDiceRoll>();

			foreach(Die d1 in diePossibilies)
			{
				foreach (Die d2 in diePossibilies)
				{
					TwoDiceRoll roll = new TwoDiceRoll(d1, d2);
					if(CanMakeAllSquares(roll))
					{
						if (!distinctRolls.Contains(roll))
						{
							Console.WriteLine("d1: {0}  d2: {1}", d1, d2);
							countDistinct++;
							//if (d1.Contains6or9) countDistinct++;
							//if (d2.Contains6or9) countDistinct++;
							//if (d1.Contains6or9 && d2.Contains6or9) countDistinct++;
							distinctRolls.Add(roll);
						}
					}
				}
			}
			Console.WriteLine("distinct: {0}", countDistinct);
		}

		private static bool CanMakeAllSquares(TwoDiceRoll roll)
		{
			Die d1 = roll.Die1;
			Die d2 = roll.Die2;
			bool[] found = new bool[100];
			foreach (int d1s in d1.Sides)
			{
				foreach (int d2s in d2.Sides)
				{
					int d1ss = d1s == 9 ? 6 : d1s;
					int d2ss = d2s == 9 ? 6 : d2s;
					int roll1 = 10*d1ss + d2ss;
					int roll2 = 10*d2ss + d1ss;
					found[roll1] = true;
					found[roll2] = true;
				}
			}

			foreach (int need in needed)
			{
				if(found[need]==false) return false;
			}

			return true;
		}

		private static void GetDiePossibilities()
		{
			diePossibilies = new List<Die>();
			for(int s1 = 0; s1<=9;s1++)
			{
				for(int s2 = s1 + 1; s2 <=9; s2++)
				{
					for(int s3 = s2 + 1; s3 <=9; s3++)
					{
						for(int s4 = s3+1;s4<=9; s4++)
						{
							for(int s5=s4+1; s5<=9; s5++)
							{
								for(int s6= s5+1; s6<=9; s6++)
								{
									Die die = new Die(s1,s2,s3,s4,s5,s6);
									diePossibilies.Add(die);
								}
							}
						}
					}
				}
			}
		}
	}
}
