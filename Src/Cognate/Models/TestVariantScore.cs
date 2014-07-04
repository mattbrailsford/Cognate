using Umbraco.Core.Persistence;

namespace Cognate.Models
{
	[TableName("cognateTestVariantScore")]
	internal class TestVariantScore : Entity
	{
		public long TestId { get; set; }
		public int VariantId { get; set; }
		public int Score { get; set; }
	}
}
