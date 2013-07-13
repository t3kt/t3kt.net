using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Tekt.Core.Entities;

namespace Tekt.Core.External
{
	internal static class TektGithub
	{
		internal static Commit ReadCommit(JObject obj)
		{
			var commitObj = (JObject)obj["commit"] ?? new JObject();
			var committerObj = (JObject)commitObj["committer"] ?? new JObject();
			return new Commit
				{
					Id = (string)obj["sha"],
					DetailUrl = (string)obj["html_url"],
					Posted = (DateTime?)committerObj["date"],
					Title = (string)commitObj["message"]
				};
		}

		public static IEnumerable<Commit> GetRepoCommits(string repoName)
		{
			try
			{
				var url = String.Format(TektConfig.GithubUrlFormat, repoName);
				var objs = GetCommitsInternal(url);
				return objs.Select(ReadCommit);
			}
			catch(Exception)
			{
				return Enumerable.Empty<Commit>();
			}
		}

		private static IEnumerable<JObject> GetCommitsInternal(string url)
		{
			var req = WebRequest.CreateHttp(url);
			req.UserAgent = TektConfig.GithubUserAgent;
			using(var resp = req.GetResponse())
			using(var stream = resp.GetResponseStream())
			using(var streamReader = new StreamReader(stream))
			using(var jsonReader = new JsonTextReader(streamReader))
			{
				var array = JArray.Load(jsonReader);
				return array.OfType<JObject>().ToArray();
			}
		}
	}
}
