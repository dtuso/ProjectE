using System;
using System.Collections.Generic;
using System.Text;
using ProjectEuler.Helpers;

namespace ProjectEuler.Problems
{
	class p104
	{
		/*
		The Fibonacci sequence is defined by the recurrence relation:

			F_(n) = F_(n−1) + F_(n−2), where F_(1) = 1 and F_(2) = 1.

		It turns out that F_(541), which contains 113 digits, is the first 
		Fibonacci number for which the last nine digits are 1-9 pandigital 
		(contain all the digits 1 to 9, but not necessarily in order). And 
		F_(2749), which contains 575 digits, is the first Fibonacci number 
		for which the first nine digits are 1-9 pandigital.

		Given that F_(k) is the first Fibonacci number for which the first 
		nine digits AND the last nine digits are 1-9 pandigital, find k.
Starting at 1/20/2009 9:23:08 AM
Problem 104
At F( 4589)   495217638 True   452911189 False
At F( 7102)   759864321 True   984519551 False
At F( 7727)   314782956 True   397498593 False
At F( 8198)   853472961 True   416719849 False
At F( 9383)   381542976 True   597655697 False
At F(12633)   618549237 True   694358178 False
At F(15708)   268143975 True   479433296 False
At F(19014)   219536487 True   980031752 False
At F(21206)   276958143 True   703716233 False
At F(21303)   517863429 True   647303202 False
At F(21434)   123479856 True   849172337 False
At F(21566)   476392185 True   141117913 False
At F(22706)   839217465 True   752923233 False
At F(22890)   238561479 True   631966520 False
At F(25790)   276539418 True   413104345 False
At F(28244)   198347265 True   473617133 False
At F(29877)   375182649 True   425055682 False
At F(32174)   415769283 True   648438457 False
At F(32717)   125643798 True   869963797 False
At F(34433)   527146983 True   732429853 False
At F(34883)   583942167 True   566962697 False
At F(37965)   734982651 True   506281090 False
At F(44691)   328961754 True   396904834 False
At F(47422)   182974653 True   143222911 False
At F(48635)   581297634 True    False
At F(54473)   682719543 True   712005293 False
At F(60438)   278943615 True   478956744 False
At F(60536)   843927561 True   840559477 False
At F(63902)   239167548 True   914882201 False
At F(68340)   734259681 True    False
At F(72424)   235164879 True   878932043 False
At F(73147)   294738156 True   419235973 False
At F(75873)   147823569 True   900238818 False
At F(77706)   175423698 True   718247608 False
At F(78159)   823169547 True   269652066 False
At F(82694)   472539186 True   235978217 False
At F(87740)   168294735 True   947751805 False
At F(89348)   189756234 True   272138701 False
At F(89814)   463912857 True   218462152 False
At F(96471)   789146352 True   989848354 False
At F(96809)   342751968 True   747065709 False
At F(96921)   874169532 True   898781346 False
At F(97552)   649825371 True   952475099 False
At F(97664)   165734289 True   712575173 False
At F(97738)   483612759 True   868526519 False
At F(98884)   152873946 True   230034563 False
At F(102787)   729614358 True   865986933 False
At F(103028)   169478523 True   113430061 False
At F(108113)   853621497 True   485254333 False
At F(109925)   413875269 True   328009925 False
At F(112760)   124976853 True   523395445 False
At F(118795)   217389465 True   454431205 False
At F(122673)   618453972 True   459243618 False
At F(123766)   163982754 True   744545263 False
At F(128455)   143827956 True   637884045 False
At F(129979)   451863279 True   647602821 False
At F(130821)   419372685 True   679943746 False
At F(132269)   172465893 True   822618069 False
At F(134521)   753128469 True   182122321 False
At F(143421)   735982164 True   855225346 False
At F(144255)   145398276 True   778246370 False
At F(146364)   826975314 True   664586448 False
At F(149126)   138469752 True   531177193 False
At F(152874)   267319458 True   637989432 False
At F(154489)   875123964 True   830347089 False
At F(163157)   279856413 True   572009637 False
At F(165755)   249358761 True   702146745 False
At F(169668)   146372985 True   325479216 False
At F(171577)   132698547 True   637346257 False
At F(172633)   651349782 True   408977553 False
At F(172678)   165294378 True   238330639 False
At F(177423)   732168549 True   264822882 False
At F(179567)   859234617 True   977088353 False
At F(180866)   256481739 True   219821313 False
At F(183400)   963245178 True   944825675 False
At F(190447)   524369817 True   671258173 False
At F(194953)   261784935 True    False
At F(214033)   798216354 True    False
At F(216366)   295314876 True   677779688 False
At F(221250)   146527893 True   496295000 False
At F(222970)   421367985 True   292941335 False
At F(228167)   541287396 True   441708753 False
At F(232143)   465892317 True    False
At F(232366)   187296354 True    False
At F(235478)   438597126 True   903761289 False
At F(236282)   465723918 True   292755441 False
At F(236912)   213964587 True   528972069 False
At F(237628)   923617854 True    False
At F(237726)   279435168 True    False
At F(240188)   941567283 True   927968381 False
At F(242616)   248795136 True   892785312 False
At F(244016)   951783642 True   829635237 False
At F(244572)   149853267 True   430493264 False
At F(245640)   236845971 True   221136480 False
At F(250448)   153829746 True   481020901 False
At F(259894)   192437685 True   999475567 False
At F(262799)   247391658 True   448535201 False
At F(262866)   248631957 True   979921688 False
At F(264896)   436985127 True   275753797 False
At F(267448)   948253761 True   958348651 False
At F(268722)   168724539 True   187670936 False
At F(270101)   263714589 True   442904101 False
At F(270361)   572691348 True   922431761 False
At F(272643)   465281379 True   974888962 False
At F(273428)   528461397 True   199370861 False
At F(275627)   193568274 True   645719193 False
At F(276485)   396487512 True   751636485 False
At F(276960)   736815294 True   334126720 False
At F(277884)   937428651 True   331060688 False
At F(278336)   271863945 True    False
At F(278652)   298157634 True   613225424 False
At F(280409)   463158729 True   975343309 False
At F(284058)   182653974 True   299952504 False
At F(287894)   867395421 True   449495817 False
At F(288111)   194327856 True   433206114 False
At F(288692)   513279648 True   402963629 False
At F(289109)   721439685 True   535737509 False
At F(290414)   386429751 True   173355577 False
At F(292855)   531986247 True   461369645 False
At F(294063)   152394687 True   852735842 False
At F(297914)   987125634 True   863390577 False
At F(300445)   875164923 True   394287945 False
At F(302413)   169732854 True   238486633 False
At F(305561)   132695874 True   133232461 False
At F(308500)   217536489 True   931443875 False
At F(308563)   318972654 True   544885717 False
At F(309329)   387512649 True   969828029 False
At F(310064)   156387294 True   868983973 False
At F(311434)   321564879 True   905036087 False
At F(312461)   137269845 True   675667861 False
At F(315817)   316297548 True   263222897 False
At F(315965)   269318745 True   531230965 False
At F(317784)   379126548 True   592019488 False
At F(320465)   749286153 True   605547965 False
At F(322658)   152947638 True   794100929 False
At F(325047)   285769143 True   289905698 False
At F(329468)   245681739 True   352786941 True

Solution found at 329468
End of main (00:00:00.5147785) 1/20/2009 9:23:08 AM

		 */

