using System.Web.Http;
using Cognate.Services;
using Umbraco.Web.Mvc;
using Umbraco.Web.WebApi;

namespace Cognate.Web.Controllers.Api
{
	[PluginController("Cognate")]
	public class ScheduledTaskController : UmbracoApiController
	{
		private readonly TestService _testService;

		public ScheduledTaskController()
		{
			_testService = new TestService();
		}

		[HttpGet]
		public bool Run()
		{
			_testService.ActivateInactiveTests();

			//TODO: Check to see if any tests need to activate
			//TODO: Check to see if any tests need to deactivate
			//TODO: Check to see if any tests should end due to max lead
			
			return true;
		}
	}
}
