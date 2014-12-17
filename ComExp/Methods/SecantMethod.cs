using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComExp.Shapes;
using ComExp.Shapes.Domains;
using ComExp.Shapes.Functions.FuncInterfaces;

namespace ComExp.Methods
{
	public class SecantMethod: INumericMethod<IFunction>
	{
		public double ComputeNext(IEnumerable<double> previuosPoints, IFunction analyzedFunction)
		{
			if (!previuosPoints.IsEnoughOfParams(2))
				MethodsHelper.ThrowGreedlyException(previuosPoints, 2);

			var lastPoints = previuosPoints.GetLastN(2);

			return MethodStep(lastPoints[0], lastPoints[1], analyzedFunction.Compute);
		}

		public IEnumerable<IShape> GenerateIllustrationForCurrentStep(IEnumerable<double> actualPoints, IFunction analyzedFunction, int iterationNumber)
		{
			if (!actualPoints.IsEnoughOfParams(3))
				MethodsHelper.ThrowGreedlyException(actualPoints, 3);

			var lastPoints = actualPoints.GetLastN(3);
			var start = lastPoints[0];

			yield return new LineShape(new UsualExpandingDomain().Update(start).Update(lastPoints[1]).Update(lastPoints[2]),
				new Point2D
				{
					X = start,
					Y = analyzedFunction.Compute(start)
				},
				new Point2D() { X = lastPoints[1], Y = analyzedFunction.Compute(lastPoints[1]) },
				string.Format("Secant of {0} step", iterationNumber)
				);

			yield return new VerticalSegment(0,
				analyzedFunction.Compute(lastPoints[2]),
				lastPoints[2], string.Format("Step of {0}", iterationNumber));
		}

		protected Func<double, double, Func<double, double>, double> MethodStep
		{
			get { return (xi_1, xi, f) => xi_1 - (f(xi_1) * (xi - xi_1)) / (f(xi) - f(xi_1)); }//https://ru.wikipedia.org/wiki/Метод_хорд
		} 

		
	}
}
