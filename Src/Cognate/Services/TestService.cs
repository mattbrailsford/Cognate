using System;
using System.Collections.Generic;
using System.Linq;
using Cognate.Models;
using Umbraco.Core;

namespace Cognate.Services
{
	internal class TestService
	{
		public IEnumerable<Test> GetTests(bool includeInactive = false)
		{
			return (IEnumerable<Test>)ApplicationContext.Current.ApplicationCache.RuntimeCache
				.GetCacheItem(Constants.AllTestsCacheKey,
				() => {
					
					//TODO: Lookup all test IPublishedContent nodes
					//TODO: Convert to test objects

					return new List<Test>();

				});
		}

		public void ClearTestsCache()
		{
			ApplicationContext.Current.ApplicationCache.RuntimeCache.ClearCacheItem(Constants.AllTestsCacheKey);
		}

		public void ActivateInactiveTests()
		{
			var testsToActivate = GetTests(true).Where(x => !x.Active
				&& (x.StartDate > DateTime.MinValue && x.StartDate < DateTime.Now)
				&& x.EndDate > DateTime.Now);


		}

		public void DeactivateActiveTests()
		{
			
		}
	}
}
