using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tekt.Core.Data;

namespace Tekt.Web.Controllers
{
	public class ProjectsController : Controller
	{
		//
		// GET: /Projects/

		public ActionResult Index()
		{
			return View(TektData.Instance.GetProjects());
		}

		//
		// GET: /Projects/Details/{id}
		public ActionResult Details(string id)
		{
			var project = TektData.Instance.GetProject(id);
			if(project == null)
				return HttpNotFound("Project not found: " + id);
			return View("Details", new Dictionary<string, object>
				{
					{"project", project},
					{"items", TektData.Instance.GetProjectItems(project)}
				});
		}

		public ActionResult Items(string id)
		{
			var project = TektData.Instance.GetProject(id);
			if(project == null)
				return HttpNotFound("Project not found: " + id);
			var items = TektData.Instance.GetProjectItems(project);
			return View("Items", items);
		}

	}
}
