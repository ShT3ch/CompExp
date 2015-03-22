using ComExp.Shapes.Domains;
using ComExp.Shapes.Functions.FuncInterfaces;

namespace ComExp.Shapes
{
	public interface IShape<out TFunc>:IShape
		where TFunc:IFunction
	{
		new TFunc Generator { get; }
	}

	public interface IShape
	{
		IFunction Generator { get; }
		IDomain ParametersDomain { get; }
		string Name { get; }
	}
}
