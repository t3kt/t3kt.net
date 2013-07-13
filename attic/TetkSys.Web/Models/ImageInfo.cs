using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace TetkSys.Web.Models
{
	public class ImageSet
	{
		public string BasePath { get; set; }
		public IEnumerable<ImageInfo> Images { get; set; }
		public ImageSet() { }
		public ImageSet(XElement root)
		{
			this.BasePath = (string) root.Attribute("base");
			this.Images = root.Elements("image").Select(e => new ImageInfo(e)).ToList();
		}
	}
	public class ImageInfo
	{
		public string Path { get; set; }
		public string ThumbPath { get; set; }
		public string AltText { get; set; }
		public ImageInfo() { }
		public ImageInfo(XElement elem)
		{
			this.Path = (string) elem.Attribute("full");
			this.ThumbPath = (string) elem.Attribute("thumb");
			this.AltText = (string) elem.Attribute("alt");
		}
	}
}