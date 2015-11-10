using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectEuler.Helpers
{
	class Factorials
	{
		private int _numEntries;
		private BigInteger[] factorials;
		public Factorials(int numEntries)
		{
			_numEntries = numEntries;
			PreLoadFactorials();
		}

		private void PreLoadFactorials()
		{
			factorials = new BigInteger[_numEntries];
			factorials[0] = 1;
			for (int i = 1; i < _numEntries; i++)
			{
				factorials[i] = i * factorials[i - 1];
			}
		}


		public void ConsoleWrite()
		{
			foreach (BigInteger value in factorials)
			{
				Console.WriteLine("{0} ", value);
			}
		}
	}
}
