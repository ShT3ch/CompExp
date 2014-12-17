using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComExp.FuncInterfaces;

namespace ComExp
{
	class Iterator<TMethod, TFunc>
		where TMethod : INumericMethod<TFunc>
		where TFunc : IFunction
	{
		private const string Cool = "";
		public String DoItRight(TMethod method, IShape<TFunc> func, IPlot plotSpace, Conditions conditions)
		{
			var actualPoints = conditions.InitialPoints.ToArray();

			UpdateDomain(func, actualPoints);

			while (!IsAnyPointPrettyCloseToRoot(func.Generator, conditions, actualPoints))
			{
				var newPoint = method.ComputeNext(actualPoints, func.Generator);
				actualPoints.Concat(Enumerable.Repeat(newPoint, 1));
				UpdateDomain(func, actualPoints);
				var pictureOfStep = method.GenerateIllustrationForCurrentStep(actualPoints, func.Generator);

				plotSpace.DrawShape(func);
				plotSpace.DrawShape(pictureOfStep);
			}
			return GenerateReport(func.Generator, conditions, actualPoints);
		}

		private static void UpdateDomain(IShape<TFunc> func, IEnumerable<double> actualPoints)
		{
			foreach (var actualPoint in actualPoints)
			{
				func.ParametersDomain.Update(actualPoint);
			}
		}

		private string GenerateReport(TFunc func, Conditions conditions, double[] actualPoints)
		{
			return string.Format(Cool, actualPoints.First(point => IsPrettyClose(DistanceToRoot(func, point, conditions), conditions)));
		}

		private bool IsAnyPointPrettyCloseToRoot(TFunc func, Conditions conditions, double[] actualPoints)
		{
			return actualPoints.Any(point => IsPrettyClose(DistanceToRoot(func, point, conditions), conditions));
		}

		double DistanceToRoot(TFunc func, double point, Conditions conditions)
		{
			return Math.Abs(func.Compute(point) - func.Compute(conditions.RootPoint));
		}

		bool IsPrettyClose(double distance, Conditions conditions)
		{
			return distance < conditions.Epsilon;
		}

	}
}
