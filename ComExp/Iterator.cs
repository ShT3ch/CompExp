using System;
using System.Collections.Generic;
using System.Linq;
using ComExp.Shapes;
using ComExp.Shapes.Functions.FuncInterfaces;
using ComExp.Visualization;

namespace ComExp
{
	class Iterator<TMethod, TFunc>
		where TMethod : INumericMethod<TFunc>
		where TFunc : IFunction
	{
		public String DoItRight(TMethod method, IShape<TFunc> func, IPlot plotSpace, Conditions conditions, IReportGenerator reporter)
		{
			var actualPoints = conditions.InitialPoints.ToArray();

			UpdateDomain(func, actualPoints);
			reporter.Init(actualPoints, func, conditions);

			while (!IsAnyPointPrettyCloseToRoot(func.Generator, conditions, actualPoints))
			{
				var newPoint = method.ComputeNext(actualPoints, func.Generator);
				
				actualPoints.Concat(Enumerable.Repeat(newPoint, 1));
				UpdateDomain(func, actualPoints);
				
				var pictureOfStep = method.GenerateIllustrationForCurrentStep(actualPoints, func.Generator);

				reporter.AddIntermidiateStep(actualPoints, pictureOfStep);

				plotSpace.DrawShape(func);
				plotSpace.DrawShape(pictureOfStep);
			}

			return reporter.GenerateReport();
		}

		private static void UpdateDomain(IShape<TFunc> func, IEnumerable<double> actualPoints)
		{
			foreach (var actualPoint in actualPoints)
			{
				func.ParametersDomain.Update(actualPoint);
			}
		}

		private bool IsAnyPointPrettyCloseToRoot(TFunc func, Conditions conditions, IEnumerable<double> actualPoints)
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
