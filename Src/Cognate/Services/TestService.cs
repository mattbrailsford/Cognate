using System;
using System.Collections.Generic;
using System.Linq;
using Cognate.Data.Repositories;
using Cognate.Extensions;
using Cognate.Models;
using Examine;
using umbraco;
using Umbraco.Core;
using Umbraco.Web;

namespace Cognate.Services
{
	internal class TestService
	{
		public IEnumerable<Test> GetTests()
		{
			return (IEnumerable<Test>)ApplicationContext.Current.ApplicationCache.RuntimeCache
				.GetCacheItem(Constants.AllTestsCacheKey,
				() =>
				{
					var contentQuery = new PublishedContentQuery(UmbracoContext.Current.ContentCache, 
						UmbracoContext.Current.MediaCache);

					var searcher = ExamineManager.Instance.DefaultSearchProvider;
					var criteria = searcher.CreateSearchCriteria();
					var query = criteria.Field("__NodeTypeAlias", "CognateTest").Compile();

					var nodes = contentQuery.TypedSearch(query, searcher);

					return nodes.Select(x => x.AsTest());
				});
		}

		public IEnumerable<Test> GetActiveTests()
		{
			return GetTests().Where(x => x.Active);
		}

		public IEnumerable<Test> GetInactiveTests()
		{
			return GetTests().Where(x => !x.Active);
		}

		public void ActivateInactiveTests()
		{
			var testsToActivate = GetInactiveTests().Where(x => 
				(x.StartDate > DateTime.MinValue && x.StartDate < DateTime.Now)
				&& x.EndDate > DateTime.Now);


		}

		public void DeactivateActiveTests()
		{
			
		}

		public int IncrementVariantScore(long testId, int variantId)
		{
			return CognateContext.Instance.Repositories.TestVariantScoreRepository.IncrementScore(testId, variantId);
		}

		public void ClearTestsCache()
		{
			ApplicationContext.Current.ApplicationCache.RuntimeCache.ClearCacheItem(Constants.AllTestsCacheKey);
		}
	}
}
