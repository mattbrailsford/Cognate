using Cognate.Models;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Web.Routing;

namespace Cognate.Extensions
{
	internal static class PublishedContentRequestExtensions
	{
		public static void SetContentVariant(this PublishedContentRequest request, IPublishedContent variant)
		{
			// Construct the variant
			var variantContent = new PublishedContentVariant(request.PublishedContent, variant);

			// Force the request context to forget the current node (doesn't like it when you try to replace a node with a node with the same id)
			request.PublishedContent = new DummyPublishedContent(-1, "Dummy");

			// Now force the internal redirect to our custom node
			request.SetInternalRedirectPublishedContent(variantContent);

			// Force the template change
			request.SetTemplate(ApplicationContext.Current.Services.FileService.GetTemplate(variantContent.TemplateId));
		}
	}
}
