using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using EMK.Cartography;

namespace ProjectEuler.Problems
{
	class Problem083
	{

		//In the 5 by 5 matrix below, the minimal path sum from the top left to the bottom right, 
		//by moving left, right, up, and down, is indicated in red and is equal to 2297.
					
		//    131		673		234		103		18

		//    201		96		342		965		150

		//    630		803		746		422		111

		//    537		699		497		121		956

		//    805		732		524		37		331

		// 2297 == 131, 201, 96, 342, 234, 103, 18, 150, 111, 422, 121, 37, 331


		//Start of Main 5/9/2008 10:26:44 AM
		//Parsed data into graph in 00:00:45.7057761
		//Lowest Path Cost: 425185
		//End of Main (00:00:47.7127526) 5/9/2008 10:27:31 AM


		private static int[,] fileNodes;
		const string fileName = @"..\..\Data\Problem083.txt";
		const int squareSize = 80;
		//const string fileName = @"..\..\Data\Problem083Test.txt";
		//const int squareSize = 5;
		const int squareSize_1 = squareSize - 1;

		private static Graph g;
		private static Node[,] nodes;
		public static void Solve()
		{

			Stopwatch sw = new Stopwatch();
			sw.Start();
			ReadData();

			ParseDataIntoGraph();

			Console.WriteLine("Parsed data into graph in {0}", sw.Elapsed);


			AStar a = new AStar(g);

			a.ChoosenHeuristic = AStar.MaxAlongAxisHeuristic;
			//a.ChoosenHeuristic = AStar.ManhattanHeuristic;
			//a.ChoosenHeuristic = AStar.EuclidianHeuristic;

			if (a.SearchPath(nodes[0, 0], nodes[squareSize_1, squareSize_1]))
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
				}

				Console.WriteLine("Lowest Path Cost: {0}", pathCost);

			}
			else
			{
				Console.WriteLine("No results.");
			}
			//Console.WriteLine(ListNodesAndArcs(g));
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
					if (x >0)
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
	}
}
