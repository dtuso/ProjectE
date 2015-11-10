using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using ProjectEuler.Helpers;

namespace ProjectEuler.Problems
{
	class Problem162
	{

		//n the hexadecimal number system numbers are represented using 16 different digits:

		//0,1,2,3,4,5,6,7,8,9,A,B,C,D,E,F

		//The hexadecimal number AF when written in the decimal number system equals 10x16+15=175.

		//In the 3-digit hexadecimal numbers 10A, 1A0, A10, and A01 the digits 0,1 and A are all present.
		//Like numbers written in base ten we write hexadecimal numbers without leading zeroes.

		//How many hexadecimal numbers containing at most sixteen hexadecimal digits exist with all 
		//of the digits 0,1, and A present at least once?
		//Give your answer as a hexadecimal number.

		//(A,B,C,D,E and F in upper case, without any leading or trailing code that marks the number 
		//as hexadecimal and without leading zeroes , e.g. 1A3F and not: 1a3f and not 0x1a3f and not 
		//$1A3F and not #1A3F and not 0000001A3F)

// if(s.Contains("0") && s.Contains("1") && s.Contains("A"))
//Start of Main 10/8/2008 10:53:23 AM
//00:00:10.0010679             18782470
//388496
//5ED90
//End of Main (00:00:10.0024205) 10/8/2008 10:53:33 AM

//if(s.IndexOf('0')>-1 && s.IndexOf('1')>-1 && s.IndexOf('A')>-1)
//Start of Main 10/8/2008 10:55:01 AM
//00:00:10.0015623             25344112
//524049
//7FF11
//End of Main (00:00:10.0260785) 10/8/2008 10:55:11 AM


		// regex is slower than my histogram checking!
		//private static readonly Regex required = new Regex("((?=.*[A])(?=.*[0])(?=.*[1]))",RegexOptions.Compiled);
		public static void Solve()
		{
			ulong MAX = checked(18446744073709550096 + 904); // FFFF FFFF FFFF FA10 + 100(dec)
			//MAX = 100000000;
			ulong numPages = 1000;
			ulong pageSize = MAX / (ulong)numPages;
			int maxLen = 16;
			ulong cnt = 0;
			Stopwatch sw = new Stopwatch();
			
			sw.Start();

			for (ulong page = 0; page < numPages; page++)
			{
				cnt += GetCounts(page, pageSize);
				Console.WriteLine("Page: {0,2} Time: {1}", page + 1, sw.Elapsed);
			}

			Console.WriteLine(cnt);
			Console.WriteLine(cnt.ToString("X"));

		}

		public static ulong GetCounts(ulong pageNum, ulong pageSize)
		{
			ulong subCount = 0;
			ulong start = pageNum*pageSize;   //   0, 1000, 2000
			ulong end = start + pageSize - 1; // 999, 1999, 2999
			for (ulong i = start; i <= end; i++)
			{
				string s = i.ToString("X");
				if(s.IndexOf('0')>-1 && s.IndexOf('1')>-1 && s.IndexOf('A')>-1)
				{
					subCount++;
				}
			}
			return subCount;
		}
	}
}
