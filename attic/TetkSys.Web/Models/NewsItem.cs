using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace TetkSys.Web.Models
{
	public class NewsItem
	{
		public string Code { get; set; }
		public string Title { get; set; }
		public DateTime Date { get; set; }
		public XElement Content { get; set;  }
	}
}