using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System;
using System.Runtime.Serialization;
using System.Xml.Linq;

namespace Tekt.Core.Data
{
	[DataContract(Name = "entry")]
	public sealed class IndexEntry
	{
		public static IndexEntry ReadEntry(XElement elem, string basePath = null)
		{
			return new IndexEntry
				{
					Id = (string)elem.Attribute("id"),
					Path = basePath + (string)elem.Attribute("path"),
					Title = (string)elem.Attribute("title")
				};
		}

		public static IEnumerable<IndexEntry> ReadEntries(XElement root)
		{
			var basePath = (string)root.Attribute("basepath");
			return root.Elements("entry").Select(e => ReadEntry(e, basePath));
		}

		[DataMember(IsRequired = true)]
		public string Id { get; set; }
		[DataMember(IsRequired = true)]
		public string Path { get; set; }
		[DataMember]
		public string Title { get; set; }
	}
}