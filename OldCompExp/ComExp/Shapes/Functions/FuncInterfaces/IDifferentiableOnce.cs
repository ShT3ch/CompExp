namespace ComExp.Shapes.Functions.FuncInterfaces
{
	public interface IDifferentiableOnce : IFunction
	{
		IFunction FirstDerivative { get; }
	}
}
