using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComExp
{
	interface IReportGenerator
	{
		String GenerateReport();
		void AddIntermidiateStep(IEnumerable<double> actualPoints, IShape pictureOfShape);
		void Init(IEnumerable<double> startPoints, IShape function, Conditions conditions);
	}
}
