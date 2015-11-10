using System;

namespace ProjectEuler.Problems
{
	class Problem031
	{

		//In England the currency is made up of pound, £, and pence, p, and there are eight coins in 
		//general circulation:

		//    1p, 2p, 5p, 10p, 20p, 50p, £1 (100p) and £2 (200p).

		//It is possible to make £2 in the following way:

		//    1×£1 + 1×50p + 2×20p + 1×5p + 1×2p + 3×1p

		//How many different ways can £2 be made using any num1 of coins?


		//Start of Main 3/3/2008 7:19:45 AM
		//73682
		//End of Main (00:00:00.0828326) 3/3/2008 7:19:46 AM

		public static void Solve()
		{
			int cntr = 1; //auto include 2 Lb coin
			for ( int l1 = 0; l1 <= 2; l1++ )
			{
				for (int p50 = 0; p50 <= 4; p50++)
				{
					if (CashValue(l1, p50, 0, 0, 0, 0, 0) > 200)
						break;
					for (int p20 = 0; p20 <= 10; p20++)
					{
						if (CashValue(l1, p50, p20, 0, 0, 0, 0) > 200)
							break;
						for (int p10 = 0; p10 <= 20; p10++)
						{
							if (CashValue(l1, p50, p20, p10, 0, 0, 0) > 200)
								break;
							for (int p05 = 0; p05 <= 40; p05++)
							{
								if (CashValue(l1, p50, p20, p10, p05, 0, 0) > 200)
									break;
								for (int p02 = 0; p02 <= 100; p02++)
								{
									if (CashValue(l1, p50, p20, p10, p05, p02, 0) > 200)
										break;
									for (int p01 = 0; p01 <= 200; p01++)
									{
										if (CashValue(l1, p50, p20, p10, p05, p02, p01) > 200)
											break;
										if (CashValue(l1,p50,p20,p10,p05,p02,p01) == 200)
										{
											cntr++;
										}
									}
								}
							}
						}
					}
				}
			}
			Console.WriteLine(cntr);
		}

		public static int CashValue(int l1, int p50, int p20, int p10, int p05, int p02, int p01)
		{
			return (l1 * 100 + p50 * 50 + p20 * 20 + p10 * 10 + p05 * 5 + p02 * 2 + p01 * 1);
		}
	}
}
