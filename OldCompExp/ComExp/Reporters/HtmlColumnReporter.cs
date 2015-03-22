using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComExp.Methods;
using ComExp.Shapes;

namespace ComExp.Reporters
{
	public class Column
	{
		public string[] Elems;

		public String GetHtml
		{
			get { return string.Join("\r\n", Elems.Select(elem => string.Format("<td>{0}</td>", elem))); }
		}
	}
	public class Row
	{
		public string[] Elems;

		public String GetHtml
		{
			get { return string.Join("\r\n", Elems.Select(elem => string.Format("<tr>{0}</tr>", elem))); }
		}
	}

	public class Step
	{
		public IEnumerable<double> xi;

		public String XiS
		{
			get { return string.Join(", ", xi); }
		}

		public int n;
		public double distToRoot;

		public Column GetHtml
		{
			get { return new Column() { Elems = new[] { n.ToString(), string.Join(", ", xi), distToRoot.ToString() } }; }
		}
	}

	public class HtmlColumnReporter : IReportGenerator
	{
		public string MethodName { get; set; }
		public string PictureSrc { get; set; }
		public int StepSize { get; set; }

		public int N { get; set; }
		public double Root { get; set; }
		public double Error { get; set; }

		public string StartPoints { get; set; }
		public Conditions Conditions { get; set; }
		public IShape Function { get; set; }

		public List<Step> Steps { get; set; }

		public static string AroundHtml(string body)
		{
			return "<html><head><style>" +
   @"table {" +
	@"width: 100%; /* Ширина таблицы */" +
   @"}" +
   @"div {vertical-align: top;}" +
   @"td {" +
	@"padding: 5px; /* Поля в ячейках */" +
	@"vertical-align: top; /* Выравнивание по верхнему краю ячеек */" +
	@"text-align: center; /* Выравнивание по верхнему краю ячеек */" +
   @"}" +
  @"</style></head>	<body>" + string.Format("{0} </body> </html>", body);
		}

		public static string AroundTable(string body)
		{
			return string.Format("<div height = \"100%\"><table border=\"1\">{0}</table></div>", body);
		}

		public HtmlColumnReporter(string methodName, string pictureSrc, int stepSize, IEnumerable<double> startPoints, Conditions conditions, IShape function)
		{
			MethodName = methodName;
			PictureSrc = pictureSrc;
			StepSize = stepSize;
			StartPoints = "start points: "+string.Join(", ", startPoints);
			Conditions = conditions;
			Function = function;

			Steps = new List<Step>();
		}

		public string GenerateReport(double root, int n, double error)
		{
			return string.Format("								" +
"	    <tr>													" +
"            <td colspan=\"3\">									" +
"                <h4 align=\"center\">{0}</h4>					" +
"            </td>												" +
"	    </tr>													" +
"	    <tr>													" +
"	        <td colspan=\"3\">									" +
"                <a href=\"{1}\">							" +
"                    <img width=\"100%\" src=\"{1}\"></img>	" +
"                </a>											" +
"	        </td>												" +
"	    </tr>													" +
"        <tr>													" +
"			<td>												" +
"			N = {2}													" +
"			</td>												" +
"			<td>												" +
"			Root = {3}													" +
"			</td>												" +
"			<td>												" +
"			Error = {4}												" +
"			</td>												" +
"		</tr>													" +
"	    <tr>													" +
"	        <td colspan=\"3\"" +
			"> {5}</td>						" +
			"	    </tr>													" +
			"{6}", MethodName, PictureSrc, n, root, error, StartPoints, new Row { Elems = Steps.Select(step => step.GetHtml).Select(column => column.GetHtml).ToArray() }.GetHtml);
		}

		public void AddIntermidiateStep(IEnumerable<double> actualPoints, IEnumerable<IShape> pictureOfShape, int iterationNumber)
		{
			var lastPoints = actualPoints.GetLastN(StepSize);

			Steps.Add(new Step() { n = iterationNumber, distToRoot = lastPoints.Last() - Conditions.RootPoint, xi = lastPoints });
		}

		public void Init(IEnumerable<double> startPoints, IShape function, Conditions conditions)
		{

		}
	}
}
