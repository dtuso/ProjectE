using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectEuler.Helpers
{
	class MyMath
	{
		//http://www.topcoder.com/tc?module=Static&d1=tutorials&d2=primeNumbers
		public static int EulersTotient(int n)
		{
			int result = n;
			for (int i = 2; i * i <= n; i++)
			{
				if (n % i == 0) result -= result / i;
				while (n % i == 0) n /= i;
			}
			if (n > 1) result -= result / n;
			return result;
		} 

		public static int Phi(int n)
		{
			return EulersTotient(n);
		}


		public static BigInteger GetPascalNumber(int row, int element)
		{
			//            n!
			//[n:k] = --------
			//        k! (n-k)!
			//                        6 * 5 * 4 * 3 * 2 * 1
			//For example, [6:3] =  -------------------------  =  20. 
			//                      3 * 2 * 1   *   3 * 2 * 1
			// n = row-1;
			// k = element-1;
			int n = row - 1;
			int k = element - 1;

			BigInteger nFac = Factorial(n);
			BigInteger kFac = Factorial(k);
			BigInteger nkFac = Factorial(n - k);

			return (nFac/(kFac*nkFac));
		}


		public static double RightTriangleGetHypotenuse(double side1, double side2)
		{
			double s1 = checked(side1*side1);
			double s2 = checked(side2*side2);
			return Math.Sqrt(checked(s1 + s2));
		}

		public static double RightTriangleGetSide2(double side1, double hypotenuse)
		{
			double s1 = checked(side1 * side1);
			double hy = checked(hypotenuse * hypotenuse);
			return Math.Sqrt(checked(hy - s1));
		}

		//http://en.wikipedia.org/wiki/Polygonal_number
		public static long GetPolygonalNumber(long numSides, long iteration)
		{
			long sqr = checked(iteration*iteration);
			long top = checked((numSides - 2)*sqr - (numSides - 4)*iteration);
			return top/2;
		}

		public static long GetPolygonalIteration(long numSides, long poly)
		{
			long x = checked((8*(numSides - 2)*poly));
			long y = checked((numSides - 4)*(numSides - 4));
			long sqrt = checked((long)Math.Sqrt(x + y));
			long top = sqrt + numSides - 4;
			long bottom = 2*(numSides - 2);
			return top/bottom;
		}

		public static bool IsPolygonal(long numSides, long number)
		{
			// http://en.wikipedia.org/wiki/Polygonal_number

			long x = checked(8 * (numSides - 2) * number);
			long y = checked((numSides - 4) * (numSides - 4));
			long sqrt = checked((long)Math.Sqrt(x + y));
			long top = sqrt + numSides - 4;
			long bottom = 2 * (numSides - 2);
			return (top % bottom == 0);

			switch(numSides)
			{
				case 3:
					return IsTriangleNumber(number);
				case 4:
					return IsSquareNumber(number);
				case 5:
					return IsPentagonalNumber(number);
				case 6:
					return IsHexagonalNumber(number);
				case 7:
					return IsHeptagonalNumber(number);
				case 8:
					return IsOctagonalNumber(number);
				default:
					Console.WriteLine("Unable to determine with number sides = {0}",numSides);
					throw new ArgumentException("Out of bounds","numSides");
			}
		}

		public static long SumNToZero(int num)
		{
			long result = 0;
			for(int i = num;i>0;i--)
				result += i;
			return result;
		}

		public static double QuadraticPos(double a, double b, double c)
		{
			double mid;
			if (c == 0 || a == 0)
				mid = b;
			else
				mid = Math.Sqrt(Math.Pow(b, 2) - 4 * a * c);
			double den = 2 * a;
			double resPos = (-b + mid) / den;
			return resPos;
		}

		public static double QuadraticNeg(double a, double b, double c)
		{
			double mid;
			if (c == 0 || a == 0)
				mid = b;
			else
				mid = Math.Sqrt(Math.Pow(b, 2) - 4 * a * c);
			double den = 2 * a;
			double resNeg = (-b - mid) / den;
			return resNeg;
		}

		public static double QuadraticMaxVal(double a, double b, double c)
		{
			double mid;
			if (c==0 || a==0)
				mid = b;
			else 
				mid = Math.Sqrt(Math.Pow(b, 2) - 4 * a * c);
			double den = 2*a;
			double resPos = (-b + mid )/ den;
			double resNeg = (-b - mid )/ den;
			return Math.Max( resNeg,resPos);
		}

		public static double GetPercent(int numerator, int denominator)
		{
			return 100 * (numerator / (double)(denominator));
		}

		public static void GetNextPrime(ref int number)
		{
			long numberLong = (long) number;
			GetNextPrime(ref numberLong);
			number = checked((int) numberLong);
		}

		public static void GetNextPrime(ref long number)
		{
			if (number < 2)
			{
				number = 2;
				return;
			}
			if (number % 2 == 0)
				number--;

			do { number += 2; } while (!IsPrime(number));
		}

		public static void GetNextPrime(ref ulong number)
		{
			if (number < 2)
			{
				number = 2;
				return;
			}
			if (number % 2 == 0)
				number--;

			do { number += 2; } while (!IsPrime(number));
		}

		public static bool IsPrime(int number)
		{
			return IsPrime((long) number);
		}

		public static bool IsPrime(long number)
		{

			//Start of Main 3/19/2008 3:53:43 PM
			//End of Main (00:00:13.1176741) 3/19/2008 3:53:56 PM

			number = Math.Abs(number);
			if (number <= 1)
				return false;
			if (number <= 3)
				return true;
			if (number % 2 == 0)
				return false;
			if (number % 3 == 0)
				return false;
			if (number % 5 == 0 && number != 5)
				return false;
			if (number % 7 == 0 && number != 7)
				return false;
			double max = Math.Sqrt(number);
			for (double den = 11; den <= max; den += 2)
			{
				if (number % den == 0)
					return false;
			}
			return true;
		}

		public static bool IsPrime(ulong number)
		{

			//Start of Main 3/19/2008 3:53:43 PM
			//End of Main (00:00:13.1176741) 3/19/2008 3:53:56 PM
			if (number <= 1)
				return false;
			if (number <= 3)
				return true;
			if (number % 2 == 0)
				return false;
			if (number % 3 == 0)
				return false;
			if (number % 5 == 0 && number != 5)
				return false;
			if (number % 7 == 0 && number != 7)
				return false;
			double max = Math.Sqrt(number);
			for (double den = 11; den <= max; den += 2)
			{
				if (number % den == 0)
					return false;
			}
			return true;
		}

		public static BigInteger Factorial(int num)
		{
			BigInteger bi = new BigInteger(1);
			for(int i=1;i<=num;i++)
				bi *= i;
			return bi;
		}

		public static BigInteger CombinatoricsShortFactorial(int numerator, int highestDenom)
		{
			if (highestDenom > numerator) throw new ArgumentException("'highestDenom' has to be less than or equal to 'numerator' ");
			BigInteger bi = new BigInteger(1);
			for (int i = numerator; i > highestDenom; i--)
				bi *= i;
			return bi;
		}

		public static BigInteger Combinatorics(int n, int r)
		{
			//In general,
			//           n!
			// nCr = ----------- ,where r ≤ n, n! = n × (n−1) × ... × 3 × 2 × 1, and 0! = 1.
			//        r! (n−r)!
			//    
			if (r>n) throw new ArgumentException("'r' has to be less than or equal to 'n' ");
			BigInteger bi = CombinatoricsShortFactorial(n, Math.Max( (n-r),r));
			bi /= Factorial(Math.Min(( n-r),r ));
			return bi;
		}

		public static bool IsTriangleNumber(long num)
		{
			// http://en.wikipedia.org/wiki/Triangular_numbers
			long tri81 = checked(8 * num + 1);
			return (0 == ((Math.Sqrt(tri81)) - 1) % 2);
		}


		public static bool IsSquareNumber(long num)
		{
			double sqrt = checked(Math.Sqrt(num));
			return MiscFunctions.IsInteger(sqrt);
		}

		public static bool IsPentagonalNumber(long num)
		{
			// http://en.wikipedia.org/wiki/Pentagonal_numbers
			long pen241 = checked(24 * num + 1);
			double mod = ((1 + Math.Sqrt(pen241)) % 6);
			return (0 == mod);
		}

		public static bool IsHexagonalNumber(long num)
		{
			// http://en.wikipedia.org/wiki/Hexagonal_number
			long hex81 = checked(8 * num + 1);
			double mod = ((1 + Math.Sqrt(hex81)) % 4);
			return (0 == mod);
		}

		public static bool IsHeptagonalNumber(long num)
		{
			//http://en.wikipedia.org/wiki/Heptagonal_numbers
			//Five times a heptagonal number, plus 1 equals a triangular number.
			long tri51 = checked(5 * num + 1);
			return IsTriangleNumber(tri51);
		}

		public static bool IsOctagonalNumber(long num)
		{
			//http://en.wikipedia.org/wiki/Octagonal_number
			//P = 3*n^2 - 2*n

			long thisGuess;
			long thisGuessAns;

			long lo ;
			long hi;
			if (num <= 280)
			{
				lo = 0;
				hi = 11;
			}
			else if (num <= 29800)
			{
				lo = 9;
				hi = 101;
			}
			else if (num <= 2998000)
			{
				lo = 99;
				hi = 1001;
			}
			else if (num <= 299980000)
			{
				lo = 999;
				hi = 10001;
			}
			else if (num <= 29999800000)
			{
				lo = 9999;
				hi = 100001;
			}
			else if (num <= 2999998000000)
			{
				lo = 99999;
				hi = 1000001;
			}
			else if (num <= 299999980000000)
			{
				lo = 999999;
				hi = 10000001;
			}
			else if (num <= 29999999800000000)
			{
				lo = 9999999;
				hi = 100000001;
			}
			else if (num <= 2999999998000000000)
			{
				lo = 99999999;
				hi = 1000000001;
			}
			else
			{
				lo = 1000000000;
				hi = (long)(Math.Sqrt(long.MaxValue));
			}
			while (true)
			{
				long dif = Math.Abs(hi - lo);
				if(dif<2)
					return false;
				thisGuess = lo + dif/2;
				thisGuessAns = (3*thisGuess*thisGuess - 2*thisGuess);
				if (thisGuessAns == num)
				{
					return true;
				}
				else if (thisGuessAns > num)
				{
					hi = thisGuess;
				}
				else
				{
					lo = thisGuess;
				}
			}

		}


		public static long GetTriangleNumber(int n)
		{
			return n*((long) n + 1)/2;
		}


		public static long GetSquareNumber(int n)
		{
			return checked(n*n);
		}


		public static long GetPentagonalNumber(int n)
		{
			return n * (3 * (long)n - 1) / 2;// Pentagonal   P(n)=n(3n−1)/2   1, 5, 12, 22, 35, ...
		}


		public static long GetHexagonalNumber(int n)
		{
			return n*(2*(long) n - 1);
		}


		public static long GetHeptagonalNumber(int n)
		{
			return n*(5*(long) n - 3)/2;
		}


		public static long GetOctagonalNumber(int n)
		{
			return n*(3*(long) n - 2);
		}

		public static double GetPositionOfPentagonalNumber(long pentagonal)
		{
			// http://en.wikipedia.org/wiki/Pentagonal_numbers
			long pen241 = checked(24 * pentagonal + 1);
			return (1 + Math.Sqrt(pen241)) / 6;
		}

		public static void ReduceFraction(ref int numerator, ref int denominator)
		{
			//http://forums.devshed.com/python-programming-11/need-some-help-optimizing-a-function-to-reduce-fractions-66824.html
			//int min = Math.Min(numerator, denominator);
			//for (int reduce = numerator; reduce > 1; reduce--)
			//{
			//    if (numerator % reduce == 0 && denominator % reduce == 0)
			//    {
			//        numerator /= reduce;
			//        denominator /= reduce;
			//        reduce = Math.Min(numerator, denominator);
			//    }
			//}
			while(true)
			{
				int gcd = Divisors.GreatestCommonDivisor(numerator, denominator);
				if(gcd==1)
					return;
				numerator /= gcd;
				denominator /= gcd;
			}
		}

		public static void ReduceFraction(ref long numerator, ref long denominator)
		{
			while (true)
			{
				long gcd = Divisors.GreatestCommonDivisor(numerator, denominator);
				if (gcd == 1)
					return;
				numerator /= gcd;
				denominator /= gcd;
			}
		}

		public static void ReduceFraction(ref ulong numerator, ref ulong denominator)
		{
			while (true)
			{
				ulong gcd = Divisors.GreatestCommonDivisor(numerator, denominator);
				if (gcd == 1)
					return;
				numerator /= gcd;
				denominator /= gcd;
			}
		}

		public static void ReduceFraction(ref BigInteger numerator, ref BigInteger denominator)
		{

			while (true)
			{
				BigInteger gcd = numerator.gcd(denominator);
				if (gcd == 1)
					break;
				numerator /= gcd;
				denominator /= gcd;
			}

		}

		public static bool AreCoprime(int a, int b)
		{
			return (Divisors.GreatestCommonDivisor(a, b) == 1);
		}

		// http://en.wikipedia.org/wiki/Modular_exponentiation
		public static int ModPow(int bas, int exp, int mod)
		{
			int result = 1;

			while (exp > 0)
			{
				if ((exp & 1) == 1) result = checked(result * bas) % mod; // multiply in this bits' contribution while using modulus to keep result small
				exp >>= 1;
				bas = checked(bas * bas) % mod;
			}

			return result;
		}
		public static long ModPow(long bas, long exp, long mod)
		{
			long result = 1;

			while (exp > 0)
			{
				if ((exp & 1) == 1) result = checked(result * bas) % mod; // multiply in this bits' contribution while using modulus to keep result small
				exp >>= 1;
				bas = checked(bas * bas) % mod;
			}

			return result;
		}

		

		//The nth root of a number A can be computed by the nth root algorithm. Start with an initial guess x0 and then iterate using the recurrence relation

		//x(k+1) = (1/n) * ( (n-1) * x(k) + ( A / (x(k)^(n-1) )
		public static string nthRoot(int radicand, int nthRoot)
		{
			double n = nthRoot;
			double xk = 0;
			double xk1 = (int)Math.Sqrt(radicand);
			for(int i = 1; i<10; i++)
			{
				xk = xk1;
				xk1 = (1/n) *((n - 1)*xk + (radicand/(Math.Pow(xk, (n - 1)))));
				Console.WriteLine(xk1);
			}
			return xk1.ToString();
		}

		public static string nthRootWithPrecision(int radicand, int nthRoot, int precision)
		{
			return nthRootWithPrecisionAndBaseSystem(radicand, nthRoot, precision, 10);
		}
		
		public static string nthRootWithPrecisionAndBaseSystem(int radicand, int nthRoot, int precision, int B_baseNumberSystem)
		{

			StringBuilder result = new StringBuilder();
			// found algorithm at http://en.wikipedia.org/wiki/Shifting_nth-root_algorithm
			BigInteger B = B_baseNumberSystem;
			BigInteger BtotheN = BigInteger.Pow(B, nthRoot);
			//int n = nthRoot;
			BigInteger y; // rootExtracted
			BigInteger y_p;
			BigInteger r; // remainder
			BigInteger r_p;
			BigInteger alpha; // next n digits
			BigInteger beta; //  next digit of Root;
			string endPadding = "";
			for (long i = 1; i <= nthRoot; i++)
				endPadding += "0";

			string numStr = radicand.ToString();

			while (numStr.Length % nthRoot != 0)
			{
				numStr = "0" + numStr;
			}

			y = 0;
			r = 0;
			for (int pos = 1; pos <= precision; pos++)
			{
				// grab n digits of the radicand
				alpha = new BigInteger( Int64.Parse(numStr.Substring(0, nthRoot)) );

				BigInteger bnra = BtotheN * r + alpha;
				BigInteger bnyn = BtotheN * BigInteger.Pow(y, nthRoot);
				BigInteger by = B * y;
				for(beta=0; beta < B; beta++)
				{
					BigInteger byb = by + (beta + 1);
					BigInteger left = BigInteger.Pow(byb, nthRoot) - bnyn;
					if(left>bnra)
					{
						break;
					}
				}
				if(B==beta)
				{
					throw new Exception("help!!!");
				}
				result.Append(beta);
				y_p = (by + beta);
				r_p = bnra - (BigInteger.Pow(y_p, nthRoot) - bnyn);
				y = y_p;
				r = r_p;

				// make sure there's enough info on the original number to keep checking stuff
				numStr = numStr.Substring(nthRoot) + endPadding;
			}


			return result.ToString();
		}

		public static decimal GuessSquareRoot(decimal num, int numIterations)
		{
			// Babylonian method
			
			decimal guess = num/ (num.ToString().Length);
			for (int i = 0; i < numIterations; i++)
			{
				decimal nextGuess = num / guess;
				guess = checked(guess + nextGuess) / 2;
			}
			return guess;
		}
	}
}
