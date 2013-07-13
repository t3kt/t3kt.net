using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TetkSys.Web.Models
{
	public class ProjectData
	{
		public ImageSet Images { get; set; }
		public IList<String> VideoIds { get; set; } 
	}
}