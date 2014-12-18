using System.Collections.Generic;
using System.Linq;
using ComExp.Methods;
using ComExp.Shapes;

namespace ComExp.Reporters
{
	public class SimpleReporter : IReportGenerator
	{
		public string GenerateReport(double Root, int N, double Error)
		{
			return report;
		}

		public void AddIntermidiateStep(IEnumerable<double> actualPoints, IEnumerable<IShape> pictureOfShape, int iterationNumber)
		{
			var numerated = actualPoints.Numerate();

			report += string.Format("Step N{0} X = {1}\r\n", numerated.Last().Number, numerated.Last().Value);
		}

		public void Init(IEnumerable<double> startPoints, IShape function, Conditions conditions)
		{
			report = string.Format("Started at ({0}) with epsilon {1}, Root = {2}\r\n",
									string.Join(", ", conditions.InitialPoints),
									conditions.Epsilon,
									conditions.RootPoint);
		}

		private string report;
	}
}