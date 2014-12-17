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

				var pictureOfStep = method.GenerateIllustrationForCurrentStep(actualPoints, func.Generator, iterationsCounter + conditions.InitialPoints.Count());

				reporter.AddIntermidiateStep(actualPoints, pictureOfStep);

				plotSpace.DrawShape(func);
				foreach (var shape in pictureOfStep)
				{
					plotSpace.DrawShape(shape);
					Console.WriteLine("Press Enter to make step");
					Console.ReadLine();
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
			var lastPoints = actualPoints.GetLastN(2);

			return IsPrettyClose(DistanceBetweenPoints(func, lastPoints[0], lastPoints[1]), conditions);
		}

		double DistanceBetweenPoints(TFunc func, double point1, double point2)
		{
			return Math.Abs(func.Compute(point1) - func.Compute(point2));
		}

		double DistanceToRoot(TFunc func, double point, Conditions conditions)
		{
			return DistanceBetweenPoints(func, point, conditions.RootPoint);
		}

		bool IsPrettyClose(double distance, Conditions conditions)
		{
			return distance < conditions.Epsilon;
		}

	}
}
