using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectEuler.Helpers
{
	class MiscFunctions
	{
		public static string ToBinary(int num, int width)
		{
			string result = "";
			do
			{
				result = ((num % 2 == 0) ? "0" : "1") + result;
				num = num / 2;
			} while (num != 0);
			return result.PadLeft(width,'0');
		}

		public static BigInteger Reverse(BigInteger num)
		{
			string reversed = String.Empty;

			foreach (char c in num.ToString())
			{
				reversed = c.ToString() + reversed;
			}
			return  new BigInteger(reversed,10);
		}

		public static decimal Reverse(decimal num)
		{
			string reversed = String.Empty;
			
			foreach (char c in num.ToString())
			{
				reversed = c.ToString() + reversed;
			}
			return Decimal.Parse( reversed );
		}


		public static int Reverse(int num)
		{
			string reversed = String.Empty;
			foreach ( char c in num.ToString( ) )
			{
				reversed = c.ToString() + reversed;
			}
			return Int32.Parse(reversed);
		}

		public static string Base10ToBinary(int num)
		{
			string resultingBinary = "";
			int pot = 1;
			while (pot <= num)
			{
				if ((num & pot) == pot)
				{
					resultingBinary = "1" + resultingBinary;
				}
				else
				{
					resultingBinary = "0" + resultingBinary;
				}
				pot *= 2;
			}
			return resultingBinary;
		}

		public static bool IsInteger(decimal num)
		{
			return (num == Math.Floor(num));
		}
		public static bool IsInteger(double num)
		{
			return (num == Math.Floor(num));
		}
		public static bool IsInteger(float num)
		{
			return (num == Math.Floor(num));
		}

		public static bool IsPalindrome(string phrase)
		{
			int len = phrase.Length;
			for (int i = 0; i < len / 2; i++)
			{
				if (phrase[i] != phrase[len - 1 - i]) return false;
			}
			return true;
		}

		public static bool IsNotBouncy(string strNum)
		{
			int i = 0;
			char l, t;
			return !IsBouncy(strNum, out i, out t, out l);
		}

		public static bool IsBouncy(string strNum, out int bouncyAt, out char thisChar, out char lastChar)
		{
			bool goingUp = false;
			bool goingDn = false;
			strNum = strNum.Replace("-", "").Replace(" ", "").Replace(".", "");
			lastChar = strNum[0];
			thisChar = '-';
			bouncyAt = 0;
			if(strNum.Length<3) 
				return false;

			for (bouncyAt = 1; bouncyAt < strNum.Length; bouncyAt++)
			{
				thisChar = strNum[bouncyAt];
				lastChar = strNum[bouncyAt - 1];
				if (thisChar > lastChar)
					goingUp = true;
				if (thisChar < lastChar)
					goingDn = true;
				if (goingDn && goingUp)
					return true;
				}
			return false;
		}

		//public static bool IsNotBouncy(int num)
		//{
		//    char[] digits = num.ToString().ToCharArray();
		//    int lastDigit = Int32.Parse(digits[0].ToString());
		//    bool wentUp = false;
		//    bool wentDn = false;
		//    foreach (char c in digits)
		//    {
		//        int thisDigit = Int32.Parse(c.ToString());
		//        if (thisDigit > lastDigit)
		//            wentUp = true;
		//        if (thisDigit < lastDigit)
		//            wentDn = true;
		//        if (wentUp && wentDn)
		//        {
		//            return false;
		//        }
		//        lastDigit = thisDigit;
		//    }

		//    return (wentUp && wentDn);
		//}
		//public static bool IsBouncy(int num)
		//{
		//    char[] digits = num.ToString().ToCharArray();
		//    int lastDigit = Int32.Parse(digits[0].ToString());
		//    bool wentUp = false;
		//    bool wentDn = false;
		//    foreach (char c in digits)
		//    {
		//        int thisDigit = Int32.Parse(c.ToString());
		//        if (thisDigit > lastDigit)
		//            wentUp = true;
		//        if (thisDigit < lastDigit)
		//            wentDn = true;
		//        if (wentUp && wentDn)
		//        {
		//            break;
		//        }
		//        lastDigit = thisDigit;

		//    }

		//    return (wentUp && wentDn);
		//}


		public static int SumDigits(BigInteger num)
		{
			int sum = 0;
			foreach ( char c in num.ToString( ).ToCharArray( ) )
			{
				sum += Int32.Parse(c.ToString());
			}
			return sum;
		}
		
		public static string CharArrayToString(char[] charArray)
		{
			StringBuilder sb = new StringBuilder();
			foreach (char c in charArray)
			{
				sb.Append(c);
			}
			return sb.ToString();
		}


	}
}
