using System;
using System.Collections.Generic;
using ComExp.Shapes;

namespace ComExp.Reporters
{
	public interface IReportGenerator
	{
		String GenerateReport();
		void AddIntermidiateStep(IEnumerable<double> actualPoints, IEnumerable<IShape> pictureOfShape);
		void Init(IEnumerable<double> startPoints, IShape function, Conditions conditions);
	}
}
