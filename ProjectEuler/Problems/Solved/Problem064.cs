using System;
using System.Collections.Generic;
using System.Text;
using ProjectEuler.Helpers;


namespace ProjectEuler.Problems
{
	// All square roots are periodic when written as continued fractions and can be written in the form:
	// 	                         1
	// √N = a_(0) +  -------------------------------------- 
	//                                    1	
	//                 a_(1) +  ---------------------------
	//                              	          1
	//                            a_(2) +  ----------------
	//                                        a_(3) + ...

	//For example, let us consider √23:
	//	
	//  √23  = 4 +  	
	//          1 +  		
	//           3 +  
	//             1 + 
	//              8 + 

	// It can be seen that the sequence is repeating. For conciseness, we use the notation 
	// √23 = [4;(1,3,1,8)], to indicate that the block (1,3,1,8) repeats indefinitely.

	//The first ten continued fraction representations of (irrational) square roots are:

	//√2=[1;(2)], period=1
	//√3=[1;(1,2)], period=2
	//√5=[2;(4)], period=1
	//√6=[2;(2,4)], period=2
	//√7=[2;(1,1,1,4)], period=4
	//√8=[2;(1,4)], period=2
	//√10=[3;(6)], period=1
	//√11=[3;(3,6)], period=2
	//√12= [3;(2,6)], period=2
	//√13=[3;(1,1,1,1,6)], period=5

	//Exactly four continued fractions, for N ≤ 13, have an odd period.

	//How many continued fractions for N ≤ 10000 have an odd period?

	/*
	Start of Main 1/22/2009 5:25:11 PM
	Problem 64 to 10000
	Count of odd period: 1322
	End of Main (00:00:00.2262284) 1/22/2009 5:25:11 PM
	*/
	class Problem064
	{

		public static void Solve(int MAX)
		{
			Console.WriteLine("Problem 64 to {0}", MAX);
			int count = 0;
			for (int i = 2; i <= MAX; i++)
			{
				//Console.WriteLine();
				//Console.WriteLine("Solving for {0}", i);
				double sqrt = Math.Sqrt(i);
				if (MiscFunctions.IsInteger(sqrt)) continue;
				int period = GetPeriod(i, sqrt);
				if (period % 2 == 1) count++;
				//Console.WriteLine();
				//Console.WriteLine("{0}: {1,3} {2}", i, period, (period % 2 == 1));
			}
			Console.WriteLine("Count of odd period: {0}", count);
		}


		//       1                  ( √23 -- first ) 
		// -------------  =  -----------------------------------
		//  √23 - first      ( √23 - first ) * ( √23 -- first )

		//       1                  ( √23 -- first ) 
		// -------------  =  -----------------------------------
		//  √23 - first      ( √23 - first ) * ( √23 -- first )

		private static int GetPeriod(int i, double sqrt)
		{
			int period = 0;
			int a = (int)Math.Floor(sqrt);
			int a2 = a*2, b = a, d = 1, an = 0, r = 0;
			//Console.Write("{0,5} [{1,2};",i , a);
			do
			{
				period++;
				d = (int) Math.Floor((decimal) (i - (b*b))/d);
				an = (int) Math.Floor((decimal) (a + b) / d);
				r = (a + b)%d;
				b = Math.Abs(r - a);
				//Console.Write("{0,2},",an);
			} while (an!=a2);
			//Console.WriteLine("...], period = {0}", period);
			return period;
		}

		/* // javascript stolen from http://home.earthlink.net/~usondermann/contfrac.html

		 var x=document.ContFrac.firstNum.value
		 var xx=x
		 var xa=Math.sqrt(x)
		 var a=parseInt(xa+" ")
		 var num=new Array()
		 num[0]=a
		 var b=a
		 var d=1
		 var period=0
		 var c=0
		 var r=0
		 var z=0
		 var ii=0
		 
		 do
		 {
		   d=Math.floor((x-(b*b))/d)
		   c=Math.floor((a+b)/d)
		   r=(a+b)%d
		   b=Math.abs(r-a)
		   s=s+c+","
		   num[(period+1)]=c
		   period++
		 } while(c!=(2*a))
		 
		 l=s.length
		 s=s.substring(0,(l-1))
		 document.ContFrac.resultNum.value=s+"...("+ period +" Repeating Digits)"
		 * 
		*/


	}
}
