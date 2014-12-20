using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComExp.Shapes.Functions.FuncInterfaces;

namespace ComExp.Shapes.Functions
{
	public class Problem1:IDifferentiableTwice
	{

		public string Name
		{
			get { return "Problem 3 1sin(x)-1.2e^(-x)"; }
		}

		public Problem1()
		{
			FirstDerivative = new SimpleFunctionKeeper(FirstDerivativeFunc);
			SecondDerivative = new SimpleFunctionKeeper(SecondDerivativeFunc);
		}

		public double Compute(double x)
		{
			return Func(x);
		}

		private Func<double, double> Func = x => 1 + Math.Sin(x) - 1.2 * Math.Exp(-x);
		private Func<double, double> FirstDerivativeFunc = x => Math.Cos(x) + 1.2*Math.Exp(-x);
		private Func<double, double> SecondDerivativeFunc = x => -Math.Sin(x) - 1.2*Math.Exp(-x);

		
		public IFunction FirstDerivative { get; private set; }
		public IFunction SecondDerivative { get; private set; }
	}
}
