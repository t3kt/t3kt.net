using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace Tekt.Web
{
	public class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(
				name: "HomeNews",
				url: "",
				defaults: new { controller = "News", action = "Index" });
			routes.MapRoute(
				name: "About",
				url: "About",
				defaults: new { controller = "Home", action = "About" });
			routes.MapRoute(
				name: "SitemapXml",
				url: "sitemap.xml",
				defaults: new { controller = "Home", action = "Sitemap" });
			routes.MapRoute(
				name: "Sitemap",
				url: "sitemap",
				defaults: new { controller = "Home", action = "Sitemap" });
			routes.MapRoute(
				name: "ProjectDetail",
				url: "Projects/{id}",
				defaults: new { controller = "Projects", action = "Details" },
				constraints: new { id = "[a-zA-Z0-0]+" });
			routes.MapRoute(
				name: "Default",
				url: "{controller}/{action}/{id}",
				defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
			);
		}
	}
}