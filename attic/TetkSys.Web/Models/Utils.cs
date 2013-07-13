using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace TetkSys.Web.Models
{
	public class XNodeHtmlString : IHtmlString
	{
		private readonly XNode node;
		public XNodeHtmlString(XNode node)
		{
			this.node = node;
		}
		public string ToHtmlString()
		{
			return node.ToString();
		}
	}
}