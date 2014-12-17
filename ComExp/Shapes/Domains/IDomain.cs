using System.Collections.Generic;

namespace ComExp.Shapes.Domains
{
	interface IDomain
	{
		IEnumerable<double> GetRangeOfArguments();
		IDomain Update(double newPoint);
	}
}
