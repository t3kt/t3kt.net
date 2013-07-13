using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml.Linq;

namespace TetkSys.Web.Models
{
	public class SiteData
	{

		private static SiteData instance;
		public static SiteData Get()
		{
			return instance ?? (instance = new SiteData());
		}

		private static string MapPath(string path)
		{
			return HttpContext.Current.Server.MapPath(path);
		}
		private static readonly Regex projectCodeRegex = new Regex( @"^[a-zA-Z0-9\-]{2,100}$" );
		private readonly string dataRoot;

		public SiteData()
		{
			dataRoot = MapPath("~/App_Data/");
		}

		private XElement GetProjectIndexRoot()
		{
			return XElement.Load(dataRoot + "Projects/index.xml");
		}

		public Project GetProject(String code)
		{
			if (String.IsNullOrWhiteSpace(code))
				return null;
			if (!projectCodeRegex.IsMatch(code))
				return null;
			var indexElem = this.GetProjectIndexRoot().Elements("project").SingleOrDefault(e => (string)e.Attribute("code") == code);
			if (indexElem == null)
				return null;
			return this.LoadProject(indexElem);
		}
		private Project LoadProject(XElement indexElem)
		{
			if (indexElem == null)
				return null;
			var file = dataRoot + "Projects/" + (string)indexElem.Attribute("path");
			var elem = XElement.Load(file);
			return new Project(elem);
		}
		public IEnumerable<Project> GetProjects()
		{
			return this.GetProjectIndexRoot().Elements("project").Select(this.LoadProject);
		}
	}
}