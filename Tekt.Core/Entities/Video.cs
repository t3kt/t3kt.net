using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Tekt.Core.Entities
{
	public class Video : TektEntity
	{
		public static Video ReadVideo(XElement elem, string basePath = null)
		{
			return new Video
				{
					Title = (string)elem.Attribute("title"),
					ThumbPath = basePath + (string)elem.Attribute("thumb"),
					Posted = (DateTime?)elem.Attribute("posted"),
					DetailUrl = (string)elem.Attribute("viewurl"),
					EmbedUrl = (string)elem.Attribute("embedurl"),
					EmbedWidth = (int?)elem.Attribute("embedwidth"),
					EmbedHeight = (int?)elem.Attribute("embedheight")
				};
		}

		public string ViewUrl { get; set; }
		public string EmbedUrl { get; set; }
		public int? EmbedWidth { get; set; }
		public int? EmbedHeight { get; set; }

		public string Embed { get; set; }
	}
}
