using System.Collections.Generic;
using ComExp.Shapes;
using ComExp.Shapes.Functions.FuncInterfaces;

namespace ComExp.Methods
{
	public interface INumericMethod<in TFunc>
		where TFunc:IFunction
	{
		double ComputeNext(IEnumerable<double> previuosPoints, TFunc analyzedFunction);
		IEnumerable<IShape> GenerateIllustrationForCurrentStep(IEnumerable<double> actualPoints, TFunc analyzedFunction, int iterationNumber);
	}
}
