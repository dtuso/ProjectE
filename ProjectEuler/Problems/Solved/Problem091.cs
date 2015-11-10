using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using ProjectEuler.Helpers;

namespace ProjectEuler.Problems
{
	class p091
	{
		//Given that 0 <= x1, y1, x2, y2 <= 50, how many right triangles can be formed?
		public static void Solve(int max)
		{

			// 14234

			Console.WriteLine("Starting 91 with max = {0}.", max);

			Point O = new Point(0,0);
			int count = 0;

			for (int p1x = 0; p1x <= max; p1x++)
			{
				for (int p1y = 0; p1y <= max; p1y++)
				{
					Point p1 = new Point(p1x, p1y);

					for (int p2x = 0; p2x <= max; p2x++)
					{
						for (int p2y = 0; p2y <= max; p2y++)
						{
							Point p2 = new Point(p2x, p2y);
							Triangle t = new Triangle(O,p1,p2);
							if(t.IsRightTriangle)
							{
								//Console.WriteLine(t);
								count++;
							}

						}
					}
				}
			}

			Console.WriteLine(count/2);
			
		}
	}
}
