using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using ProjectEuler.Helpers;

namespace ProjectEuler.Problems
{

	//Three mirrors are arranged in the shape of an equilateral triangle, with their reflective surfaces 
	//pointing inwards. There is an infinitesimal gap at each vertex of the triangle through which a 
	//laser beam may pass.

	//Label the vertices A, B and C. There are 2 ways in which a laser beam may enter vertex C, bounce off 
	//11 surfaces, then exit through the same vertex: one way is shown below; the other is the reverse 
	//of that.

	//There are 80840 ways in which a laser beam may enter vertex C, bounce off 1000001 surfaces, then exit 
	//through the same vertex.

	//In how many ways can a laser beam enter at vertex C, bounce off 12017639147 surfaces, then exit 
	//through the same vertex?

	// 60 + 60 + 60
	// 0 to 30 (but not including)

	class Problem202
	{
		private static double sideLen = Math.Pow(10, 5);
		private static PointD Origin = new PointD(0,0);
		private static double triangleHeight = Math.Sqrt(Math.Pow(sideLen, 2) - Math.Pow(sideLen / 2, 2));
		private static PointD pointR = new PointD(sideLen / 2, triangleHeight);
		private static PointD pointL = new PointD(-sideLen / 2, triangleHeight);
		private static LineD LefLine = new LineD(Origin,pointL);
		private static LineD TopLine = new LineD(pointL, pointR);
		private static LineD RigLine = new LineD(pointR, Origin);

		private static double inc= 1.0d;

		public static void Solve()
		{

			for (double aimedAtX = pointL.X + inc; aimedAtX < pointR.X; aimedAtX += inc)
			{
				LineD firstShot = new LineD(Origin, new PointD(aimedAtX, (triangleHeight + 1)));
				long numBounces = CountBounces(firstShot);
			}
			//PointD hit = firstShot.GetIntersection(B);
			


		}


		private static long CountBounces(LineD shot)
		{
			long numBounces = 0;
			LineD reflectedFrom = null;
			LineD lastReflectedFrom = null;
			PointD p = null;
			LineDIntersection intersect;
			do
			{
				// find intersection (try each of three lines)
				p = null; // reset
				reflectedFrom = null;
				if (p == null && lastReflectedFrom != TopLine)
				{
					intersect = shot.GetIntersection(TopLine);
					if (intersect.LinesIntersect)
					{
						p = intersect.Intersection;
						reflectedFrom = TopLine;
					}
				}
				if (p == null && lastReflectedFrom != LefLine)
				{
					intersect = shot.GetIntersection(LefLine);
					if (intersect.LinesIntersect)
					{
						p = intersect.Intersection;
						reflectedFrom = LefLine;
					}
				}
				if (p == null && lastReflectedFrom != RigLine)
				{
					intersect = shot.GetIntersection(RigLine);
					if (intersect.LinesIntersect)
					{
						p = intersect.Intersection;
						reflectedFrom = RigLine;
					}
				}

				// compute attack deltas.  Reverse the "horizontal" determined by orientation!!!



				// negate the "horizontal" element

				// compute the new line

				// increaseshot count
				numBounces++;
				lastReflectedFrom = reflectedFrom;
			} while (p != Origin);
		

			return numBounces;
		}




		private static void TestAndReportOnIntersection(LineD Line1, LineD Line2)
		{
			LineDIntersection hit = Line1.GetIntersection(Line2);
			Console.WriteLine();
			Console.WriteLine("Line1 {0}", Line1);
			Console.WriteLine("Line2 {0}", Line2);
			if (hit.LinesIntersect)
			{
				Console.WriteLine("Found Point {0}", hit.Intersection);
				Console.WriteLine("ua {0} ub {1}", hit.ua, hit.ub);
			}
			else
			{
				if (hit.LinesAreParallel)
					Console.WriteLine("Parallel");
				else if (hit.LinesAreCoincident)
					Console.WriteLine("Coincident");
				else
				{
					Console.WriteLine("ua {0} ub {1}", hit.ua, hit.ub);
					Console.WriteLine("May have hit, but not long enough!");
				}
			}
		}

	}
}
