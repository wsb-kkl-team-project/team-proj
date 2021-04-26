using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
	public class Validator
	{
		public static bool CheckForSquareGraph(Dictionary<string, double[]> graph)
		{
			foreach (var node in graph)
			{
				if(graph.Count != node.Value.Length)
				{
					return false;
				}
			}
			return true;
		}
	}
}
