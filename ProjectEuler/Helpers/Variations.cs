using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectEuler.Helpers
{
	class MaskVariations
	{
		public int Length;
		public int Keep;

		public bool[] BoolMask;
		
		private int lowest;
		private int maximum;
		private int highestBitVal;
		private int Value;
		private int numBitsSet;

		public MaskVariations(int length, int keep)
		{
			Length = length;
			Keep = keep;

			lowest = (int) Math.Pow(2, keep) - 1;
			maximum = (int) Math.Pow(2, length) - 1;
			highestBitVal = (maximum + 1) / 2;
			Value = lowest;
			ProcessValue();
		}

		public bool GetNext()
		{
			while(true)
			{
				Value++;
				if (Value > maximum)
					return false;
				ProcessValue();

				if (numBitsSet == Keep)
					return true;
			}  
		}

		private void ProcessValue()
		{
			BoolMask = ToBoolArray();
			numBitsSet = CountBits();
		}

		private int CountBits()
		{
			int numOnes = 0;
			for (int i = 0; i < Length; i++)
			{
				if (BoolMask[i])
				{
					numOnes++;
				}
			}
			return numOnes;
		}

		private bool[] ToBoolArray()
		{
			bool[] result = new bool[Length];
			int pos = Length - 1;
			int val = Value;
			for (int bitVal = highestBitVal; bitVal >= 1; bitVal /= 2)
			{
				if (val >= bitVal)
				{
					result[pos] = true;
					val -= bitVal;
				}
				pos--;
			}
			return result;
		}
	}
}
