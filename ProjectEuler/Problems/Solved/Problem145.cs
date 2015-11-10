using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ProjectEuler.Problems
{
    /// <summary>
    /// Some positive integers n have the property that the sum [ n + reverse(n) ] consists entirely of odd (decimal) digits. For instance, 36 + 63 = 99 and 409 + 904 = 1313. We will call such numbers reversible; so 36, 63, 409, and 904 are reversible. Leading zeroes are not allowed in either n or reverse(n).
    /// There are 120 reversible numbers below one-thousand.
    /// How many reversible numbers are there below one-billion (109)?

    /// </summary>
    public class Problem145
    {

        static Regex re = new Regex("^[13579]+$");
        public static void Solve(int maxInt)
        {
            int cnt = 0;
            for (int i = 1; i < maxInt; i++)
            {
                if (isReversible(i))
                {
                    cnt++;
                }
                if (i % 100000 == 0)
                    Console.WriteLine("{0,4} = {1,10}", cnt, i);
            }
            Console.WriteLine("{0} has {1} reversible numbers", maxInt, cnt);
        }

        private static bool isReversible(int num)
        {
            if ((num%10)==0)
                return false;

            return containsOnlyOddDigits(num + ReverseNumber(num));

        }

        private static bool containsOnlyOddDigits(int num)
        {
            return re.IsMatch( num.ToString());
        }

        private static int ReverseNumber(int num)
        {
            StringBuilder r = new StringBuilder();
            char[] cNum = num.ToString().ToCharArray( );
            for (int i = cNum.Length - 1; i >= 0; i--)
            {
                r.Append(cNum[i]);
            }
            return Int32.Parse(r.ToString());
        }
    }
}
