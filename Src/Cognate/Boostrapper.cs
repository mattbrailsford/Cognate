using Cognate.Cache;
using Cognate.Data;
using Cognate.Routing;
using Umbraco.Core;

namespace Cognate
{
	public class Boostrapper : ApplicationEventHandler
	{
		protected override void ApplicationStarting(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
		{
			DataManager.Init();
			CacheManager.Init();
			RouteHandler.Init();
		}
	}
}
