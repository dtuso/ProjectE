using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectEuler.Problems
{
	class TryCatchFinally
	{

		public static void Solve(bool exitEarly, out int somethingToHaveFunWith)
		{
			somethingToHaveFunWith = 10;

			try
			{
				Console.WriteLine("Exit Early: {0}", exitEarly);
				somethingToHaveFunWith = 20;
				if (exitEarly)
					return;
			}
			catch(Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
			finally
			{
				somethingToHaveFunWith = 30;
				Console.WriteLine("Finally");
			}
		}
	}
}
