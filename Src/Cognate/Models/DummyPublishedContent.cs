using System;
using System.Collections.Generic;
using Umbraco.Core.Models;
using Umbraco.Core.Models.PublishedContent;

namespace Cognate.Models
{
	// Used purely to force the umbraco pipeline to forget the current node
	// so that we can replace it with a node with the same id as the previous 
	// node but with different values (ie, the variant)
	internal class DummyPublishedContent : IPublishedContent
	{
		public DummyPublishedContent(int id, string name)
		{
			Id = id;
			Name = name;
		}

		#region IPublishedContent Members

		public IEnumerable<IPublishedContent> ContentSet { get; private set; }
		public PublishedContentType ContentType { get; private set; }
		public int Id { get; private set; }
		public int TemplateId { get; private set; }
		public int SortOrder { get; private set; }
		public string Name { get; private set; }
		public string UrlName { get; private set; }
		public string DocumentTypeAlias { get; private set; }
		public int DocumentTypeId { get; private set; }
		public string WriterName { get; private set; }
		public string CreatorName { get; private set; }
		public int WriterId { get; private set; }
		public int CreatorId { get; private set; }
		public string Path { get; private set; }
		public DateTime CreateDate { get; private set; }
		public DateTime UpdateDate { get; private set; }
		public Guid Version { get; private set; }
		public int Level { get; private set; }
		public string Url { get; private set; }
		public PublishedItemType ItemType { get; private set; }
		public bool IsDraft { get; private set; }
		public IPublishedContent Parent { get; private set; }
		public IEnumerable<IPublishedContent> Children { get; private set; }
		public ICollection<IPublishedProperty> Properties { get; private set; }

		public object this[string alias]
		{
			get { return null; }
		}

		public int GetIndex()
		{
			return 0;
		}

		public IPublishedProperty GetProperty(string alias)
		{
			return null;
		}

		public IPublishedProperty GetProperty(string alias, bool recurse)
		{
			return null;
		}

		#endregion
	}
}
