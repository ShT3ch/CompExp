﻿using System;
using ComExp.Shapes.Functions.FuncInterfaces;

namespace ComExp.Shapes.Functions
{
	class SimpleFunctionKeeper : IFunction
	{
		public SimpleFunctionKeeper(Func<double, double> storedFunk)
		{
			StoredFunk = storedFunk;
		}

		public double Compute(double x)
		{
			return StoredFunk(x);
		}

		public string Name
		{
			get { return "SimpleFunctionKeeper" + StoredFunk.GetHashCode(); }
		}

		public Func<double, double> StoredFunk { get; private set; }
	}
}
