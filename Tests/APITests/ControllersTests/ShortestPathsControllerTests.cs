using API;
using API.Controllers;
using API.Models;
using API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Tests.APITests.ControllersTests
{
	public class ShortestPathsControllerTests
	{
		private ShortestPathsController _controller;
		private AlgorithmDijkstra _realAlgorithm = new AlgorithmDijkstra();

		public ShortestPathsControllerTests()
		{
			_controller = new ShortestPathsController(_realAlgorithm);
		}

		[Fact]
		public void ComputePaths_With_InvalidBody()
		{
			Graph[] invalidBodys = new Graph[]
			{
				null,
				new Graph { Data = null },
				new Graph { Data = new Dictionary<string, double[]>() },
				new Graph 
				{ 
					Data = new Dictionary<string, double[]>
					{
						{ "a", new double[]{ 1, 2, 3, 4 } }
					}
				},
			};

			foreach (Graph graph in invalidBodys)
			{
				IActionResult result = _controller.ComputePaths(graph);
				Assert.IsType<BadRequestObjectResult>(result);
			}
		}

		[Fact]
		public void ComputePath_With_Valid()
		{
			Graph body = new Graph
			{
				Data = new Dictionary<string, double[]>
				{
					{ "A", new double[]{ 0, 1, 2, 3 } },
					{ "B", new double[]{ 1, 0, Constants.MATH_INFINITY, 2 } },
					{ "C", new double[]{ 2, Constants.MATH_INFINITY, 0, Constants.MATH_INFINITY } },
					{ "D", new double[]{ 3, 2, Constants.MATH_INFINITY, 0} },
				}
			};
			Graph expected = new Graph
			{
				Data = new Dictionary<string, double[]>
				{
					{ "A", new double[]{ 0, 1, 2, 3 } },
					{ "B", new double[]{ 1, 0, 3, 2 } },
					{ "C", new double[]{ 2, 3, 0, 5 } },
					{ "D", new double[]{ 3, 2, 5, 0 } },
				}
			};

			IActionResult result = _controller.ComputePaths(body);
			Assert.IsType<OkObjectResult>(result);
			var resultBody = (result as OkObjectResult).Value;
			Assert.IsType<Graph>(resultBody);
			Graph actual = resultBody as Graph;
			Assert.Equal(expected.Data.Count, actual.Data.Count);
			for (int i = 0; i < expected.Data.Count; i++)
			{
				var expectedElement = expected.Data.ElementAt(i);
				var actualElement = actual.Data.ElementAt(i);
				Assert.Equal(expectedElement.Key, actualElement.Key);
				for (int j = 0; j < expectedElement.Value.Length; j++)
				{
					Assert.Equal(expectedElement.Value[j], actualElement.Value[j]);
				}
			}
		}
	}
}
