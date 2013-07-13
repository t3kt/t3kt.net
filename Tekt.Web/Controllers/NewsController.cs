using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Tekt.Core.Data;

namespace Tekt.Web.Controllers
{
	public class NewsController : Controller
	{
		public ActionResult Index()
		{
			return View(TektData.Instance.GetBlogEntries());
		}

		public ActionResult Category(string id)
		{
			ViewBag.Category = id;
			return View("Index", TektData.Instance.GetBlogEntries(id));
		}

	}
}
