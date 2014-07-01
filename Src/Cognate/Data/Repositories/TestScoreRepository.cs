using System.Linq;
using System.Web;
using Cognate.Data.Repositories;
using Cognate.Models;

[assembly: PreApplicationStartMethod(typeof(TestScoreRepository), "Initialize")]

namespace Cognate.Data.Repositories
{
	internal class TestScoreRepository : AbstractRepository<TestScore>
	{
		public int GetScore(int testId, int variantId)
		{
			var testScore = All()
				.SingleOrDefault(x => x.TestId == testId && x.VariantId == variantId);

			if (testScore != null)
			{
				return testScore.Score;
			}

			return 0;
		}

		public int IncrementScore(int testId, int variantId)
		{
			var testScore = All()
				.SingleOrDefault(x => x.TestId == testId && x.VariantId == variantId);
			
			if (testScore != null)
			{
				testScore.Score++;
			}
			else
			{
				testScore = new TestScore
				{
					TestId = testId,
					VariantId = variantId,
					Score = 1
				};
			}

			Save(testScore);

			return testScore.Score;
		}

		public int GetTotal(int testId)
		{
			return All().Where(x => x.TestId == testId).Sum(x => x.Score);
		}

		public int GetBestVariantId(int testId)
		{
			return All().Where(x => x.TestId == testId)
				.OrderBy(x => x.Score)
				.Select(x => x.VariantId)
				.FirstOrDefault();
		}

		public static void Initialize()
		{
			new TestScoreRepository().EnsureDatabaseTable();
		}
	}
}
