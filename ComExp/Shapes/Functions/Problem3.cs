using System;
using ComExp.Shapes.Functions.FuncInterfaces;

namespace ComExp.Shapes.Functions
{
	public class Problem3 : IDifferentiableTwice
	{
		public Problem3()
		{
			FirstDerivative = new SimpleFunctionKeeper(MyFunc1);
			SecondDerivative = new SimpleFunctionKeeper(MyFunc2);
		}
		public double Compute(double x)
		{
			return MyFunc(x);
		}

		private Func<double, double> MyFunc = (x) => Math.Log10(x) - 0.19 / x;
		private Func<double, double> MyFunc1 = x => (x + 0.437491) / (x * x * Math.Log(10));
		private Func<double, double> MyFunc2 = x => (-0.434294 * x - 0.38) / (x * x * x);


		public IFunction FirstDerivative { get; private set; }
		public IFunction SecondDerivative { get; private set; }
	}
}