using System;
using System.Collections.Generic;
using System.Linq;
using ILNumerics;
using ILNumerics.Drawing.Plotting;

namespace ComExp.Visualization
{
	public class LineGroup
	{
		public readonly ILLinePlot Graph;

		private IEnumerable<double> Range;
		private float[,] Points;

		public LineGroup(string nameOfLine = "nameless")
		{
			Graph = new ILLinePlot(ILMath.empty<float>(), nameOfLine, lineWidth: 2);
		}

		public LineGroup CreateLine(Func<double, double> func, IEnumerable<double> range)
		{
			Range = range;

			var temp = Range.Select(x => new float[3] {(float)x, (float)func(x), 0}).ToArray();
			
			Points = new float[temp.Length,3];
			for (var i = 0; i < temp.Length; i++)
			{
				Points[i, 0] = temp[i][0];
				Points[i, 1] = temp[i][1];
				Points[i, 2] = temp[i][2];
			}

			var vector = (ILArray<float>) Points;

			Graph.Line.Positions.Update(vector);
			Graph.Configure();
			return this;
		}
	}
}