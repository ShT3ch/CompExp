using System;
using System.Collections.Generic;
using System.Linq;
using ComExp.Shapes;
using ComExp.Shapes.Domains;
using ComExp.Shapes.Functions.FuncInterfaces;

namespace ComExp.Methods
{
	public class MethodOfFixedChords : INumericMethod<IFunction>
	{
		public double ComputeNext(IEnumerable<double> previuosPoints, IFunction analyzedFunction)
		{
			if (!previuosPoints.IsEnoughOfParams(2))
				MethodsHelper.ThrowGreedlyException(previuosPoints, 2);

			return MethodStep(previuosPoints.First(), previuosPoints.Last(), analyzedFunction.Compute);
		}

		public IEnumerable<IShape> GenerateIllustrationForCurrentStep(IEnumerable<double> actualPoints, IFunction analyzedFunction)
		{
			if (!actualPoints.IsEnoughOfParams(3))
				MethodsHelper.ThrowGreedlyException(actualPoints, 3);

			var start = actualPoints.First();
			var lastPoints = actualPoints.Numerate().GetLastN(2);

			yield return new LineShape(new SegmentDomain(start, lastPoints[0].Value),
				new Point2D
				{
					X = start,
					Y = analyzedFunction.Compute(start)
				},
				new Point2D() {X = lastPoints[0].Value, Y = analyzedFunction.Compute(lastPoints[1].Value)},
				string.Format("Chord of {0} step", lastPoints[1].Number)
				);

			yield return new VerticalSegment(0,
				analyzedFunction.Compute(lastPoints[1].Value),
				lastPoints[1].Value, string.Format("Step of {0}",
					lastPoints[1].Number));
		}

		private Func<double, double, Func<double, double>, double> MethodStep = (x0, xi, f) => xi - (f(xi) * (xi - x0)) / (f(xi) - f(x0)); //https://ru.wikipedia.org/wiki/Метод_хорд
	}
}