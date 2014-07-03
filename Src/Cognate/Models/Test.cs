using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Models;

namespace Cognate.Models
{
	internal class Test : Entity
	{
		public string Name { get; set; }
		public int SourcePageId { get; set; }
		public int TargetPageId { get; set; }
		public string TargetQueryString { get; set; }
		public int MaxLead { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public bool Active { get; set; }

		internal IPublishedContent Content { get; set; }
	}
}
