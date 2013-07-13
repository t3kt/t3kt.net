using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Web;
using System.Web.Mvc;
using Tekt.Core.Data;

namespace Tekt.Web.Controllers
{
	public class CacheController : Controller
	{

		public ActionResult ClearCache(string key)
		{
			if(key != "srsly-do-it")
				return Content("no! go away!");
			var cache = TektData.Instance.Cache;
			if(cache == null)
				return Content("we aren't caching...");
			var memcache = cache as MemoryCache;
			if(memcache != null)
			{
				memcache.Trim(100);
			}
			else
			{
				foreach(var entry in cache)
					cache.Remove(entry.Key);
			}
			return Content("ok!");
		}

	}
}
