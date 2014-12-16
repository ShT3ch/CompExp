using System;
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
		String СделайЗаебись(TMethod method, TFunc func, IPlot plotSpace, Conditions conditions)
		{
			var actualPoints = conditions.InitialPoints.ToArray();

			var actualDomenOfFunction = new 

			if (actualPoints.Any(point => IsPrettyClose(DistanceToRoot(func, point, conditions), conditions)))
				return string.Format(Cool, actualPoints.First(point => IsPrettyClose(DistanceToRoot(func, point, conditions), conditions)));

			while (!actualPoints.Any(point => IsPrettyClose(DistanceToRoot(func, point, conditions), conditions)))
			{

			}
			return string.Format(Cool, actualPoints.First(point => IsPrettyClose(DistanceToRoot(func, point, conditions), conditions)));
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
