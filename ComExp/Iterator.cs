using System;
using System.Collections.Generic;
using System.Linq;
using ComExp.Methods;
using ComExp.Reporters;
using ComExp.Shapes;
using ComExp.Shapes.Functions.FuncInterfaces;
using ComExp.Visualization;

namespace ComExp
{
	public class Iterator<TMethod, TFunc>
		where TMethod : INumericMethod<TFunc>
		where TFunc : IFunction
	{
		public String DoItRight(TMethod method, IShape<TFunc> func, IPlot plotSpace, Conditions conditions, IReportGenerator reporter)
		{
			var actualPoints = conditions.InitialPoints.ToArray();

			UpdateDomain(func, actualPoints);
			reporter.Init(actualPoints, func, conditions);

			var iterationsCounter = 0;

			while (!IsAnyPointPrettyCloseToRoot(func.Generator, conditions, actualPoints) && iterationsCounter++ < conditions.MaxNumberOfIteration)
			{
				var newPoint = method.ComputeNext(actualPoints, func.Generator);

				actualPoints = actualPoints.Concat(Enumerable.Repeat(newPoint, 1)).ToArray();
				UpdateDomain(func, actualPoints);

				var pictureOfStep = method.GenerateIllustrationForCurrentStep(actualPoints, func.Generator);

				reporter.AddIntermidiateStep(actualPoints, pictureOfStep);

				plotSpace.DrawShape(func);
				foreach (var shape in pictureOfStep)
				{
					plotSpace.DrawShape(shape);
				}


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
