using System;

using ProjectEuler.Helpers;

namespace ProjectEuler.Problems
{
    class Problem025
    {

        public static void Solve()
        {
            int maxIterations = 1000;
            int lastLen = 0;
        	BigInteger lastFactorial = 0;
            BigInteger thisFactorial = 1;
            int i = 1;
            while (lastLen < maxIterations)
            {
                i++;
                thisFactorial = GetNextFactorial(thisFactorial, ref lastFactorial);
                if (thisFactorial.ToString().Length > lastLen)
                {
					lastLen = thisFactorial.ToString().Length;
                    Console.WriteLine("F{0}={1} \t\t {2} long", i, thisFactorial, lastLen);
                }
            }
        }

        
        static BigInteger GetNextFactorial(BigInteger thisFactorial, ref BigInteger lastFactorial)
        {
            BigInteger tmp = thisFactorial;
            BigInteger result = thisFactorial + lastFactorial;
            lastFactorial = tmp;
            return result;
        }

    }
}
