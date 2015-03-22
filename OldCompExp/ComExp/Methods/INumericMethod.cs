using System.Collections.Generic;
using ComExp.Shapes;
using ComExp.Shapes.Functions.FuncInterfaces;

namespace ComExp.Methods
{
	public interface INumericMethod<in TFunc>
		where TFunc:IFunction
	{
		string Name { get; }
		string SrcImg { get; }

		int StepSize { get; }
		IEnumerable<double> ComputeNext(IEnumerable<double> previuosPoints, TFunc analyzedFunction);
		IEnumerable<IShape> GenerateIllustrationForCurrentStep(IEnumerable<double> actualPoints, TFunc analyzedFunction, int iterationNumber);
	}
}
