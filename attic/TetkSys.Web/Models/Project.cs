using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace TetkSys.Web.Models
{
	public class Project
	{
		public string Code { get; set; }
		public string Title { get; set; }
		public XElement Description { get; set; }
		public IHtmlString DescriptionHtml
		{
			get { return new XNodeHtmlString(Description); }
		}

		public Project() { }
		public Project(XElement elem)
		{
			this.Code = (string) elem.Attribute("code");
			this.Title = (string) elem.Attribute("title");
			this.Description = elem.Element("description");
		}
	}
}