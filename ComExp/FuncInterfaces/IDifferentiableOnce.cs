namespace ComExp.FuncInterfaces
{
	interface IDifferentiableOnce : IFunction
	{
		IFunction FirstDerivative { get; }
	}
}
