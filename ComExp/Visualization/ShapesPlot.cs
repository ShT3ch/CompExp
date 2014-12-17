using System.Collections.Generic;
using ComExp.Shapes;

namespace ComExp.Visualization
{
	class ShapesPlot : BasePlot, IPlot
	{
		public void DrawShape(IShape shape)
		{
			var line = new LineGroup(shape.Name);

			CleanUpOldShapeAndRegistrateNew(shape, line);

			line.CreateLine(shape.Generator.Compute, shape.ParametersDomain.GetRangeOfArguments());

			Space.Configure();
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

		readonly Dictionary<string, LineGroup> ExistanceLines = new Dictionary<string, LineGroup>();
	}
}
