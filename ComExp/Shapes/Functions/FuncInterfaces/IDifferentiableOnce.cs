namespace ComExp.Shapes.Functions.FuncInterfaces
{
	interface IDifferentiableOnce : IFunction
	{
		IFunction FirstDerivative { get; }
	}
}
