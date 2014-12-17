using System.Collections.Generic;
using ComExp.FuncInterfaces;

namespace ComExp
{
	interface INumericMethod<in TFunc>
		where TFunc:IFunction
	{
		double ComputeNext(IEnumerable<double> previuosPoints, TFunc analyzedFunction);
		IShape GenerateIllustrationForCurrentStep(IEnumerable<double> actualPoints, TFunc analyzedFunction);
	}
}
