using System;
using System.Collections.Generic;
using Umbraco.Core.Models;

namespace Cognate.Models
{
	//TODO: Decide which properties should be proxied back to source

	public class PublishedContentVariant : IPublishedContent
	{
		private readonly IPublishedContent _source;
		private readonly IPublishedContent _variant;

		internal PublishedContentVariant(IPublishedContent source, IPublishedContent variant)
		{
			_source = source;
			_variant = variant;
		}

		public IEnumerable<IPublishedContent> Children
		{
			get { return _source.Children; }
		}

		public IEnumerable<IPublishedContent> ContentSet
		{
			get { return _source.ContentSet; }
		}

		public Umbraco.Core.Models.PublishedContent.PublishedContentType ContentType
		{
			get { return _variant.ContentType; }
		}

		public DateTime CreateDate
		{
			get { return _variant.CreateDate; }
		}

		public int CreatorId
		{
			get { return _variant.CreatorId; }
		}

		public string CreatorName
		{
			get { return _variant.CreatorName; }
		}

		public string DocumentTypeAlias
		{
			get { return _variant.DocumentTypeAlias; }
		}

		public int DocumentTypeId
		{
			get { return _variant.DocumentTypeId; }
		}

		public int GetIndex()
		{
			return _source.GetIndex();
		}

		public IPublishedProperty GetProperty(string alias, bool recurse)
		{
			// Because we are faking being the original node, if we are asked
			// for a recursive property, check ourselves and than pass the 
			// recursive call off to the original nodes parent

			var prop = _variant.GetProperty(alias);

			if (prop != null || !recurse) 
				return prop;

			return _source.Parent == null 
				? null 
				: _source.Parent.GetProperty(alias, true);
		}

		public IPublishedProperty GetProperty(string alias)
		{
			return _variant.GetProperty(alias);
		}

		public int Id
		{
			get { return _source.Id; }
		}

		public bool IsDraft
		{
			get { return _variant.IsDraft; }
		}

		public PublishedItemType ItemType
		{
			get { return _variant.ItemType; }
		}

		public int Level 
		{
			get { return _source.Level; }
		}

		public string Name
		{
			get { return _source.Name; }
		}

		public IPublishedContent Parent
		{
			get { return _source.Parent; }
		}

		public string Path
		{
			get { return _source.Path; }
		}

		public ICollection<IPublishedProperty> Properties
		{
			get { return _variant.Properties; }
		}

		public int SortOrder
		{
			get { return _source.SortOrder; }
		}

		public int TemplateId
		{
			get { return _variant.TemplateId; }
		}

		public DateTime UpdateDate
		{
			get { return _variant.UpdateDate; }
		}

		public string Url
		{
			get { return _source.Url; }
		}

		public string UrlName
		{
			get { return _source.UrlName; }
		}

		public Guid Version
		{
			get { return _variant.Version; }
		}

		public int WriterId
		{
			get { return _variant.WriterId; }
		}

		public string WriterName
		{
			get { return _variant.WriterName; }
		}

		public object this[string alias]
		{
			get { return _variant[alias]; }
		}
	}
}
