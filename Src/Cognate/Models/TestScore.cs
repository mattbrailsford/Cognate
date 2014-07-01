using Umbraco.Core.Persistence;

namespace Cognate.Models
{
	[TableName("cognateTestScore")]
	internal class TestScore : Entity
	{
		public int TestId { get; set; }
		public int VariantId { get; set; }
		public int Score { get; set; }
	}
}
