using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectEuler.Helpers
{
	class NumericPermutations
	{
		private int[] digits;
		private bool _eof = false;
		public int Length { get { return digits.Length; } }

		public NumericPermutations(int len)
		{
			if (len<2 )
				throw new ArgumentOutOfRangeException( "Valid lengths are greater than 1.");
			digits = new int[len];

			for (int i = 0; i < len; i++)
			{
				digits[i] = i;
			}

		}

		public int[] Current
		{
			get 
			{
				return digits;
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
			while ((i >= 0) && (digits[i] > digits[i + 1]))
			{
				--i;
			}
			return i;
		}

		private void swapWithSmallestAfterHighest(int hi)
		{
			// P(i) will be swapped with the smallest of the elements after P(i), but not larger than P(i)
			int lo = digits.Length - 1;
			while (digits[hi] > digits[lo])
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
			int pswap = digits[first];
			digits[first] = digits[second];
			digits[second] = pswap;
		}

	}
}
