using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Tekt.Core.Data;

namespace Tekt.Core.Entities
{
	public sealed class BlogEntry : TektEntity
	{
		public static BlogEntry ReadEntry(XElement elem)
		{
			return new BlogEntry
				{
					Id = (string)elem.Element(TektData.atomNS + "id"),
					Posted = (DateTime)elem.Element(TektData.atomNS + "published"),
					Categories = elem.Elements(TektData.atomNS + "category").Select(e => (string)e.Attribute("term")).ToList(),
					Title = (string)elem.Element(TektData.atomNS + "title"),
					Content = (string)elem.Element(TektData.atomNS + "content")
				};
		}

		public static IEnumerable<BlogEntry> ReadEntries(XElement root)
		{
			return root.Elements(TektData.atomNS + "entry").Select(ReadEntry);
		}

		public IList<string> Categories { get; set; }
		public string Content { get; set; }

	}
}
