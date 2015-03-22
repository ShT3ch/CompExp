using System;
using System.Collections.Generic;
using System.Linq;
using ComExp.Shapes;
using ComExp.Shapes.Functions.FuncInterfaces;

namespace ComExp.Methods
{
	public class BisecantMethod : INumericMethod<IFunction>
	{
		public string Name
		{
			get { return "BisecantMethod"; }
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

			var lastPoints = previuosPoints.GetLastN(2);

			var bisect = lastPoints.Average();

			if (Math.Abs(analyzedFunction.Compute(bisect)) < double.Epsilon)
				return Enumerable.Empty<double>();

			var firstSegment = analyzedFunction.Compute(lastPoints[0]) * analyzedFunction.Compute(bisect);
			if (firstSegment < 0)
				return new[] { lastPoints[0], bisect };

			var seconsSegment = analyzedFunction.Compute(bisect) * analyzedFunction.Compute(lastPoints[1]);
			if (seconsSegment < 0)
				return new[] { bisect, lastPoints[1] };

			throw new Exception(string.Format("Метод деления пополам сломался. Выбран отрезок с концами одного знака(f({0}) = {1},f({2}) = {3})",
				lastPoints[0],
				analyzedFunction.Compute(lastPoints[0]),
				lastPoints[1],
				analyzedFunction.Compute(lastPoints[1])));
		}

		public IEnumerable<IShape> GenerateIllustrationForCurrentStep(IEnumerable<double> actualPoints, IFunction analyzedFunction, int iterationNumber)
		{
			if (!actualPoints.IsEnoughOfParams(2))
				MethodsHelper.ThrowGreedlyException(actualPoints, 2);

			var lastPoints = actualPoints.GetLastN(2);

			for (var i = 0; i < 2; i++)
			{
				var onLinePoint = analyzedFunction.Compute(lastPoints[i]);

				yield return new VerticalSegment(onLinePoint + 1, onLinePoint - 1, lastPoints[i], String.Format("Bound({0}) N{1}", lastPoints[i], iterationNumber));
			}
		}
	}
}