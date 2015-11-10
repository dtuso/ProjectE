using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace ProjectEuler.Problems
{
	class Problem166
	{

		//A 4x4 grid is filled with digits d, 0  d  9.

		//It can be seen that in the grid

		//6 3 3 0
		//5 0 4 3
		//0 7 1 4
		//1 2 4 5

		//the sum of each row and each column has the value 12. Moreover the sum of each diagonal is also 12.

		//In how many ways can you fill a 4x4 grid with the digits d, 0 d 9 so that each row, each column, and both diagonals have the same sum? 


		//7130034


		public static void Solve()
		{
			Stopwatch sw = new	Stopwatch();
			sw.Start();
			long totSolved = 0;
			for (int sum = 9*4; sum >=0; sum--)
			{
				// now count each for a sum of sum
				long subSolved = 0;
				// a b c d
				// e f g h 
				// i j k l
				// m n o p

				for (int a = 0; a < 10; a++)
				{
					if(a + 27 < sum) continue;
					if (a > sum) break;
					for (int b = 0; b < 10; b++)
					{
						if (a + b + 18 < sum) continue;
						if (a + b > sum) break;
						for (int c = 0; c < 10; c++)
						{
							if (a + b + c + 9 < sum) continue;
							if (a + b + c  > sum) break;
							for (int d = 0; d < 10; d++)
							{
								if (a + b + c + d < sum) continue;
								if (a + b + c + d > sum) break;
								for (int e = 0; e < 10; e++)
								{
									if (e + 27 < sum) continue;
									if (a + e + 18 < sum) continue;
									if (a + e > sum) break;
									for (int f = 0; f < 10; f++)
									{
										if (a + f + 18 < sum) continue; //diag
										if (e + f + 18 < sum) continue;
										if (e + f > sum) break;
										if (b + f + 18 < sum) continue;
										if (b + f > sum) break;
										for (int g = 0; g < 10; g++)
										{
											if (e + f + g + 9 < sum) continue;
											if (e + f + g > sum) break;
											if (c + g + 18 < sum) continue;
											if (c + g > sum) break;
											for (int h = 0; h < 10; h++)
											{
												if (e + f + g + h < sum) continue;
												if (e + f + g + h > sum) break;
												if (d + h + 18 < sum) continue;
												if (d + h > sum) break;
												for (int i = 0; i < 10; i++)
												{
													if (i + 27 < sum) continue;
													if (a + e + i > sum) break;
													if (a + e + i + 9 < sum) continue;
													for (int j = 0; j < 10; j++)
													{
														if (i + j + 18 < sum) continue;
														if (i + j > sum) break;
														if (b + f + j + 9 < sum) continue;
														if (b + f + j > sum) break;
														for (int k = 0; k < 10; k++)
														{
															if (a + f + k + 9 < sum) continue; //diag
															if (i + j + k + 9 < sum) continue;
															if (i + j + k > sum) break;
															if (c + g + k + 9 < sum) continue;
															if (c + g + k > sum) break;
															for (int l = 0; l < 10; l++)
															{
																if (i + j + k + l > sum) break;
																if (d + h + l + 9 < sum) continue;
																if (d + h + l > sum) break;
																for (int m = 0; m < 10; m++)
																{
																	if (m + 27 < sum) continue;
																	if (a + e + i + m > sum) break;
																	if (d + g + j + m > sum) break; // diag
																	for (int n = 0; n < 10; n++)
																	{
																		if (m + n + 18 < sum) continue;
																		if (m + n > sum) break;
																		if (b + f + j + n > sum) break;
																		for (int o = 0; o < 10; o++)
																		{
																			if (m + n + o + 9 < sum) continue;
																			if (m + n + o > sum) break;
																			if (c + g + k + o > sum) break;
																			for (int p = 0; p < 10; p++)
																			{
																				if (m + n + o + p > sum) break;
																				if (d + h + l + p > sum) break;
																				if (a + f + k + p > sum) break; //diag
																				if (
																					(a + b + c + d == sum) &&
																					(e + f + g + h == sum) &&
																					(i + j + k + l == sum) &&
																					(m + n + o + p == sum) &&

																					(a + e + i + m == sum) &&
																					(b + f + j + n == sum) &&
																					(c + g + k + o == sum) &&
																					(d + h + l + p == sum) &&

																					(a + f + k + p == sum) &&
																					(d + g + j + m == sum))
																				{
																					subSolved = checked(subSolved + 1);
																					//Console.WriteLine();
																					//Console.WriteLine("{0} {1} {2} {3}", a, b, c, d);
																					//Console.WriteLine("{0} {1} {2} {3}", e,f,g,h);
																					//Console.WriteLine("{0} {1} {2} {3}", i,j,k,l);
																					//Console.WriteLine("{0} {1} {2} {3}", m,n,o,p);

																				}
																			}
																		}
																	}
																}
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
				Console.WriteLine("At {2}, for {0,2} there were {1,8} solutions.", sum,subSolved,sw.Elapsed);
				totSolved = checked(totSolved +subSolved);
			}
			Console.WriteLine("totSolved {0,8}.", totSolved);

		}
	}
}
