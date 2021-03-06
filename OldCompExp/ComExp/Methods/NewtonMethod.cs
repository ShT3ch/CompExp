﻿using System;
using System.Collections.Generic;
using System.Linq;
using ComExp.Shapes;
using ComExp.Shapes.Domains;
using ComExp.Shapes.Functions.FuncInterfaces;

namespace ComExp.Methods
{
	public class NewtonMethod : INumericMethod<IDifferentiableOnce>
	{
		public string Name
		{
			get { return "NewtonMethod"; }
		}

		public string SrcImg {
			get { return Name + ".SVG"; }
		}

		public int StepSize { get; set; }

		public IEnumerable<double> ComputeNext(IEnumerable<double> previuosPoints, IDifferentiableOnce analyzedFunction)
		{
			if (!previuosPoints.IsEnoughOfParams(1))
				MethodsHelper.ThrowGreedlyException(previuosPoints, 1);

			var prevPoint = previuosPoints.Last();

			if (prevPoint < 0)
				throw new Exception("Метод Ньютона - тыква. Касательная ушла не туда. Надо попробовать начать метод с другой стороны от корня.");

			var inPoint = analyzedFunction.Compute(prevPoint);
			var inPointDerivative = analyzedFunction.FirstDerivative.Compute(prevPoint);



			yield return prevPoint - inPoint / inPointDerivative;
		}

		public IEnumerable<IShape> GenerateIllustrationForCurrentStep(IEnumerable<double> actualPoints, IDifferentiableOnce analyzedFunction, int iterationNumber)
		{
			if (!actualPoints.IsEnoughOfParams(2))
				MethodsHelper.ThrowGreedlyException(actualPoints, 2);

			var lastPoints = actualPoints.GetLastN(2);

			yield return new LineShape(new UsualExpandingDomain().Update(lastPoints[0]).Update(lastPoints[1]),
									   new Point2D { X = lastPoints[0], Y = analyzedFunction.Compute(lastPoints[0]) },
									   new Point2D { X = lastPoints[1], Y = 0 },
									   string.Format("Secant N{0}", iterationNumber));

			yield return new VerticalSegment(0,
											 analyzedFunction.Compute(lastPoints[1]),
											 lastPoints[1],
											 string.Format("Newton step N{0}", iterationNumber));
		}
	}
}
