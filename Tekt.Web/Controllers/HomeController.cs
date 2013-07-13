using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Tekt.Core.Data;
using Tekt.Core.Web;

namespace Tekt.Web.Controllers
{
	public class HomeController : Controller
	{
		//
		// GET: /Home/

		public ActionResult Index()
		{
			return View();
		}

		public ActionResult About()
		{
			return View();
		}

		private IEnumerable<SitemapEntry> GetSitemapEntries()
		{
			var requestUrl = Request.Url;
			var prefix = requestUrl.Scheme + "://" + requestUrl.Host;
			if(requestUrl.Port != 80)
				prefix += ":" + requestUrl.Port;
			yield return new SitemapEntry(prefix + Url.Action("Index", "Home"))
				{
					ChangeFrequency = ChangeFrequency.Daily,
					Priority = 1.0m
				};
			yield return new SitemapEntry(prefix + Url.Action("Index", "Projects"))
				{
					ChangeFrequency = ChangeFrequency.Weekly,
					Priority = 1.0m
				};
			yield return new SitemapEntry(prefix + Url.Action("Index", "News"))
				{
					ChangeFrequency = ChangeFrequency.Hourly,
					Priority = 1.0m
				};
			foreach(var project in TektData.Instance.GetProjects())
			{
				yield return new SitemapEntry(prefix + Url.Action("Details", "Projects", new { id = project.Id }))
					{
						ChangeFrequency = ChangeFrequency.Daily,
						Priority = 0.8m
					};
			}
		}

		public ActionResult Sitemap()
		{
			var entries = GetSitemapEntries();
			var root = SitemapEntry.WriteUrlsetElement(entries);
			return Content(root.ToString(), "text/xml");
		}

	}
}
