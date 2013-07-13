using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tekt.Core
{
	internal static class TektConfig
	{
		private static NameValueCollection appSettings;

		private static string Setting(string key)
		{
			if (appSettings == null)
				appSettings = ConfigurationManager.AppSettings;
			return appSettings[key];
		}
		public static string DbConn
		{
			get { return Setting("TektDbConn"); }
		}

		public static string FlickrApiKey
		{
			get { return Setting("FlickrApiKey"); }
		}

		public static string GithubUrlFormat
		{
			get { return Setting("GithubURLFormat"); }
		}

		public static string GithubUserAgent
		{
			get { return Setting("GithubUserAgent"); }
		}

		public static string VimeoAlbumUrlFormat
		{
			get { return Setting("VimeoAlbumUrlFormat"); }
		}

		public static string VimeoOEmbedUrlFormat
		{
			get { return Setting("VimeoOEmbedUrlFormat"); }
		}

		public static string BlogFeedURL
		{
			get { return Setting("BlogFeedURL"); }
		}
	}
}
