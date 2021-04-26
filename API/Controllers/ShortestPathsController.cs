using API.Models;
using API.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Controllers
{
	[Route("api/v1/paths")]
	public class ShortestPathsController : ControllerBase
	{
		private readonly IAlgorithmDistances _algorithm;

		public ShortestPathsController(IAlgorithmDistances algorithm)
		{
			_algorithm = algorithm;
		}

		[HttpPost]
		public IActionResult ComputePaths([FromBody] Graph body)
		{
			if(body is null || body.Data is null || body.Data.Count < 4 || !Validator.CheckForSquareGraph(body.Data))
			{
				return BadRequest("Given body is invalid. Graph must has at least 4 nodes.");
			}

			double[][] graph = new double[body.Data.Count][];
			for (int i = 0; i < graph.Length; i++)
			{
				graph[i] = body.Data.Values.ElementAt(i);
			}

			_algorithm.Prepare(graph);
			double[][] distancesArray = _algorithm.Find();
			Dictionary<string, double[]> distances = new Dictionary<string, double[]>();
			for (int i = 0; i < distancesArray.Length; i++)
			{
				string symbol = body.Data.Keys.ElementAt(i);
				distances.Add(symbol, distancesArray[i]);
			}
			return Ok(new Graph { Data = distances });
		}
	}
}
