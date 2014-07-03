using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cognate.Data
{
	internal class DataManager
	{
		public static void Init()
		{
			EnsureDatabaseTables();
		}

		private static void EnsureDatabaseTables()
		{
			CognateContext.Instance.Repositories.TestVariantScoreRepository.EnsureDatabaseTable();
		}
	}
}
