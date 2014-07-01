using System;
using System.Reflection;
using Umbraco.Core.Persistence;

namespace Cognate.Extensions
{
	internal static class TypeExtensions
	{
		public static string GetTableName(this Type type)
		{
			var attr = type.GetCustomAttribute<TableNameAttribute>(false);
			return attr != null ? attr.Value : type.Name;
		}

		public static string GetPrimaryKeyName(this Type type)
		{
			var attr = type.GetCustomAttribute<PrimaryKeyAttribute>(true);
			return attr != null ? attr.Value : "Id";
		}
	}
}
