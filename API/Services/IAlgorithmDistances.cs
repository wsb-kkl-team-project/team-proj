using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
	public interface IAlgorithmDistances
	{
		void Prepare(double[][] graph);
		double[][] Find();
	}
}
