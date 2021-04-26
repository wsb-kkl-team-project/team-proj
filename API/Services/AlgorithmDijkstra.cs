using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
	public class AlgorithmDijkstra : IAlgorithmDistances
	{
		private double[][] _graph;
		private double[] _distances;
		private bool[] _visited;
		private double _sum, _min;
		private int _foundIndex, _iIndex, _jIndex;

		public double[][] Find()
		{
			if (_graph == null) return null;

			double[][] result = new double[_graph.Length][];
			for (int source = 0; source < _graph.Length; source++)
			{
				_distances = new double[_graph.Length];
				_visited = new bool[_graph.Length];
				Array.Fill(_distances, Constants.MATH_INFINITY);
				_distances[source] = 0;
				for (_iIndex = 0; _iIndex < _graph.Length; _iIndex++)
				{
					_foundIndex = FindIndexOfMin(_distances, _visited);
					_visited[_foundIndex] = true;
					for (_jIndex = 0; _jIndex < _graph.Length; _jIndex++)
					{
						_sum = _distances[_foundIndex] + _graph[_foundIndex][_jIndex];
						if (_visited[_jIndex] == false && _graph[_foundIndex][_jIndex] != Constants.MATH_INFINITY && _sum < _distances[_jIndex])
							_distances[_jIndex] = _sum;
					}
				}
				result[source] = _distances;
			}
			return result;
		}

		public void Prepare(double[][] graph)
		{
			_graph = graph;
		}

		private int FindIndexOfMin(double[] distances, bool[] visited)
		{
			_min = Constants.MATH_INFINITY;
			_foundIndex = -1;
			for (_jIndex = 0; _jIndex < distances.Length; _jIndex++)
			{
				if (distances[_jIndex] < _min && visited[_jIndex] == false)
				{
					_min = distances[_jIndex];
					_foundIndex = _jIndex;
				}
			}
			return _foundIndex;
		}
	}
}
