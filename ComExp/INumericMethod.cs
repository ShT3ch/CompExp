using System.Collections.Generic;
using ComExp.Shapes;
using ComExp.Shapes.Functions.FuncInterfaces;

namespace ComExp
{
	interface INumericMethod<in TFunc>
		where TFunc:IFunction
	{
		double ComputeNext(IEnumerable<double> previuosPoints, TFunc analyzedFunction);
		IShape GenerateIllustrationForCurrentStep(IEnumerable<double> actualPoints, TFunc analyzedFunction);
	}
}
