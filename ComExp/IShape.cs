using ComExp.FuncInterfaces;

namespace ComExp
{
	interface IShape<out TFunc>:IShape
		where TFunc:IFunction
	{
		new TFunc Generator { get; }
	}

	interface IShape
	{
		IFunction Generator { get; }
		IDomain ParametersDomain { get; }
		string Name { get; }
	}
}
