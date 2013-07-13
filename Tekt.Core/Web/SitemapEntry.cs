using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Tekt.Core.Web
{
	public enum ChangeFrequency
	{
		Always,
		Hourly,
		Daily,
		Weekly,
		Monthly,
		Yearly,
		Never
	}
	public class SitemapEntry
	{

		public static XElement WriteUrlsetElement(IEnumerable<SitemapEntry> entries)
		{
			return new XElement(NS + "urlset",
								from entry in entries
								select entry.WriteElement());
		}

		private static readonly XNamespace NS = "http://www.sitemaps.org/schemas/sitemap/0.9";

		public string Location { get; private set; }

		public DateTime? LastModified { get; set; }

		public ChangeFrequency? ChangeFrequency { get; set; }

		public decimal? Priority { get; set; }

		public SitemapEntry(string location)
		{
			this.Location = location;
		}

		public XElement WriteElement()
		{
			return new XElement(NS + "url",
								new XElement(NS + "loc", this.Location),
								this.LastModified == null ? null : new XElement(NS + "lastmod", this.LastModified.Value),
								this.ChangeFrequency == null ? null : new XElement(NS + "changefreq", this.ChangeFrequency.Value.ToString().ToLower()),
								this.Priority == null ? null : new XElement(NS + "priority", this.Priority.Value));
		}

	}
}
