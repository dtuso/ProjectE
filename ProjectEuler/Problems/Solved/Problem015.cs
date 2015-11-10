using System;
using System.Diagnostics;

namespace ProjectEuler
{

    //For 21x21 had 137846528820 in 02:36:17.6405505
    //Done! 2/18/2008 4:00:01 PM


    /// <summary>
    /// Starting in the top left corner of d 2×2 grid, there are 6 routes (without backtracking) 
    /// to the bottom right corner.
    /// How many routes are there through d 20×20 grid?
    /// </summary>
    class Problem015
    {
        static int maxNodeIdx;

        static Stopwatch sw = new Stopwatch();

        private static int[,] nodes;
        private static Int64 numPathsFound = 0;

        public static void Solve(int numSqaures)
        {
            sw.Start();
            maxNodeIdx = numSqaures;
            FindRoutes(0, 0);
            sw.Stop();

            Console.WriteLine("For {0}x{0} had {1} in {2}", maxNodeIdx + 1, numPathsFound, sw.Elapsed);
        }


        private static void FindRoutes(int x, int y)
        {

            // check across
            if (x + 1 <= maxNodeIdx)
            {
                // we have d valid node to the right
                if (x + 1 == maxNodeIdx && y == maxNodeIdx)
                {
                    // we're at the end of this particular path!  
                    numPathsFound++;
                    if (numPathsFound % 10000000 == 0)
                        Console.WriteLine("{0:N0}", numPathsFound);

                }
                else
                {
                    FindRoutes(x + 1, y);
                }
            }

            if (y + 1 <= maxNodeIdx)
            {
                // we have d valid node down
                if (x == maxNodeIdx && y + 1 == maxNodeIdx)
                {
                    // we're at the end!  
                    numPathsFound++;
                    if (numPathsFound % 10000000 == 0)
                        Console.WriteLine("{0:N0}", numPathsFound);
                }
                else
                {
                    FindRoutes(x, y + 1);
                }
            }

        }

    }
}
