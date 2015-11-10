using System;
using System.Collections.Generic;

namespace ProjectEuler.Helpers
{
	
	class Pandigital
	{
		private char[] digits;
		private bool _eof = false;
		public int Length { get { return digits.Length; } }

		public Pandigital(int len)
		{
			if (len<1 || len > 10)
				throw new ArgumentOutOfRangeException( "Valid lengths are 1 to 9.");
			digits = new char[len];

			for (int i = 0; i < len; i++)
			{
				if (len==10)
					digits[i] = Char.Parse(i.ToString());
				else
					digits[i] = Char.Parse((i + 1).ToString());
			}

		}

		public BigInteger Value
		{
			get
			{
				return new BigInteger(MiscFunctions.CharArrayToString(digits), 10);
			}
		}

		public string Current
		{
			get 
			{
				return MiscFunctions.CharArrayToString(digits);
			}
		}

		public bool EOF
		{
			get { return _eof; }
		}


		public bool MoveNext()
		{
			if(!_eof)
				_eof = !findNext();
			return !_eof;
		}

		private bool findNext()
		{
			int hi = findHighestIndex();
			if (hi < 0)
				return false;
			swapWithSmallestAfterHighest(hi);
			swapLastFewElements(hi);

			return true;
		}

		private int findHighestIndex()
		{
			// first find the largest index i so that P(i) < P(i + 1).
			int i = digits.Length - 2;
			while ((i >= 0) && (Int32.Parse(digits[i].ToString()) > Int32.Parse(digits[i + 1].ToString())))
			{
				--i;
			}
			return i;
		}

		private void swapWithSmallestAfterHighest(int hi)
		{
			// P(i) will be swapped with the smallest of the elements after P(i), but not larger than P(i)
			int lo = digits.Length - 1;
			while (Int32.Parse(digits[hi].ToString()) > Int32.Parse(digits[lo].ToString()))
			{
				lo--;
			}
			Swap(hi, lo);
		}

		private void swapLastFewElements(int hi)
		{
			int k = 0;
			for (int j = hi + 1; j < (digits.Length + hi) / 2 + 1; ++j, ++k)
			{
				Swap(j, (digits.Length - k - 1));
			}
		}

		private void Swap(int first, int second)
		{
			char pswap = digits[first];
			digits[first] = digits[second];
			digits[second] = pswap;
		}




		
		public static char[] MakePandigitalOneToN(int n)
		{
			char[]  chars = new char[n];

			for (int i = 0; i < n; i++)
			{
				chars[i] = Char.Parse((i + 1).ToString());
			}
			return chars;
		}
		public static bool IsPandigital(string number)
		{
			int len = number.Length;
			List<char> chars = new List<char>( );
			for (int i = 0; i < len; i++)
			{
				if (number[i] == '0')
				{
					return false;
				}
				else if(chars.Contains( number[i] ))
				{
					return false;
				}
				else
				{
					chars.Add( number[i]);
				}
			}
			return true;
		}
	}
}
