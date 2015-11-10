using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectEuler.Helpers
{
	class RepeatingFractional
	{

		public static string GetSequence(int num, int denom)
		{
			int decimalsOfPrecision = denom * 2;
			decimalsOfPrecision = Math.Max(5, decimalsOfPrecision);
			BigInteger offset = new BigInteger("1" + Repeat('0', decimalsOfPrecision),10); // use offset to get n decimals of precision

			BigInteger fraction = offset * num / denom;
			string unitFraction = fraction.ToString();

			unitFraction = Repeat('0', decimalsOfPrecision - unitFraction.Length) + unitFraction;

			string sequence = GetRepeatingElement(unitFraction);
			return (sequence == "0") ? "" : sequence;
		}


		public static string GetSequence(BigInteger num, BigInteger denom)
		{
			Console.WriteLine("\tBEFORE {0} {1}", num,denom);
			MyMath.ReduceFraction(ref num, ref denom);
			Console.WriteLine("\tAFTER  {0} {1}", num, denom);
			return "";
			int decimalsOfPrecision = 0;
			Int32.TryParse(denom.ToString(),out decimalsOfPrecision);
			decimalsOfPrecision *= 2;
			decimalsOfPrecision = Math.Max(5, decimalsOfPrecision);
			//BigInteger offset = BigInteger.Parse("1" + Repeat('0', decimalsOfPrecision)); // use offset to get n decimals of precision
			BigInteger offset = BigInteger.Pow(10, decimalsOfPrecision);
			Console.WriteLine("decimalsOfPrecision={0}", decimalsOfPrecision);

			BigInteger fraction = offset * num / denom;
			string unitFraction = fraction.ToString();

			unitFraction = Repeat('0', decimalsOfPrecision - unitFraction.Length) + unitFraction;

			string sequence = GetRepeatingElement(unitFraction);
			return (sequence=="0")?"":sequence;
		}


		private static string Repeat(char chr, int times)
		{
			StringBuilder sb = new StringBuilder();
			for (int i = 1; i <= times; i++)
				sb.Append(chr);
			return sb.ToString();
		}

		private static string GetRepeatingElement(string unitFraction)
		{
			string repeatingElement = "";
			int len = unitFraction.Length;
			bool found = false;
			for (int startPos = 0; startPos < len && !found; startPos++)
			{
				for (int lenRepeat = 1; lenRepeat <= (len - startPos - 1) / 2 && !found; lenRepeat++)
				{
					repeatingElement = unitFraction.Substring(startPos, lenRepeat);
					bool isRepeating = true;
					//string allZeros = Repeat('0', lenRepeat);
					for (int checkPos = startPos; checkPos < len - lenRepeat; checkPos += lenRepeat)
					{
						if (repeatingElement != unitFraction.Substring(checkPos, lenRepeat) )
						{
							isRepeating = false;
							break;
						}
					}
					found = isRepeating;
				}
			}
			return repeatingElement;
		}



	}
}
