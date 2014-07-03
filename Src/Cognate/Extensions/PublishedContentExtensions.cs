using System;
using Cognate.Models;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace Cognate.Extensions
{
	internal static class PublishedContentExtensions
	{
		public static Test AsTest(this IPublishedContent content)
		{
			return new Test
			{
				Id = content.Id,
				Name = content.Name,
				SourcePageId = content.Get<int>("sourcePage"),
				TargetPageId = content.Get<int>("targetPage"),
				TargetQueryString = content.Get<string>("targetQueryString", defaultValue:""),
				MaxLead = content.Get<int>("maxLead", defaultValue:100),
				StartDate = content.Get<DateTime>("startDate", defaultValue:DateTime.MinValue),
				EndDate = content.Get<DateTime>("endDate", defaultValue:DateTime.MaxValue),
				Active = content.Get<bool>("active"),
				Content = content
			};
		}

		public static T Get<T>(this IPublishedContent content, 
			string key, bool recursive = false, T defaultValue = default(T))
		{
			return content.GetPropertyValue<T>(key, recursive, defaultValue);
		}
	}
}
