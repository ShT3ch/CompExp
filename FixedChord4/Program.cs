using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ComExp;
using ComExp.Methods;
using ComExp.Reporters;
using ComExp.Shapes;
using ComExp.Shapes.Domains;
using ComExp.Shapes.Functions;
using ComExp.Shapes.Functions.FuncInterfaces;
using ComExp.Visualization;

namespace FixedChord4
{
	class Program
	{
		[STAThread]
		static void Main(string[] args)
		{
			var host = new PlotHost();
			host.Initialize();

			var func = new Problem4();
			var shape = new ShapeKeeper<Problem4>(func, new UsualExpandingDomain(), func.Name);
			var conditions = new Conditions4();
			var cycle = new Iterator<Problem4>(shape, conditions);


			var methods = new List<INumericMethod<IDifferentiableTwice>>
			{
				new BisecantMethod(){StepSize = 2},
				new MethodOfFixedChords(){StepSize = 1},
				new SecantMethod(){StepSize = 1},
				new NewtonMethod(){StepSize = 1},
				new ParabolaMethod(-1){StepSize = 3},
				//new MullersMethod(conditions.InitialPoints.Min(), conditions.InitialPoints.Max(), func, -1)
			};


			Task.Run(() =>
			{
				var reports = new List<string>();

				Console.ReadLine();
				foreach (var numericMethod in methods)
				{
					var reporter = new HtmlColumnReporter(numericMethod.Name, func.Name + numericMethod.SrcImg, numericMethod.StepSize, conditions.InitialPoints, conditions, shape);
					var plot = new ShapesPlot();
					host.AccomodateControl(plot, numericMethod.Name);

					host.InitializedEvent.WaitOne();
					var reportText = cycle.DoItRight(numericMethod, shape, plot, conditions, reporter);
					Console.WriteLine(reportText);

					plot.ExportImage(func.Name+numericMethod.SrcImg);

					reports.Add(reportText);
				}

				var resultReport = HtmlColumnReporter.AroundHtml(
					HtmlColumnReporter.AroundTable(
					(new Row()
					{
						Elems = new[]
						{
							(new Column()
							{
								Elems = reports.Select(HtmlColumnReporter.AroundTable).ToArray()
							}).GetHtml
						}
					}).GetHtml));
				using (var file = new StreamWriter(File.Open("Report_" + func.Name + ".html", FileMode.Create)))
					file.WriteLine(resultReport);

			}
				);
			host.InitializedEvent.Set();
			Application.Run(PlotHost.Main);
		}
	}
}
