using System;
using System.Collections.Generic;
using System.Text;
using ProjectEuler.Helpers;

namespace ProjectEuler.Problems
{
	class p068
	{
		// Consider the following "magic" 3-gon ring, filled with the numbers 1 to 6, 
		// and each line adding to nine.

		// Working clockwise, and starting from the group of three with the numerically lowest 
		// external node (4,3,2 in this example), each solution can be described uniquely. 
		// For example, the above solution can be described by the set: 4,3,2; 6,2,1; 5,1,3.

		// It is possible to complete the ring with four different totals: 9, 10, 11, and 12. 
		// There are eight solutions in total.
		//  Total	Solution Set
		//   9	4,2,3; 5,3,1; 6,1,2
		//   9	4,3,2; 6,2,1; 5,1,3
		//  10	2,3,5; 4,5,1; 6,1,3
		//  10	2,5,3; 6,3,1; 4,1,5
		//  11	1,4,6; 3,6,2; 5,2,4
		//  11	1,6,4; 5,4,2; 3,2,6
		//  12	1,5,6; 2,6,4; 3,4,5
		//  12	1,6,5; 3,5,4; 2,4,6

		//By concatenating each group it is possible to form 9-digit strings; the maximum string 
		//for a 3-gon ring is 432621513.

		//Using the numbers 1 to 10, and depending on arrangements, it is possible to form 
		//16- and 17-digit strings. What is the maximum 16-digit string for a "magic" 5-gon ring?
		public static void Solve()
		{
			List<string> foundSolutionSets = new List<string>();
			NumericPermutations perms = new NumericPermutations(10);
			while (!perms.EOF)
			{
				FiveGon five = new FiveGon(perms.Current);
				if(five.IsValidFiveGonRing && five.Is16digitFiveGonRing)
				{
					Console.WriteLine("sum: {0} for {1}", five.Total, five.SolutionSetString);
					if(!foundSolutionSets.Contains(five.SolutionSetString))
					{
						foundSolutionSets.Add(five.SolutionSetString);
					}
				}
				perms.MoveNext();
			}
			long minSolSet = Int64.MaxValue;
			long maxSolSet = 0;
			foreach (string s in foundSolutionSets)
			{
				long sol = Int64.Parse(s);
				if(sol<minSolSet) minSolSet = sol;
				if(sol>maxSolSet) maxSolSet = sol;
			}
			Console.WriteLine(minSolSet);
			Console.WriteLine(maxSolSet);
		}
	}
	class FiveGon
	{
		private string solutionSetString;
		private bool isValidFiveGonRing;
		private int[] sums;
		private int[] sets;
		private int[] nodes;

		//         [0]                 
		//           .
		//            .
		//             [6]     [1]
		//            .   .    .
		//          .       . .  
		//       [5]        [7]
		//      .  .        .
		//    .     .      .
		//  [4]    [9] -- [8] -- [2]
		//            .
		//             .  
		//             [3] 
		public FiveGon(int[] zeroBasedNums)
		{
			nodes = new int[10];
			for(int idx=0; idx<10; idx++)
			{
				nodes[idx] = zeroBasedNums[idx] + 1;
			}
			sets = new int[5];
			sets[0] = Convert.ToInt32(Set0string);
			sets[1] = Convert.ToInt32(Set1string);
			sets[2] = Convert.ToInt32(Set2string);
			sets[3] = Convert.ToInt32(Set3string);
			sets[4] = Convert.ToInt32(Set4string);
			sums = new int[5];
			sums[0] = nodes[0] + nodes[6] + nodes[7];
			sums[1] = nodes[1] + nodes[7] + nodes[8];
			sums[2] = nodes[2] + nodes[8] + nodes[9];
			sums[3] = nodes[3] + nodes[9] + nodes[5];
			sums[4] = nodes[4] + nodes[5] + nodes[6];
			isValidFiveGonRing = (sums[0] == sums[1] & sums[1] == sums[2] && sums[2] == sums[3] && sums[3] == sums[4]);
		}

		public bool IsValidFiveGonRing
		{
			get { return isValidFiveGonRing; }
		}
		public int Total
		{
			get { return sums[0]; }
		}
		public bool Is16digitFiveGonRing
		{
			// if a 10 is in pos's 5-9, you will get a 17 digit solution string
			get { return (nodes[0] == 10 || nodes[1] == 10 || nodes[2] == 10 || nodes[3] == 10 || nodes[4] == 10); }
		}
		private string Set0string
		{
			get{return String.Format("{0}{1}{2}",nodes[0], nodes[6], nodes[7]);}
		}
		private string Set1string
		{
			get { return String.Format("{0}{1}{2}", nodes[1], nodes[7], nodes[8]); }
		}
		private string Set2string
		{
			get { return String.Format("{0}{1}{2}", nodes[2], nodes[8], nodes[9]); }
		}
		private string Set3string
		{
			get { return String.Format("{0}{1}{2}", nodes[3], nodes[9], nodes[5]); }
		}
		private string Set4string
		{
			get { return String.Format("{0}{1}{2}", nodes[4], nodes[5], nodes[6]); }
		}
		public long SolutionSet
		{
			get
			{
				long result = 0;
				for (int i = 0; i < 5; i++ )
					result += sets[i];
				return result;
			}
		}
		public string SolutionSetString
		{
			get
			{
				if (solutionSetString == null)
				{
					int lowestIdx = -1;
					int lowestVal = 9999;
					for (int i = 0; i < 5; i++)
					{
						int thisVal = sets[i];
						if (thisVal < lowestVal)
						{
							lowestVal = thisVal;
							lowestIdx = i;
						}
					}
					int idx = lowestIdx;
					StringBuilder sb = new StringBuilder();
					for (int cntr = 1; cntr <= 5; cntr++)
					{
						switch (idx)
						{
							case 0:
								sb.AppendFormat(Set0string);
								break;
							case 1:
								sb.AppendFormat(Set1string);
								break;
							case 2:
								sb.AppendFormat(Set2string);
								break;
							case 3:
								sb.AppendFormat(Set3string);
								break;
							case 4:
								sb.AppendFormat(Set4string);
								break;
						}
						idx++;
						if (idx > 4) idx = 0;
					}
					solutionSetString = sb.ToString();
				}
				return solutionSetString;
			}
		}
	}
}
