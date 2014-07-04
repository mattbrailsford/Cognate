using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using Cognate.Extensions;
using Cognate.Models;
using Umbraco.Core;
using Umbraco.Core.Logging;
using Umbraco.Core.Models;
using Umbraco.Web.Routing;

namespace Cognate.Routing
{
	internal class RouteHandler
	{
		private const string QueryStringPatternMatchFormat = @"(\?|&){0}(&|$)";

		public static void Init()
		{
			PublishedContentRequest.Prepared += (sender, args) =>
			{
				var req = sender as PublishedContentRequest;
				if (req == null) return;

				if(req.PublishedContent == null) return;

				var contentId = req.PublishedContent.Id;
				var activeTests = CognateContext.Instance.Services.TestService.GetActiveTests();
				var activeTest = activeTests.FirstOrDefault(x => x.SourcePageId == contentId || x.TargetPageId == contentId);

				if (activeTest != null)
				{
					if (activeTest.SourcePageId == contentId)
					{
						HandleSourcePageRequest(req, activeTest);
					}
					else if (activeTest.TargetPageId == contentId)
					{
						HandleTargetPageRequest(req, activeTest);
					}
				}

				UpdateRefererForNextRequest(req);
			};
		}

		private static void HandleSourcePageRequest(PublishedContentRequest req, Test activeTest)
		{
			var cookieName = string.Format(Constants.TestCookieKeyFormat, activeTest.Id);
			var cookieValue = HttpContext.Current.Request.Cookies[cookieName];

			IPublishedContent variant = null;

			if (cookieValue != null)
			{
				// Lookup variant from cookie
				int variantId;
				if (int.TryParse(cookieValue.Value, out variantId))
				{
					variant = activeTest.Content.Children.FirstOrDefault(x => x.Id == variantId);
				}
			}
			
			if(variant == null)
			{
				// No cookie set so, or no such variant found just choose random child
				variant = activeTest.Content
					.Children.SingleRandomOrDefault();

				// Store the selection for next time
				HttpContext.Current.Response.Cookies
					.Add(new HttpCookie(cookieName, variant.Id.ToInvariantString())
					{
						Expires = activeTest.EndDate != DateTime.MaxValue ? activeTest.EndDate : DateTime.Now.AddYears(1)
					});
			}

			req.SetContentVariant(variant); 
		}

		private static void HandleTargetPageRequest(PublishedContentRequest req, Test activeTest)
		{
			// Get the refering pages id
			var refererId = GetRefererId();

			// Check it matches the active test source and that the querystring is valid
			if (refererId == activeTest.SourcePageId && 
				(activeTest.TargetQueryString.IsNullOrWhiteSpace() || Regex.IsMatch(HttpContext.Current.Request.Url.Query,
					string.Format(QueryStringPatternMatchFormat, activeTest.TargetQueryString), RegexOptions.IgnoreCase)))
			{
				// Whoop! Goal achieved, lets find out what varient they saw
				var testCookieName = string.Format(Constants.TestCookieKeyFormat, activeTest.Id);
				var testCookie = HttpContext.Current.Request.Cookies[testCookieName];

				int variantId;
				if (testCookie != null && int.TryParse(testCookie.Value, out variantId))
				{
					// Variant found, so increment the score
					CognateContext.Instance.Services.TestService.IncrementVariantScore(activeTest.Id, variantId);
				}
				else
				{
					LogHelper.Warn<RouteHandler>(string.Format("Test '{0}' converted but no variant view was found",
						activeTest.Name));
				}
			}
		}

		private static int GetRefererId()
		{
			// Get the referer id from the last request
			var refererCookie = HttpContext.Current.Request.Cookies[Constants.RefererCookieKey];
			if (refererCookie == null) 
				return 0;

			int refererId;
			return int.TryParse(refererCookie.Value, out refererId)
				? refererId
				: 0;
		}

		private static void UpdateRefererForNextRequest(PublishedContentRequest req)
		{
			// Save history to cookie
			HttpContext.Current.Response.Cookies.Add(new HttpCookie(Constants.RefererCookieKey, 
				req.PublishedContent.Id.ToInvariantString()));
		}
	}
}
