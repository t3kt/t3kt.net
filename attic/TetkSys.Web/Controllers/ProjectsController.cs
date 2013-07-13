using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using TetkSys.Core.Data;
using TetkSys.Core.Web;
using TetkSys.Web.Models;

namespace TetkSys.Web.Controllers
{
	public class ProjectsController : Controller
	{
		//
		// GET: /Projects/

		public ActionResult Index()
		{
			return View(TetkDB.Instance.GetProjects());
		}

		//
		// GET: /Projects/Details/{id}
		public ActionResult Details(string id)
		{
			var project = TetkDB.Instance.GetProject(id);
			if(project == null)
				return HttpNotFound("Project: " + id);
			return View("Details", project);
		}

	}
}
