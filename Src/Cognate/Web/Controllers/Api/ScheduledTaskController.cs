using System.Web.Http;
using Umbraco.Web.Mvc;
using Umbraco.Web.WebApi;

namespace Cognate.Web.Controllers.Api
{
	[PluginController("Cognate")]
	public class ScheduledTaskController : UmbracoApiController
	{
		[HttpGet]
		public bool Run()
		{
			//TODO: Check to see if any tests need to activate
			//TODO: Check to see if any tests need to deactivate
			//TODO: Check to see if any tests should end due to max lead

			return true;
		}
	}
}
