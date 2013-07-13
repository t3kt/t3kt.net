using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Tekt.Core.Data.Entities
{
	partial class Project
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

		internal IEnumerable<string> BlogCategories
		{
			get { return Properties.ArrOrEmpty("blogCategories").AsStringsOrEmpty(); }
		}

		internal IEnumerable<string> FlickrSetIds
		{
			get { return Properties.ArrOrEmpty("flickrSetIds").AsStringsOrEmpty(); }
		}

		internal IEnumerable<string> VimeoAlbumIds
		{
			get { return Properties.ArrOrEmpty("vimeoAlbumIds").AsStringsOrEmpty(); }
		}

		internal IEnumerable<string> GithubRepos
		{
			get { return Properties.ArrOrEmpty("githubRepos").AsStringsOrEmpty(); }
		}

		public IEnumerable<Item> Items
		{
			get { return this.ProjectItems.Select(x => x.Item); }
		}

	}
}
