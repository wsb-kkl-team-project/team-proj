using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
	public class Graph
	{
		public Dictionary<string, double[]> Data { get; set; } = new Dictionary<string, double[]>();
	}
}
