using ComExp.FuncInterfaces;

namespace ComExp
{
	interface IShape
	{
		IFunction Generator { get; }
		IDomain ParametersDomain { get; }
	}
}
