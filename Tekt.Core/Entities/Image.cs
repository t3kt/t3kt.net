using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Tekt.Core.Data;

namespace Tekt.Core.Entities
{
	public class Image : TektEntity
	{
		public static Image ReadImage(XElement elem, string basePath = null)
		{
			return new Image
			{
				Title = (string)elem.Attribute("title"),
				ThumbPath = basePath + (string)elem.Attribute("thumb"),
				Posted = (DateTime?)elem.Attribute("posted"),
				Path = basePath + (string)elem.Attribute("path"),
				Width = (int?)elem.Attribute("width"),
				Height = (int?)elem.Attribute("height"),
				ThumbWidth = (int?)elem.Attribute("thumbwidth"),
				ThumbHeight = (int?)elem.Attribute("thumbheight")
			};
		}

		public static Image ReadFeedImage(XElement elem)
		{
			var thumb = elem.Element(TektData.mediaNS + "thumbnail");
			var content = elem.Element(TektData.mediaNS + "content");
			var pubDate = elem.Element("pubDate");
			return new Image
				{
					Title = (string)elem.Element("title"),
					Posted = pubDate == null ? null : (DateTime?)DateTime.Parse(pubDate.Value),
					ThumbPath = thumb == null ? null : (string)thumb.Attribute("url"),
					ThumbWidth = thumb == null ? null : (int?)thumb.Attribute("width"),
					ThumbHeight = thumb == null ? null : (int?)thumb.Attribute("height"),
					Path = content == null ? null : (string)content.Attribute("url"),
					Width = content == null ? null : (int?)content.Attribute("width"),
					Height = content == null ? null : (int?)content.Attribute("height")
				};
		}

		public string Path { get; set; }
		public int? ThumbWidth { get; set; }
		public int? ThumbHeight { get; set; }
		public int? Width { get; set; }
		public int? Height { get; set; }

	}
}
