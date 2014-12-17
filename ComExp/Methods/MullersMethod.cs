using System;
using System.Collections.Generic;
using System.Linq;
using ComExp.Shapes;
using ComExp.Shapes.Domains;
using ComExp.Shapes.Functions;
using ComExp.Shapes.Functions.FuncInterfaces;

namespace ComExp.Methods
{
	public class MullersMethod : INumericMethod<IDifferentiableTwice>
	{
		public MullersMethod(double a, double b, IDifferentiableTwice analyzedFunction, int sign = 1)
		{
			Sign = sign;
			var domain = new UsualExpandingDomain().Update(a).Update(b);

			MaximumOfDoubleDiff = domain.GetRangeOfArguments().Select(x => analyzedFunction.SecondDerivative.Compute(x)).Max();
		}

		public IEnumerable<double> ComputeNext(IEnumerable<double> previuosPoints, IDifferentiableTwice analyzedFunction)
		{
			if (!previuosPoints.IsEnoughOfParams(3))
				MethodsHelper.ThrowGreedlyException(previuosPoints, 3);

			var lastPoints = previuosPoints.GetLastN(3);

			var differed = analyzedFunction.FirstDerivative.Compute(lastPoints[2]);

			var deltaXi = (-differed + Sign * Math.Sqrt(differed * differed + 2 * MaximumOfDoubleDiff * (analyzedFunction.Compute(lastPoints[2])))) / (-MaximumOfDoubleDiff);

			yield return deltaXi + lastPoints[2];
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

			yield return new ShapeKeeper<IFunction>(new SimpleFunctionKeeper(x => analyzedFunction.Compute(x3)
				+ analyzedFunction.FirstDerivative.Compute(x3) * (x - x3)
				- MaximumOfDoubleDiff * Math.Pow(x - x3, 2) / 2),
				new UsualExpandingDomain().Update(lastPoints[0]).Update(lastPoints[1]).Update(lastPoints[2]).Update(lastPoints[3]),
				string.Format("Porabola on {0}x*x+{1}*x+{2} N{3}", A1, B1, C1, iterationNumber)
				);

			yield return new VerticalSegment(0, analyzedFunction.Compute(lastPoints[3]), lastPoints[4], string.Format("Step N{0}", iterationNumber));
		}

		protected Func<double, double, double, Func<double, double>, double> MethodStep
		{
			get
			{
				return (xi_2, xi_1, xi, f) =>
				{
					var b = B(xi_2, xi_1, xi, f);
					var discriminant = Discriminant(xi_2, xi_1, xi, f);
					var c = C(xi_2, xi_1, xi, f);

					return xi - (xi - xi_1) * ((2 * c) / (Math.Max(b + discriminant, b - discriminant)));
				};
			}
		}

		protected Func<double, double, double, Func<double, double>, double> Discriminant
		{
			get
			{
				return (xi_2, xi_1, xi, f) =>
				{
					var b = B(xi_2, xi_1, xi, f);
					var a = A(xi_2, xi_1, xi, f);
					var c = C(xi_2, xi_1, xi, f);

					return Math.Sqrt(Math.Pow(b, 2) - 4 * a * c);
				};
			}
		}

		protected Func<double, double, double, double> Q
		{
			get { return (xi_2, xi_1, xi) => (xi - xi_1) / (xi_1 - xi_2); }
		}

		protected Func<double, double, double, Func<double, double>, double> A
		{
			get
			{
				return (xi_2, xi_1, xi, f) => Q(xi_2, xi_1, xi) * f(xi) - Q(xi_2, xi_1, xi) * f(xi_1) + Math.Pow(Q(xi_2, xi_1, xi), 2) * f(xi_2);
			}
		}

		protected Func<double, double, double, Func<double, double>, double> B
		{
			get
			{
				return (xi_2, xi_1, xi, f) => (2 * Q(xi_2, xi_1, xi) + 1) * f(xi) - Math.Pow(1 + Q(xi_2, xi_1, xi), 2) * f(xi_1) + Math.Pow(Q(xi_2, xi_1, xi), 2) * f(xi_2);
			}
		}

		protected Func<double, double, double, Func<double, double>, double> C
		{
			get
			{
				return (xi_2, xi_1, xi, f) => (1 + Q(xi_2, xi_1, xi)) * f(xi);
			}
		}

		private double MaximumOfDoubleDiff { get; set; }
		private int Sign { get; set; }
	}
}