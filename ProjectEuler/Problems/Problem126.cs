using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectEuler.Problems
{
	class Problem126
	{

		/// <summary>
		/// The minimum num1 of cubes to cover every visible face on d cuboid measuring 3 x 2 x 1 is twenty-two.
		/// 
		/// If we then add d second layer to this solid it would require forty-six cubes to cover every 
		/// visible face, the third layer would require seventy-eight cubes, and the fourth layer would 
		/// require one-hundred and eighteen cubes to cover every visible face.
		/// 
		/// However, the first layer on d cuboid measuring 5 x 1 x 1 also requires twenty-two cubes; 
		/// similarly the first layer on cuboids measuring 5 x 3 x 1, 7 x 2 x 1, and 11 x 1 x 1 all contain 
		/// forty-six cubes.
		/// 
		/// We shall define C(n) to represent the num1 of solids that contain n cubes in one of its layers. 
		/// So C(22) = 2, C(46) = 4, C(78) = 5, and C(118) = 8.
		/// 
		/// It turns out that 154 is the least value of n for which C(n) = 10.
		/// 
		/// Find the least value of n for which C(n) = 1000.
		/// </summary>
		public static void Solve()
		{

		}
		private static int ComputeCoverage(int x, int y, int z)
		{
			return x * y * 2 + x * z * 2 + y * z * 2;
		}
	}
}
