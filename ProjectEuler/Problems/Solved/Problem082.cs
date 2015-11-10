using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using EMK.Cartography;

namespace ProjectEuler.Problems
{

	//NOTE: This problem is a more challenging version of Problem 81.

	//The minimal path sum in the 5 by 5 matrix below, by starting in any cell in the left column and finishing 
	//in any cell in the right column, and only moving up, down, and right, is indicated in red; the sum is equal to 994.
			
	//    131		673		234		103		18

	//    201		96		342		965		150

	//    630		803		746		422		111

	//    537		699		497		121		956

	//    805		732		524		37		331
	
	//		==>> 201, 96, 342, 234, 103, 18


	//Elapsed time for startY=79 in 00:01:20.4454831
	//Lowest Path Cost: 260324
	//02:01:34.2788996

	class Problem082
	{
		private static int[,] fileNodes;
		const string fileName = @"..\..\Data\Problem082Data.txt";
		const int squareSize = 80;
		//const string fileName = @"..\..\Data\TestData.txt";
		//const int squareSize = 5;
		const int squareSize_1 = squareSize - 1;

		private static Graph g;
		private static Node[,] nodes;

		public static void Solve()
		{

			Stopwatch sw = new Stopwatch();

			ReadData();
			sw.Start();
			ParseDataIntoGraph();

			Console.WriteLine("Parsed data into graph in {0}", sw.Elapsed);
			sw.Stop();

			AStar a = new AStar(g);

			a.ChoosenHeuristic = AStar.MaxAlongAxisHeuristic;
			//a.ChoosenHeuristic = AStar.ManhattanHeuristic;
			//a.ChoosenHeuristic = AStar.EuclidianHeuristic;

			int lowestPathCost = Int32.MaxValue;

			for (int start = 0; start < squareSize; start++)
			{
				Stopwatch swx = new Stopwatch();
				swx.Start();
				for (int end = 0; end < squareSize; end++)
				{

					Stopwatch sw1 = new Stopwatch();
					sw1.Start();
					if (a.SearchPath(nodes[0, start], nodes[squareSize_1, end]))
					{
						double pathCost = 0;
						bool isFirst = true;
						foreach (Arc arc in a.PathByArcs)
						{
							if (isFirst)
							{
								isFirst = false;
								pathCost += fileNodes[(int)arc.StartNode.X, (int)arc.StartNode.Y];
							}
							pathCost += arc.Cost;
							//Console.WriteLine(arc.ToString());
						}
						Console.WriteLine("{3} Path Cost from {0,2} to {1,2} is {2}", start, end, pathCost, sw1.Elapsed);
						if (pathCost < lowestPathCost)
						{
							lowestPathCost = (int)pathCost;
						}
					}
					else
					{
						Console.WriteLine("No results.");
					}


				}
				Console.WriteLine("\tElapsed time for startY={0,2} in {1}", start, swx.Elapsed);
			}
			Console.WriteLine("Lowest Path Cost: {0}", lowestPathCost);
		}


		static private string ListNodesAndArcs(Graph GraphToDescribe)
		{
			StringBuilder SB = new
				StringBuilder("Description of the Graph:\n\tNodes> ");
			foreach (Node N in GraphToDescribe.Nodes)
				SB.Append(N.ToString() + "; ");
			SB.Append("\n\tArcs> ");
			foreach (Arc A in GraphToDescribe.Arcs)
				SB.Append(A.ToString() + "; ");
			return SB.ToString();
		}

		private static void ParseDataIntoGraph()
		{
			g = new Graph();


			// LOAD NODES
			for (int y = 0; y <= squareSize_1; y++)
			{
				for (int x = 0; x <= squareSize_1; x++)
				{
					g.AddNode(nodes[x, y]);
				}

			}

			// LOAD ARCS
			for (int y = 0; y <= squareSize_1; y++)
			{
				for (int x = 0; x <= squareSize_1; x++)
				{
					// check up
					if (y > 0)
					{
						g.AddArc(nodes[x, y], nodes[x, y - 1], fileNodes[x, y - 1]);
					}

					// check down
					if (y < squareSize_1)
					{
						g.AddArc(nodes[x, y], nodes[x, y + 1], fileNodes[x, y + 1]);
					}

					// check left
					if (x > 0)
					{
						g.AddArc(nodes[x, y], nodes[x - 1, y], fileNodes[x - 1, y]);
					}

					// check right
					if (x < squareSize_1)
					{
						g.AddArc(nodes[x, y], nodes[x + 1, y], fileNodes[x + 1, y]);
					}
				}

			}

		}

