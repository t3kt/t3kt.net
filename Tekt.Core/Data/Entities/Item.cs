using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Tekt.Core.Data.Entities
{
	partial class Item
	{

		private JObject properties;

		protected JObject Properties
		{
			get
			{
				if(properties == null)
				{
					this.properties = String.IsNullOrEmpty(this.PropertiesJson) ? new JObject() : JObject.Parse(this.PropertiesJson);
				}
				return properties;
			}
		}

		public IEnumerable<Project> Projects
		{
			get { return this.ItemProjects.Select(x => x.Project); }
		}

		internal void CopyFrom(Item other)
		{
			this.Title = other.Title;
			this.Date = other.Date;
			this.DetailUrl = other.DetailUrl;
			this.Content = other.Content;
			this.PropertiesJson = other.PropertiesJson;
		}

	}
}
