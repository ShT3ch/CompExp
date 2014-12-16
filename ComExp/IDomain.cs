using System.Collections.Generic;

namespace ComExp
{
	interface IDomain
	{
		IEnumerable<double> GetRangeOfArguments();
		IDomain Update(double newPoint);
	}
}
