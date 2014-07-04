using System.Linq;
using Cognate.Services;
using Umbraco.Core.Models;
using Umbraco.Core.Services;

namespace Cognate.Cache
{
	internal class CacheManager
	{
		public static void Init()
		{
			// Clear the test cache if any tests are published
			ContentService.Published += (sender, args) =>
			{
				if (args.PublishedEntities.Any(x => x.ContentType.Alias == "CognateTest" ||
					(x.Parent() != null && x.Parent().ContentType.Alias == "CognateTest")))
				{
					CognateContext.Instance.Services.TestService.ClearTestsCache();
				}
			};
		}
	}
}
