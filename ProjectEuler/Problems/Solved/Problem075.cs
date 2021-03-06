using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using ProjectEuler.Helpers;

namespace ProjectEuler.Problems
{
	class Problem075
	{

		//It turns out that 12 cm is the smallest length of wire can be bent to form a right angle 
		//triangle in exactly one way, but there are many more examples.

		//12 cm: (3,4,5)
		//24 cm: (6,8,10)
		//30 cm: (5,12,13)
		//36 cm: (9,12,15)
		//40 cm: (8,15,17)
		//48 cm: (12,16,20)

		//In contrast, some lengths of wire, like 20 cm, cannot be bent to form a right angle 
		// triangle, and other lengths allow more than one solution to be found; for example, 
		// using 120 cm it is possible to form exactly three different right angle triangles.

		//120 cm: (30,40,50), (20,48,52), (24,45,51)

		//Given that L is the length of the wire, for how many values of L ≤ 2,000,000 can exactly 
		//one right angle triangle be formed?


		// http://www.clarku.edu/~djoyce/trig/right.html
		// In Euclid's Elements there is a description of all the possible Pythagorean triples. 
		// Here's a modern paraphrase of Euclid. Take any two odd numbers m and n, with m <  n, 
		// and relatively prime (that is, no common factors). Let a = mn, let b = (n^2 – m^2)/2, 
		// and let c = (n^2 + m^2)/2. Then a:b:c is a Pythagorean triple. For instance, if you take 
		// m = 1, and n = 3, then you get the smallest Pythagorean triple 3:4:5

		//For max=2000000, found 214954
		//End of Main (00:01:33.3252419) 5/5/2008 4:00:49 PM

		private static int BIGGEST = 2000000; //2000000;
		private static int[] solutions = new int[BIGGEST + 1];
		private static List<int> multipleRightLenghts = new List<int>();
		private static List<int> divisorsRightLenghts = new List<int>();
		private static List<int> singleRightLenghts = new List<int>();


		public static void Solve()
		{
			Stopwatch sw = new Stopwatch();
			sw.Start();

			for(int m = 1; m < BIGGEST ; m++)
			{
				double max_n = 1+ MyMath.QuadraticMaxVal(2, m, -2*BIGGEST);
				for (int n = m + 2; n < max_n; n += 2)
				{
					if (MyMath.AreCoprime(m, n))
					{
						int a = m*n;
						int b = checked(n*n - m*m)/2;
						int c = checked(n*n + m*m)/2;
						int len = checked(a + b + c);
						if (len > BIGGEST)
							break;
						//Console.Write(" {0,7} ",len );
						LogSolution(len);
					}
				}
			}

			for(int i=1;i<BIGGEST;i++)
			{
				if(solutions[i]==1)
				{
					multipleRightLenghts.Add(i);
				}
			}

			// potential multiple divisors, so filter out the extras such as 120

			foreach (int sol in multipleRightLenghts)
			{
				int divisors = 0;
				foreach (int divisor in divisorsRightLenghts)
				{
					if (sol % divisor == 0)
					{
						divisors++;
						if (divisors > 1)
							break;
					}
				}
				if(divisors==0 || divisors==1)
				{
					
					bool addThisDivisor = true;
					foreach (int divisor in singleRightLenghts)
					{
						if (divisor*2 > sol)
							break;
						if (sol % divisor == 0)
						{
							addThisDivisor = false;
							break;
						}
					}
					if (addThisDivisor)
					{
						divisorsRightLenghts.Add(sol);
					}
					singleRightLenghts.Add(sol);
					Console.Write(" {0,7} ", sol);
					if (sol % 200000 < 10)
					{
						Console.WriteLine();
						Console.WriteLine(sw.Elapsed);
					}
				}
			}
			Console.WriteLine();
			Console.WriteLine("For max={0}, found {1}",BIGGEST, singleRightLenghts.Count);

		}

		private static void LogSolution(int len)
		{
			for (int i = 1; i <= BIGGEST;i++)
			{
				if(len*i>BIGGEST)
					break;
				solutions[len*i]++;
			}
		}

		//public static void Solve()
		//{
			
		//    for (int l = 12; l <= BIGGEST; l+=2)
		//    {
				//int divisors = 0;
				//foreach (int i in singleRightLenghts)
				//{
				//    if(i*2>l)
				//        break;
				//    if (l % i == 0)
				//    {
				//        if (++divisors>1)
				//            break;
				//    }

				//}
				//if (divisors == 1)
				//{
				//    Console.WriteLine("found: {0,7}",l);
				//    singleRightLenghts.Add(l);
				//}
		//        else if (divisors == 0)
		//            FindRightAngleSolutions(l);
		//    }
		//    Console.WriteLine(singleRightLenghts.Count);
		//}

		//private static void FindRightAngleSolutions(int len)
		//{
		//    List<long> solutions = new List<long>();
		//    for(int a = 1; a<len-2;a++)
		//    {
		//        for(int b = a+1;b<len-2;b++)
		//        {

		//            int toMatch = len - a - b;
		//            double hypot = MyMath.RightTriangleGetHypotenuse(a, b);
		//            double side2 = MyMath.RightTriangleGetSide2(a, b);
		//            if ((hypot > 0 && toMatch == hypot) || (side2 > 0 && toMatch == side2))
		//            {
		//                int min = Math.Min(toMatch, Math.Min(a, b));
		//                int max = Math.Max(toMatch, Math.Max(a, b));
		//                long idx = checked(min * (long)BIGGEST + max);
		//                if (solutions.Contains(idx))
		//                    continue; // for loop
		//                solutions.Add(idx);
		//                //Console.WriteLine("{3,7}: {0,6} {1,6} {2,6} ", min, max, len - min - max, len);
		//                if (solutions.Count > 1) return;
		//            }
		//        }
		//    }
		//    if (solutions.Count == 1)
		//    {
		//        Console.WriteLine("Found :{0,7}", len);
		//        singleRightLenghts.Add(len);
		//    }
		//}



	}
}
