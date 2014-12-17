using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComExp.Shapes;

namespace ComExp
{
	interface IReportGenerator
	{
		String GenerateReport();
		void AddIntermidiateStep(IEnumerable<double> actualPoints, IEnumerable<IShape> pictureOfShape);
		void Init(IEnumerable<double> startPoints, IShape function, Conditions conditions);
	}
}
