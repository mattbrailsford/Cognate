using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cognate.Extensions
{
	internal static class ObjectExtensions
	{
		public static T SingleRandom<T>(this IEnumerable<T> collection)
		{
			return collection.OrderBy(x => Guid.NewGuid()).First();
		}

		public static T SingleRandomOrDefault<T>(this IEnumerable<T> collection)
		{
			return collection.OrderBy(x => Guid.NewGuid()).FirstOrDefault();
		}
	}
}
