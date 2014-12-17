using ComExp.Shapes.Domains;
using ComExp.Shapes.Functions.FuncInterfaces;

namespace ComExp.Shapes
{
	public class ShapeKeeper<TFunc> : IShape<TFunc>
		where TFunc : IFunction
	{
		public ShapeKeeper(TFunc generator, IDomain domain, string name)
		{
			Generator = generator;
			ParametersDomain = domain;
			Name = name;
		}

		public TFunc Generator { get; private set; }
		IFunction IShape.Generator
		{
			get { return Generator; }
		}
		public IDomain ParametersDomain { get; private set; }
		public string Name { get; private set; }
	}
}
