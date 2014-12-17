using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComExp.Shapes;
using ComExp.Shapes.Functions.FuncInterfaces;

namespace ComExp.Methods
{
	class NewTonMethod:INumericMethod<IDifferentiableOnce>
	{
		public double ComputeNext(IEnumerable<double> previuosPoints, IDifferentiableOnce analyzedFunction)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<IShape> GenerateIllustrationForCurrentStep(IEnumerable<double> actualPoints, IDifferentiableOnce analyzedFunction, int iterationNumber)
		{
			throw new NotImplementedException();
		}
	}
}
