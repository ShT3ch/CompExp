using System;
using System.Collections.Generic;
using ComExp.Shapes;
using ComExp.Shapes.Domains;
using ComExp.Shapes.Functions;
using ComExp.Shapes.Functions.FuncInterfaces;

namespace ComExp.Methods
{
	public class ParabolaMethod : INumericMethod<IDifferentiableTwice>
	{
		private int Sign { get; set; }

		public ParabolaMethod(int sign = 1)
		{
			Sign = sign;
		}

		public IEnumerable<double> ComputeNext(IEnumerable<double> previuosPoints, IDifferentiableTwice analyzedFunction)
		{
			if (!previuosPoints.IsEnoughOfParams(3))
				MethodsHelper.ThrowGreedlyException(previuosPoints, 3);

			var lastPoints = previuosPoints.GetLastN(3);

			var x1 = lastPoints[0];
			var x2 = lastPoints[1];
			var x3 = lastPoints[2];

			var y1 = analyzedFunction.Compute(lastPoints[0]);
			var y2 = analyzedFunction.Compute(lastPoints[1]);
			var y3 = analyzedFunction.Compute(lastPoints[2]);

			var denom = (x1 - x2) * (x1 - x3) * (x2 - x3);
			var A1 = (x3 * (y2 - y1) + x2 * (y1 - y3) + x1 * (y3 - y2)) / denom;
			var B1 = (Math.Pow(x3, 2) * (y1 - y2) + Math.Pow(x2, 2) * (y3 - y1) + Math.Pow(x1, 2) * (y2 - y3)) / denom;
			var C1 = (x2 * x3 * (x2 - x3) * y1 + x3 * x1 * (x3 - x1) * y2 + x1 * x2 * (x1 - x2) * y3) / denom;

			var root = (-B1 + Sign * Math.Sqrt(B1 * B1 - 4 * A1 * C1)) / (2 * A1);

			if (x1 < root && root < x2)
				return new List<double> { x1, root, x2 };
			if (x2 < root && root < x3)
				return new List<double> { x2, root, x3 };

			throw new Exception("Метод параболы сломался. Надо попробовать другой знак дискриминанта.");

			//		throw new Exception(string.Format("Метод параболы сломался. Выбран отрезок с концами одного знака(f({0}) = {1},f({2}) = {3})",
			//lastPoints[0],
			//analyzedFunction.Compute(lastPoints[0]),
			//lastPoints[1],
			//analyzedFunction.Compute(lastPoints[1])));
		}

		public IEnumerable<IShape> GenerateIllustrationForCurrentStep(IEnumerable<double> actualPoints, IDifferentiableTwice analyzedFunction, int iterationNumber)
		{
			if (!actualPoints.IsEnoughOfParams(4))
				MethodsHelper.ThrowGreedlyException(actualPoints, 4);

			var lastPoints = actualPoints.GetLastN(4);

			var x1 = lastPoints[0];
			var x2 = lastPoints[1];
			var x3 = lastPoints[2];

			var y1 = analyzedFunction.Compute(lastPoints[0]);
			var y2 = analyzedFunction.Compute(lastPoints[1]);
			var y3 = analyzedFunction.Compute(lastPoints[2]);

			var denom = (x1 - x2) * (x1 - x3) * (x2 - x3);
			var A1 = (x3 * (y2 - y1) + x2 * (y1 - y3) + x1 * (y3 - y2)) / denom;
			var B1 = (Math.Pow(x3, 2) * (y1 - y2) + Math.Pow(x2, 2) * (y3 - y1) + Math.Pow(x1, 2) * (y2 - y3)) / denom;
			var C1 = (x2 * x3 * (x2 - x3) * y1 + x3 * x1 * (x3 - x1) * y2 + x1 * x2 * (x1 - x2) * y3) / denom;

			yield return new ShapeKeeper<IFunction>(new SimpleFunctionKeeper(x => x * x * A1 + x * B1 + C1),
				new UsualExpandingDomain().Update(lastPoints[0]).Update(lastPoints[1]).Update(lastPoints[2]).Update(lastPoints[3]),
				string.Format("Parabola on {0}x*x+{1}*x+{2} N{3}", A1, B1, C1, iterationNumber)
				);

			var rightRoot = (-B1 + Sign*Math.Sqrt(B1 * B1 - 4 * A1 * C1)) / (2 * A1);

			yield return new VerticalSegment(0, analyzedFunction.Compute(rightRoot), rightRoot, string.Format("Step N{0}", iterationNumber));
		}
	}
}
