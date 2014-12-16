namespace ComExp.FuncInterfaces
{
	interface IDifferentiableTwice : IDifferentiableOnce
	{
		IFunction SecondDerivative { get; }
	}
}
