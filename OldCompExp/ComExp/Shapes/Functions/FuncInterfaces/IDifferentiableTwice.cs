namespace ComExp.Shapes.Functions.FuncInterfaces
{
	public interface IDifferentiableTwice : IDifferentiableOnce
	{
		IFunction SecondDerivative { get; }
	}
}
