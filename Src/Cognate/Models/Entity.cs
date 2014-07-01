using Umbraco.Core.Persistence;
using Umbraco.Core.Persistence.DatabaseAnnotations;

namespace Cognate.Models
{
	[PrimaryKey("Id")]
	internal abstract class Entity
	{
		[PrimaryKeyColumn]
		public long Id { get; set; }
	}
}
