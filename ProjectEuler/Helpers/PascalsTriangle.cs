using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectEuler.Helpers
{
	public class PascalsTriangle
	{

		private int _numRows;
		private BigInteger[][] values;

		public PascalsTriangle(int numRows)
		{
			_numRows = numRows;
			BuildTriangle();

		}

		private void BuildTriangle()
		{
			values = new BigInteger[_numRows + 1][];
			values[0] = new BigInteger[] { 1 };
			values[1] = new BigInteger[] { 1, 1 };

			//row num is rowIdx + 1
			for (int rowIdx = 2; rowIdx <= _numRows; rowIdx++)
			{
				// build the new row
				values[rowIdx] = new BigInteger[rowIdx+1];
				values[rowIdx][0] = 1;
				values[rowIdx][rowIdx] = 1;
				int midPoint = rowIdx/2;
				for (int col = 1; col <= midPoint; col++)
				{
					values[rowIdx][col] = values[rowIdx-1][col - 1] + values[rowIdx-1][col];
					values[rowIdx][rowIdx - col] = values[rowIdx][col];
				}
			}
		}

		public BigInteger[] GetPascalRow(int row)
		{
			return values[row];
		}

		public BigInteger GetPascalNumber(int row, int element)
		{
			// row num is rowIdx + 1
			// element is colIdx + 1
			return values[row-1][element-1];
		}

		public void ConsoleWrite()
		{
			foreach(BigInteger[] rows in values)
			{
				foreach(BigInteger value in rows)
				{
					Console.Write("{0} ", value);
				}
				Console.WriteLine();
			}
		}
	}
}
