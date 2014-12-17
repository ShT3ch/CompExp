namespace ComExp.Shapes.Functions.FuncInterfaces
{
	interface IDifferentiableTwice : IDifferentiableOnce
	{
		IFunction SecondDerivative { get; }
	}
}
