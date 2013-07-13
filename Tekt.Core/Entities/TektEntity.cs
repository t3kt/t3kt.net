using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Tekt.Core.Data;

namespace Tekt.Core.Entities
{
	public class TektEntity
	{
		public static TektEntity ReadItem(XElement elem, string basePath = null)
		{
			if(elem.Name == "image")
				return Image.ReadImage(elem, basePath);
			if(elem.Name == "video")
				return Video.ReadVideo(elem, basePath);
			return null;
		}

		public string Id { get; set; }
		public string Title { get; set; }
		public string ThumbPath { get; set; }
		public DateTime? Posted { get; set; }
		public string DetailUrl { get; set; }
	}
}
