using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace RoutePlanning
{
	public static class PathFinderTask
	{
		public static int[] FindBestCheckpointsOrder(Point[] checkpoints)
		{
			var bestOrder = MakeTrivialPermutation(checkpoints.Length);
			var currentPassing = new int[checkpoints.Length];
			currentPassing[0] = 0;

			var recursionOrderVer2 = MakeRecursion2(checkpoints, checkpoints[0], currentPassing, 0, 1);
			var recursionOrderVer3 = MakeRecursion3(checkpoints, bestOrder,  1);
			return recursionOrderVer2;
		}

		private static int[] MakeRecursion(Point[] checkpoints, Point currentPoint,int[] currentPassing, double currentLengthOfPath, int amountOfCompletedNodes)
		{
			if (amountOfCompletedNodes == checkpoints.Length)
				return currentPassing;
			
			amountOfCompletedNodes++;
			var mostNearNode = FindNearCheckpoint(checkpoints, currentPoint, currentPassing);

			currentPassing[amountOfCompletedNodes - 1] = mostNearNode;
			MakeRecursion(checkpoints, checkpoints[mostNearNode], currentPassing,
				currentLengthOfPath + currentPoint.DistanceTo(checkpoints[mostNearNode]), amountOfCompletedNodes);
			return currentPassing;
		}
		
		private static int FindNearCheckpoint(Point[] checkpoints,Point currentPoint,int[] currentPassing)
		{
			var minLengthOfPath = 10000d;
			var nearNode=0;
			for (var i = 0; i < checkpoints.Length; i++)
			{
				if (!currentPassing.Contains(i) && currentPoint.DistanceTo(checkpoints[i])<=minLengthOfPath)
				{
					minLengthOfPath = currentPoint.DistanceTo(checkpoints[i]);
					nearNode = i;
				}
			} 
			
			for (var i = 0; i < checkpoints.Length; i++)
			{
				if (currentPoint.DistanceTo(checkpoints[i]) == minLengthOfPath)
					Console.Write(i + " | ");
			}
			Console.WriteLine();
			return nearNode;
		}
		
		private static int[] MakeTrivialPermutation(int size)
		{
			var bestOrder = new int[size];
			for (var i = 0; i < bestOrder.Length; i++)
				bestOrder[i] = i;
			return bestOrder;
		}
		
		private static int[] MakeRecursion2(Point[] checkpoints, Point currentPoint,int[] currentPassing, double currentLengthOfPath, int amountOfCompletedNodes)
		{
			if (amountOfCompletedNodes == checkpoints.Length)
				return currentPassing;
			
			var dictionaryOfAllPaths=new Dictionary<int[],double>();
			amountOfCompletedNodes++;
			
			var mostNearNodes = FindNearCheckpoints(checkpoints, currentPoint, currentPassing);
			foreach (var nextNode in mostNearNodes)
			{
				currentPassing[amountOfCompletedNodes - 1] = nextNode;
				var order=MakeRecursion2(checkpoints, checkpoints[nextNode], currentPassing,
                                     					currentLengthOfPath + currentPoint.DistanceTo(checkpoints[nextNode]), amountOfCompletedNodes);
				if (!dictionaryOfAllPaths.ContainsKey(order) )
				{
					dictionaryOfAllPaths.Add(order, PointExtensions.GetPathLength(checkpoints,order));
				}
				
				if (dictionaryOfAllPaths.ContainsKey(order) && dictionaryOfAllPaths[order]<PointExtensions.GetPathLength(checkpoints,order))
				{
					dictionaryOfAllPaths.Remove(order);
					dictionaryOfAllPaths.Add(order, PointExtensions.GetPathLength(checkpoints,order));
				}
			}
			var max = dictionaryOfAllPaths.Max(s => s.Value);
			var result = dictionaryOfAllPaths.Where(s => s.Value.Equals(max)).Select(s => s.Key).ToList();
			return result[0];
		}
		
		private static List<int> FindNearCheckpoints(Point[] checkpoints,Point currentPoint,int[] currentPassing)
		{
			var minLengthOfPath = 10000d;
			var nearNodes=new List<int>();
			for (var i = 0; i < checkpoints.Length; i++)
			{
				if (!currentPassing.Contains(i) && currentPoint.DistanceTo(checkpoints[i])<=minLengthOfPath)
				{
					minLengthOfPath = currentPoint.DistanceTo(checkpoints[i]);
				}
			}

			for (var i = 0; i < checkpoints.Length; i++)
			{
				if (currentPoint.DistanceTo(checkpoints[i]) == minLengthOfPath)
				{
					Console.Write(i + " | ");
					nearNodes.Add(i);
				}
			}
			Console.WriteLine();
			return nearNodes;
		}

		private static int[] MakeRecursion3(Point[] checkpoints,int[] order, int position )
		{
			var newOrder = order;
			for (var i = 0; i < checkpoints.Length; i++)
			{
				var index = Array.IndexOf(order, i, 1, position - 1);
				newOrder = MakeRecursion3(checkpoints, order, i);
				if (PointExtensions.GetPathLength(checkpoints, newOrder)< PointExtensions.GetPathLength(checkpoints, order))
					order=newOrder;
			}
			
			/*if (PointExtensions.GetPathLength(checkpoints, newOrder)< PointExtensions.GetPathLength(checkpoints, order))
				return order;*/
			return newOrder;
			
		}
		
		/////
		///
		private static double MakePathPermutations(Point[] checkpoints, int[] shortestPath,
			int[] path, int position, double shortestDistance)
		{
			var numberOfCheckpoints = checkpoints.Length;
			var distance = checkpoints.GetPathLength(path);

			if (position == path.Length && distance < shortestDistance)
			{
				shortestDistance = distance;
				Array.Copy(path, shortestPath, numberOfCheckpoints);
				return shortestDistance;
			}

			for (var i = 0; i < numberOfCheckpoints; i++)
			{
				var index = Array.IndexOf(path, i, 0, position);

				if (index == -1)
				{
					path[position] = i;
					shortestDistance = MakePathPermutations(checkpoints, shortestPath, path,
						position + 1, shortestDistance);
				}
			}

			return shortestDistance;
		}
	}
}