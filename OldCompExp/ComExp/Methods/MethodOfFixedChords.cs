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
		public string Name
		{
			get { return "MethodOfFixedChords"; }
		}

		public string SrcImg
		{
			get { return Name + ".SVG"; }
		}

		public int StepSize { get; set; }

		public IEnumerable<double> ComputeNext(IEnumerable<double> previuosPoints, IFunction analyzedFunction)
		{
			if (!previuosPoints.IsEnoughOfParams(2))
				MethodsHelper.ThrowGreedlyException(previuosPoints, 2);

			yield return MethodStep(previuosPoints.First(), previuosPoints.Last(), analyzedFunction.Compute);
		}

		public IEnumerable<IShape> GenerateIllustrationForCurrentStep(IEnumerable<double> actualPoints, IFunction analyzedFunction, int iterationNumber)
		{
			if (!actualPoints.IsEnoughOfParams(3))
				MethodsHelper.ThrowGreedlyException(actualPoints, 3);

			var start = actualPoints.First();
			var lastPoints = actualPoints.GetLastN(2);

			yield return new LineShape(new UsualExpandingDomain().Update(start).Update(lastPoints[0]).Update(lastPoints[1]),
				new Point2D
				{
					X = start,
					Y = analyzedFunction.Compute(start)
				},
				new Point2D() { X = lastPoints[0], Y = analyzedFunction.Compute(lastPoints[0]) },
				string.Format("Chord of {0} step", iterationNumber)
				);

			yield return new VerticalSegment(0,
				analyzedFunction.Compute(lastPoints[1]),
				lastPoints[1], string.Format("Chord step N{0}", iterationNumber));
		}

		protected Func<double, double, Func<double, double>, double> MethodStep
		{
			get { return (x0, xi, f) => xi - (f(xi) * (xi - x0)) / (f(xi) - f(x0)); }//https://ru.wikipedia.org/wiki/Метод_хорд
		}
	}
}