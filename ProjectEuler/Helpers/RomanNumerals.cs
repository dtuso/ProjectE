using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectEuler.Helpers
{


	//http://groups.google.co.uk/group/microsoft.public.dotnet.languages.csharp/msg/da321010fd7fd92f?q=ten.egnaro%40werts+roman&hl=en&lr=&ie=UTF-8&oe=UTF-8&rnum=1

	
	class RomanNumerals
	{
		private class RomanNumber
		{
			int _value; string _rep;
			internal RomanNumber(int value, string rep) { _value = value; _rep = rep; }
			public int Value { get { return _value; } }
			public string Rep { get { return _rep; } }

		}

		static RomanNumber[] numbers = new RomanNumber[] {
			new RomanNumber(1000, "M"),
			new RomanNumber(900, "CM"),
			new RomanNumber(500, "D"),
			new RomanNumber(400, "CD"),
			new RomanNumber(100, "C"),
			new RomanNumber(90, "XC"),
			new RomanNumber(50, "L"),
			new RomanNumber(40, "XL"),
			new RomanNumber(10, "X"),
			new RomanNumber(9, "IX"),
			new RomanNumber(5, "V"),
			new RomanNumber(4, "IV"),
			new RomanNumber(1, "I")
		};

		//Then you can simply go through the list top to bottom, if the entry has a
		//Value >= than your input then add its Representation to the output,
		//subtract, and repeat. If not then go on to the next number, repeat until the
		//input = 0.

		public static string ToRomanNumeral(int number)
		{
			if(number>10000)
			{
				throw new Exception("Unable to convert numbers over 10,000 ");
			}

			StringBuilder result = new StringBuilder();
			int numIndex = 0;
			while (number > 0)
			{
				int val = numbers[numIndex].Value;
				if (number >= val)
				{
					number -= val;
					result.Append(numbers[numIndex].Rep);
				}
				else
				{
					numIndex++;
				}
			}
			return result.ToString();

		}

		public static int ToInteger(string roman)
		{
			//Note that this algorithm does not have to do the horrific replacement of the
			//prefixed forms ("IIII" to "IV" etc.) as they are already in the table.

			//The roman-to-integer conversion algorithm is almost perfectly the reverse:
			//    0. Result = 0.
			//    1. Set index = 0.
			//    2. Repeat while index < number of elements in the array:
			//            Does Rep of the entry at top of table match head of string?
			//                If so, add Value to Result and remove the match from the
			//head of the string.
			//                If not, increment index, go to 2.
			//    3. If the string is not empty then the input was ill-formed, throw
			//exception.
			//    4. Return Result. 
			
			string decreasing = roman;
			int res =  0;
			int i = 0;

			while (i < numbers.Length)
			{
				bool done = false;
				while (!done)
				{
					if (decreasing.Length==0)
						break;
					if (decreasing.Length < numbers[i].Rep.Length)
						break;
					if (decreasing.Substring(0, numbers[i].Rep.Length) == numbers[i].Rep)
					{
						done = false;
						decreasing = decreasing.Remove(0, numbers[i].Rep.Length);
						res += numbers[i].Value;
					}
					else
					{
						done = true;
					}
				}

				i++;
			}

			return res;
		}



	}

}
