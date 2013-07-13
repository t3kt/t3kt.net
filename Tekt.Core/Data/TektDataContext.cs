using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekt.Core.Data.Entities;

namespace Tekt.Core.Data
{
	partial class TektDataContext
	{

		public static TektDataContext GetInstance()
		{
			return new TektDataContext();
		}


		public T GetItemByKey<T>(string key) where T : Item
		{
			return this.Items.Where(i => i.Key == key).OfType<T>().SingleOrDefault();
		}

	}
}