		/* USED BIGINT TO FIND THIS BIG STRING

		//Starting at 1/7/2009 1:42:38 PM
		//  2749 143726895 True 002250249 False
		//88828106537442795144856947481258123757328497512971082550849594869904343729166347445778189142607018696009097838888100249857961468963109397474633021259193191710194820901979749019986070363344994730510615347898195622449135490767124166811351357698548796543254781165934697095774209967295831730032487635917361084208373719802974909743959166533870257037902949272302730609429217776205450052787466838982078405170627906728611609644633095784091348887335611424632603140717771518556838040482807011139566938279063367
		//93011366003791501139124180416150996570324585861472852993251848309768327376
		//14372689553387917661829645671564334144347634506448917723663290089702222530009397342095721197748390703442578832440606734797476053095767644629443572915711792647722348302386453121454440843797863921561581124399573134833792671117667661197245544556688422949193607193895988306702702760603047336208386100938317422813175407356709232675779685357629997245797294804250463809150187026942349354902182628605422407739419382801150894021953277500195893045355811369520046888338772777218694864406890501694863448727599353
		//830662539700881454734823358742184362414868465995609763288002569665002250249
		 
		 */

		private static int startingI = 2749;

		private static ulong lastFib_top = 88828106537;
		private static ulong lastFib_bot = 768327376;
		private static ulong thisFib_top = 143726895534;
		private static ulong thisFib_bot = 002250249;

		public static void Solve()
		{
			Console.WriteLine("Problem 104");
			int i = startingI;
			while (true)
			{
				NextFibonacci();
				i++;
				string thisFibStr_top = thisFib_top.ToString();
				string first9 = thisFibStr_top.Substring(0, 9);
				bool isPanFirst = IsPandigitial1to9(first9);

				if (isPanFirst)
				{
					string thisFibStr_bot = thisFib_bot.ToString();
					string last9 = "";
					bool isPanLast = false;
					if (thisFibStr_bot.Length == 9)
					{
						last9 = thisFibStr_bot.Substring(thisFibStr_bot.Length - 9);
						isPanLast = IsPandigitial1to9(last9);
					}
					Console.WriteLine("At F({0,5})   {1} {2}   {3} {4}", i, first9, isPanFirst, last9, isPanLast);
					if (isPanLast) break;
				}
			}
			Console.WriteLine();
			Console.WriteLine("Solution found at {0}", i);
		}


		private static void NextFibonacci()
		{
			ulong newFib_top = lastFib_top + thisFib_top;
			ulong newFib_bot = lastFib_bot + thisFib_bot;
			lastFib_top = thisFib_top;
			lastFib_bot = thisFib_bot;
			thisFib_top = newFib_top;
			thisFib_bot = newFib_bot;
			if (thisFib_top >= 1000000000000)
			{
				thisFib_top = (ulong)Math.Round((decimal)thisFib_top / 10);
				lastFib_top = (ulong)Math.Round((decimal)lastFib_top / 10);
			}
			if (thisFib_bot >= 1000000000)
			{
				thisFib_bot = ulong.Parse(thisFib_bot.ToString().Substring(thisFib_bot.ToString().Length - 9));
			}
		}

		private static bool IsPandigitial1to9(string number)
		{
			// ensure no 0's
			if (number.Contains("0")) return false;
			// ensure every position is unique
			for (int idx = 0; idx < 8; idx++)
			{
				for (int idx2 = idx + 1; idx2 < 9; idx2++)
				{
					if (number[idx] == number[idx2]) return false;
				}
			}
			return true;
		}


	}
}
