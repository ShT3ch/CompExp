using System;
using System.Collections.Generic;
using System.Linq;

namespace ComExp.Methods
{
	public class NumeredValue
	{
		public double Value;
		public int Number;
	}

	public class Counter
	{
		public int GetNumber {
			get { return counter++; }
		}
		private int counter;
	}

	public static class MethodsHelper
	{
		public static bool IsEnoughOfParams(this IEnumerable<double> parameters, int nessesary)
		{
			return parameters.Count() >= nessesary;
		}

		public static double[] GetLastN(this IEnumerable<double> parameters, int N)
		{
			CheckParametersCount(parameters, N);

			return parameters.Skip(Math.Max(0, parameters.Count() - N)).Take(N).ToArray();
		}

		public static NumeredValue[] GetLastN(this IEnumerable<NumeredValue> parameters, int N)
		{
			CheckParametersCount(parameters.Select(elem => elem.Value), N);

			return parameters.Skip(Math.Max(0, parameters.Count() - N)).Take(N).ToArray();
		}

		public static IEnumerable<NumeredValue> Numerate(this IEnumerable<double> parameters)
		{
			var counter = new Counter();
			return parameters.Select(elem => new NumeredValue { Value = elem, Number = counter.GetNumber });
		}

		public static void ThrowGreedlyException(IEnumerable<double> parameters, int N)
		{
			throw new ArgumentException(string.Format("Недостаточно элементов(Нужно {0}) в коллекции {1}", N, String.Join(", ", parameters)));
		}

		private static void CheckParametersCount(IEnumerable<double> parameters, int N)
		{
			if (!IsEnoughOfParams(parameters, N))
				ThrowGreedlyException(parameters, N);
		}
	}
}