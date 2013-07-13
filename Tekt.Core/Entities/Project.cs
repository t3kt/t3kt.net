using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Tekt.Core.Entities
{
	public class Project
	{
		public static Project ReadProject(XElement elem)
		{
			var basePath = (string)elem.Attribute("basepath");
			var mainImgElem = elem.Element("mainimage");
			var blogCats = (string)elem.Attribute("blogcategories");
			var flickrSetId = (string)elem.Attribute("flickrsetid");
			var vimeoAlbumId = (string)elem.Attribute("vimeoalbumid");
			var items = elem.Elements()
							.Select(e => TektEntity.ReadItem(e, basePath))
							.Where(x => x != null);
			return new Project
				{
					Id = (string)elem.Attribute("id"),
					Title = (string)elem.Attribute("title"),
					Description = elem.Element("description"),
					Summary = elem.Element("summary"),
					BlogCategories = blogCats == null ? new List<string>() : blogCats.Split(' ').ToList(),
					Items = items.ToList(),
					MainImage = mainImgElem == null ? null : Image.ReadImage(mainImgElem, basePath),
					FlickrSetId = flickrSetId,
					VimeoAlbumId = vimeoAlbumId,
					GithubRepo = (string)elem.Attribute("githubrepo")
				};
		}

		public string Id { get; set; }
		public string Title { get; set; }
		public XElement Description { get; set; }
		public XElement Summary { get; set; }

		public List<string> BlogCategories { get; set; }

		public Image MainImage { get; set; }

		public List<TektEntity> Items { get; set; }

		public string FlickrSetId { get; set; }
		public string VimeoAlbumId { get; set; }
		public string GithubRepo { get; set; }
	}
}
