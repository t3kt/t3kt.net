using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Xml.Linq;

namespace Tekt.Core.Web
{
	public static class UIHelpers
	{
		public static IHtmlString Tag(this HtmlHelper html, string tagName, string cssClass = null, string text = null, IDictionary<string, object> attributes = null)
		{
			var tag = new TagBuilder(tagName);
			if(attributes != null)
			{
				foreach(var entry in attributes)
				{
					var value = entry.Value;
					if(value == null || (value is string && String.IsNullOrEmpty((string)value)))
						continue;
					tag.Attributes[entry.Key.ToLower()] = entry.Value.ToString();
				}
			}
			if(cssClass != null)
				tag.AddCssClass(cssClass);
			if(text != null)
				tag.SetInnerText(text);
			return new HtmlString(tag.ToString());
		}
		public static IHtmlString Tag(this HtmlHelper html, string tagName, string cssClass = null, string text = null, object attributes = null)
		{
			return Tag(html, tagName, cssClass, text, new RouteValueDictionary(attributes));
		}

		public static IHtmlString ContentXml(this HtmlHelper html, XContainer container)
		{
			var xml = new StringBuilder();
			foreach(var child in container.Nodes())
				xml.Append(child);
			return new HtmlString(xml.ToString());
		}
	}
}
