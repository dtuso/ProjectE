using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectEuler.Helpers
{
	public class Histogram
	{



		public static int[] NumericHistogram(decimal num)
		{
			int[] digitCount = new int[10];
			string strNum = num.ToString().Replace(".", "");
			for (int i = 0; i < strNum.Length; i++)
			{
				digitCount[strNum[i]-48]++;
			}
			return digitCount;
		}

		public static string NumericHistogram(double num)
		{
			StringBuilder sb = new StringBuilder();
			string strNum = num.ToString().Replace( ".","");
			int[] digitCount = new int[10];

			for (int i = 0; i < strNum.Length; i++)
			{
				digitCount[strNum[i] - 48]++;
			}
			for (int i = 0; i < 10; i++)
			{
				sb.AppendFormat("{0}|", digitCount[i]);
			}

			return sb.ToString();
		}

		public static string NumericHistogram(int num)
		{
			StringBuilder sb = new StringBuilder();
			string strNum = num.ToString();
			int[] digitCount = new int[10];

			for (int i = 0; i < strNum.Length; i++)
			{
				digitCount[strNum[i] - 48]++;
			}
			for (int i = 0; i < 10; i++)
			{
				sb.AppendFormat("{0}|", digitCount[i]);
			}

			return sb.ToString();
		}

		public static string WordHistogram(string word)
		{
			word = word.ToUpper( );

			int[] charCount = new int[26];

			for (int i = 0; i < word.Length; i++)
			{
				int ascii = (int)word[i] - 65;
				charCount[ascii]++;
			}
			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < 26; i++)
			{
				sb.AppendFormat("{0}|", charCount[i]);
			}
			if (sb.Length > 0)
				sb.Remove(sb.Length - 1, 1);

			return sb.ToString();
		}

		public static int[] HexadecimalHistogram(string hex)
		{
			hex = hex.ToUpper();

			int[] charCount = new int[16];

			for (int i = 0; i < hex.Length; i++)
			{

				int ascii = (int)hex[i];
				if (ascii > 64)
					ascii -= 55; // positions 10-15
				else
					ascii -= 48; // positions 00-09
				charCount[ascii]++;
			}

			return charCount;
		}

	}
}
