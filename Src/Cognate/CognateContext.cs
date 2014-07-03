using System;
using Cognate.Data.Repositories;
using Cognate.Services;

namespace Cognate
{
	internal class CognateContext
	{
		#region Singleton
		
		private static readonly Lazy<CognateContext> _instance = new Lazy<CognateContext>(() => new CognateContext());

		private CognateContext()
		{
			Services = new CognateServiceContext();
			Repositories = new CognateRepositoryContext();
		}

		public static CognateContext Instance
		{
			get { return _instance.Value; }
		}

		#endregion

		public virtual CognateServiceContext Services { get; private set; }
		public virtual CognateRepositoryContext Repositories { get; private set; }
	}

	internal class CognateRepositoryContext
	{
		private readonly Lazy<TestVariantScoreRepository> _testVariantScoreRepository;

		public CognateRepositoryContext()
		{
			_testVariantScoreRepository = new Lazy<TestVariantScoreRepository>(() => new TestVariantScoreRepository());
		}

		public virtual TestVariantScoreRepository TestVariantScoreRepository 
		{
			get { return _testVariantScoreRepository.Value; }
		}
	}

	internal class CognateServiceContext
	{
		private readonly Lazy<TestService> _testService;

		public CognateServiceContext()
		{
			_testService = new Lazy<TestService>(() => new TestService());
		}

		public virtual TestService TestService
		{
			get { return _testService.Value; }
		}
	}
}
