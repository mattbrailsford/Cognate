using System;
using System.Web;
using System.Xml;
using Cognate.Util;
using umbraco.cms.businesslogic.packager.standardPackageActions;
using Umbraco.Core.Logging;
using umbraco.interfaces;

namespace Cognate.PackageActions
{
	/// <summary>
	/// Adds a scheduled task to the scheduled tasks section of the UmbracoSettings.config file
	/// </summary>
	public class AddScheduledTask : IPackageAction
	{
		#region IPackageAction Members

		public string Alias()
		{
			return "Cognate_AddScheduledTask";
		}

		public bool Execute(string packageName, XmlNode xmlData)
		{
			var result = false;

			//Get attribute values
			var log = XmlUtil.GetAttributeValueFromNode(xmlData, "log").ToLower() == "true" ? "true" : "false";
			var scheduledTaskAlias = XmlUtil.GetAttributeValueFromNode(xmlData, "scheduledTaskAlias");
			var interval = XmlUtil.GetAttributeValueFromNode(xmlData, "interval");
			var url = ParseUrl(XmlUtil.GetAttributeValueFromNode(xmlData, "url"));

			//Open the Umbraco Settings config file
			var umbracoSettingsFile = Umbraco.Core.XmlHelper.OpenAsXmlDocument("/config/umbracoSettings.config");

			//Select scheduled tasks node from the settings file
			var scheduledTaskRootNode = umbracoSettingsFile.SelectSingleNode("//scheduledTasks");

			//Create a new scheduled task node 
			var scheduledTaskNode = (XmlNode)umbracoSettingsFile.CreateElement("task");

			//Append addributes
			scheduledTaskNode.Attributes.Append(Umbraco.Core.XmlHelper.AddAttribute(umbracoSettingsFile, "log", log));
			scheduledTaskNode.Attributes.Append(Umbraco.Core.XmlHelper.AddAttribute(umbracoSettingsFile, "alias", scheduledTaskAlias));
			scheduledTaskNode.Attributes.Append(Umbraco.Core.XmlHelper.AddAttribute(umbracoSettingsFile, "interval", interval));
			scheduledTaskNode.Attributes.Append(Umbraco.Core.XmlHelper.AddAttribute(umbracoSettingsFile, "url", url));


			//Append the new rewrite scheduled task to the Umbraco Settings config file
			scheduledTaskRootNode.AppendChild(scheduledTaskNode);

			//Save the Umbraco Settings config file with the new Scheduled task
			umbracoSettingsFile.Save(HttpContext.Current.Server.MapPath("/config/umbracoSettings.config"));

			//No errors so the result is true
			result = true;

			return result;
		}

		/// <summary>
		/// Sample xml
		/// </summary>
		public XmlNode SampleXml()
		{
			var sample = "<Action runat=\"install\" undo=\"true/false\" alias=\"Eddc_AddScheduledTask\" scheduledTaskAlias=\"myscheduledtask\" log=\"true\" interval=\"60\"  url=\"/myscheduledpage.aspx\"></Action>";
			return helper.parseStringToXmlNode(sample);
		}

		/// <summary>
		/// Removes the scheduled task from the UmbracoSettings.config file
		/// </summary>
		public bool Undo(string packageName, System.Xml.XmlNode xmlData)
		{
			var result = false;

			//Get alias to remove
			var scheduledTaskAlias = XmlUtil.GetAttributeValueFromNode(xmlData, "scheduledTaskAlias");

			//Open the Umbraco Settings config file
			var umbracoSettingsFile = Umbraco.Core.XmlHelper.OpenAsXmlDocument("/config/umbracoSettings.config");

			//Select scheduled tasks root node from the settings file
			var scheduledTaskRootNode = umbracoSettingsFile.SelectSingleNode("//scheduledTasks");

			//Get the child node with the scheduled task we want to remove
			//Select the url rewrite rule by name from the config file
			var scheduledTaskNode = scheduledTaskRootNode.SelectSingleNode("//task[@alias = '" + scheduledTaskAlias + "']");

			if (scheduledTaskNode != null)
			{
				//Child node is not null, remove it
				scheduledTaskRootNode.RemoveChild(scheduledTaskNode);

				//Save the modified configuration file
				umbracoSettingsFile.Save(HttpContext.Current.Server.MapPath("/config/umbracoSettings.config"));
			}

			return result;
		}

		#endregion

		#region Helper Methods

		/// <summary>
		/// Takes the url determines if its a virtual url, if so convert it to an absolute url
		/// </summary>
		/// <param name="url">The url to parse</param>
		private string ParseUrl(string url)
		{
			try
			{
				if (url.StartsWith("~"))
				{
					// Not allowed skip it
					url = url.Substring(1);
				}

				if (url.StartsWith("/"))
				{
					var u = new UriBuilder
					{
						Host = HttpContext.Current.Request.Url.Host,
						Path = url
					};

					url = u.Uri.ToString();
				}
			}
			catch (Exception ex)
			{
				LogHelper.Error<AddScheduledTask>("Error parsing the url for AddScheduledTask package action ", ex);
			}

			//Return the url
			return url;
		}
		#endregion
	}

}
