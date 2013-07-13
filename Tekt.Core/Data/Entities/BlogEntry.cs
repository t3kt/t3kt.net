using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Tekt.Core.Data.Entities
{
	partial class BlogEntry
	{
		public IEnumerable<string> Categories
		{
			get { return Properties.ArrOrEmpty("categories").AsStringsOrEmpty(); }
			set { Properties["categories"] = value == null ? null : new JArray(value); }
		}
	}
}
