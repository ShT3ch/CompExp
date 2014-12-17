using System;
using ComExp.Shapes.Functions.FuncInterfaces;

namespace ComExp.Shapes.Functions
{
	public class Problem4 : IDifferentiableTwice
	{
		public Problem4()
		{
			FirstDerivative = FirstDerivativeFunc;
			SecondDerivative = SecondDerivativeFunc;
		}

		public double Compute(double x)
		{
			return MyFunc(x);
		}

		public IFunction FirstDerivative { get; private set; }

		public IFunction SecondDerivative { get; private set; }

		private readonly Func<double, double> MyFunc = x => 2 * Math.Cos(x) - Math.Exp(x);
		private readonly SimpleFunctionKeeper FirstDerivativeFunc = new SimpleFunctionKeeper(x => -2 * Math.Sin(x) - Math.Exp(x));
		private readonly SimpleFunctionKeeper SecondDerivativeFunc = new SimpleFunctionKeeper(x => -2 * Math.Cos(x) - Math.Exp(x));
	}
}