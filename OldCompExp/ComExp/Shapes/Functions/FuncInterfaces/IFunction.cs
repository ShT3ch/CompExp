namespace ComExp.Shapes.Functions.FuncInterfaces
{
	public interface IFunction
	{
		double Compute(double x);

		string Name { get; }
	}
}
