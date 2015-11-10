using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace sideTesting
{
	class CheckPalindrome
	{

		public static bool isPalindrome(string phrase)
		{
			//bool isPal = false;
			//isPal = Regex.IsMatch( possible, @"^(.{" + possible.Length/2 + @"}).?\~1$");

			//return isPal;

			for (int i = 0; i < phrase.Length / 2; i++)
			{
				if (!(phrase[i] == phrase[phrase.Length - 1 - i])) return false;
			}
			return true;


		}
	}
}