		private static void ReadData()
		{
			fileNodes = new int[squareSize, squareSize];
			nodes = new Node[squareSize, squareSize];
			string[] lines = File.ReadAllLines(fileName);

			// create raw fileNodes
			for (int y = 0; y < squareSize; y++)
			{
				string[] vals = lines[y].Split(',');
				for (int x = 0; x < squareSize; x++)
				{
					int val = Int32.Parse(vals[x]);
					fileNodes[x, y] = val;
					nodes[x, y] = new Node(x, y, 0);
				}
			}
		}



		//static readonly Stopwatch sw = new Stopwatch();

		//const string fileName = @"..\..\Data\Problem082.txt";
		//const int squareSize = 80;
		////const string fileName = @"..\..\Data\Problem082Test.txt";
		////const int squareSize = 5;
		//const int squareSize_1 = squareSize-1;

		//private static int[,] fileNodes;

		//private static long lowestFound = 265000;//21 000 000;


		//public static void Solve()
		//{
		//    // load up the fileNodes
		//    ReadData();

		//    sw.Start();


		//    for (int startY = 0; startY < squareSize; startY++)
		//    {
		//        Console.WriteLine();
		//        Console.WriteLine("{1} Starting Y={0}",startY, sw.Elapsed);

		//        Point startLocation = new Point(0,startY);
		//        Point prevLoc = new Point(-1, -1);
		//        long startingSum = 0;
		//        DoNextLocation(startLocation, prevLoc, startingSum);
		//    }


		//    sw.Stop();

		//    Console.WriteLine();
		//    Console.WriteLine("Lowest path is {0} Found in {1}", lowestFound, sw.Elapsed);
		//}




		//private static void DoNextLocation(Point thisLoc, Point prevLoc, long currentSum)
		//{

		//    currentSum += fileNodes[thisLoc.X, thisLoc.Y];

		//    //make sure we don't need to check any further
		//    if(currentSum>lowestFound)
		//        return;

		//    //check for the far right column
		//    if (thisLoc.X == squareSize_1)
		//    {
		//        //Console.WriteLine("At End {0} has {1}", thisLoc,currentSum);
		//        // check to see if this path is the lowest
		//        if(currentSum<lowestFound)
		//        {
		//            lowestFound = currentSum;
		//            Console.WriteLine("\t{1} New Lowest: {0}", lowestFound,sw.Elapsed);
		//        }
		//        return;
		

		//    }

		//    // check up
		//    if(thisLoc.Y>0)
		//    {

		//        Point nextLoc = new Point(thisLoc.X, thisLoc.Y - 1);
		//        if (nextLoc != prevLoc)
		//            DoNextLocation(nextLoc, thisLoc, currentSum);
		//    }

		//    // check down
		//    if (thisLoc.Y < squareSize_1)
		//    {

		//        Point nextLoc = new Point(thisLoc.X, thisLoc.Y + 1);
		//        if (nextLoc != prevLoc)
		//            DoNextLocation(nextLoc, thisLoc, currentSum);
		//    }

		//    // check right
		//    if (thisLoc.X < squareSize_1)
		//    {
		//        Point nextLoc = new Point(thisLoc.X + 1, thisLoc.Y);
		//        DoNextLocation(nextLoc, thisLoc, currentSum);
		//    }

		//}

		//private static void ReadData()
		//{
		//    int totalSum = 0;
		//    fileNodes = new int[squareSize, squareSize];
		//    string[] lines = File.ReadAllLines(fileName);

		//    // create raw fileNodes
		//    for (int y = 0; y < squareSize; y++)
		//    {
		//        string[] vals = lines[y].Split(',');
		//        for (int x = 0; x < squareSize; x++)
		//        {
		//            int val = Int32.Parse(vals[x]);
		//            totalSum += val;
		//            fileNodes[x, y] = val;
		//        }
		//    }
		//    double avg = (double)totalSum / squareSize / squareSize;
		//    double shortestRouteAvg = squareSize * avg;
		//    Console.WriteLine(shortestRouteAvg);
		//}


	}
}
