using Umbraco.Web.Routing;

namespace Cognate.Routing
{
	internal class RouteHandler
	{
		public static void Init()
		{
			PublishedContentRequest.Prepared += (sender, args) =>
			{
				var req = sender as PublishedContentRequest;
				if (req == null) return;

				//TODO: Check to see if this is a source page request
				// HandleSourcePageRequest(req);

				//TODO: Check to see if this is a target page request
				// HandleTargetPageRequest(req);
			};
		}

		private static void HandleSourcePageRequest(PublishedContentRequest req)
		{
			//TODO: Check for cookie
			//TODO: Select variant to display
			//TODO: Drop cookie
			//TODO: Redirect view
			//req.SetInternalRedirectPublishedContent();
		}

		private static void HandleTargetPageRequest(PublishedContentRequest req)
		{
			//TODO: Pickup cookie
			//TODO: Increment variant score
		}
	}
}
