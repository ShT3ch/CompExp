using System.Linq;
using ComExp.Shapes.Domains;
using ComExp.Shapes.Functions;
using ComExp.Shapes.Functions.FuncInterfaces;

namespace ComExp.Shapes
{
	public class VerticalSegment : IShape
	{
		public VerticalSegment(double Y1, double Y2, double X, string name)
		{
			Name = name;
			var countOfCalls = 0;

			Generator = new SimpleFunctionKeeper(d =>
			{
				if (countOfCalls == 0)
					return Y1;
				return Y2;
			});

			ParametersDomain = new SimpleDomainKeeper(Enumerable.Repeat(X, 2));
		}

		public IFunction Generator { get; private set; }
		public IDomain ParametersDomain { get; private set; }
		public string Name { get; private set; }
	}
}