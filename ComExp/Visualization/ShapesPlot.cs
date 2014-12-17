using System.Collections.Generic;
using ComExp.Shapes;
using ILNumerics.Drawing.Plotting;

namespace ComExp.Visualization
{
	public class ShapesPlot : BasePlot, IPlot
	{
		public ShapesPlot()
		{
			Space.Children.Add(Legend = new ILLegend());
		}

		public void DrawShape(IShape shape)
		{
			var line = new LineGroup(shape.Name);

			CleanUpOldShapeAndRegistrateNew(shape, line);

			line.CreateLine(shape.Generator.Compute, shape.ParametersDomain.GetRangeOfArguments());

			ReLegend();

			this.Configure();
			IUpdated();
		}

		private void CleanUpOldShapeAndRegistrateNew(IShape newShape, LineGroup newLine)
		{
			if (ExistanceLines.ContainsKey(newShape.Name))
			{
				Space.Children.Remove(ExistanceLines[newShape.Name].Graph);
				ExistanceLines.Remove(newShape.Name);
			}

			ExistanceLines.Add(newShape.Name, newLine);
			Space.Children.Add(newLine.Graph);
		}

		private void ReLegend()
		{
			Space.Children.Remove(Legend);
			Space.Children.Add(Legend = new ILLegend());
		}

		private ILLegend Legend = new ILLegend();
		readonly Dictionary<string, LineGroup> ExistanceLines = new Dictionary<string, LineGroup>();
	}
}
