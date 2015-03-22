using System;
using System.Collections.Generic;
using ComExp.Shapes;

namespace ComExp.Reporters
{
	public interface IReportGenerator
	{
		string GenerateReport(double Root, int N, double Error);
		void AddIntermidiateStep(IEnumerable<double> actualPoints, IEnumerable<IShape> pictureOfShape, int iterationNumber);
		void Init(IEnumerable<double> startPoints, IShape function, Conditions conditions);
	}
}
