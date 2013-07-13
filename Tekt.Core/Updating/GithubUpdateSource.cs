using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Tekt.Core.Data;
using Tekt.Core.Data.Entities;

namespace Tekt.Core.Updating
{
	class GithubUpdateSource : UpdateSource
	{

		protected override Task UpdateInternal()
		{
			var allRepoIds = Projects.SelectMany(p => p.GithubRepos).Distinct();
			var repoTasks = allRepoIds.Select(this.UpdateRepoCommits);
			return Task.WhenAll(repoTasks);
		}

		private async Task UpdateRepoCommits(string repoId)
		{
			var projects = Projects.Where(p => p.GithubRepos.Contains(repoId)).ToArray();
			var url = String.Format(TektConfig.GithubUrlFormat, repoId);
			var commits = await GetCommits(url);
			foreach(var commit in commits)
			{
				var storedCommit = DataContext.GetItemByKey<Commit>(commit.Key);
				if(storedCommit != null)
					continue;
				DataContext.Items.InsertOnSubmit(commit);
				var c = commit;
				DataContext.ItemProjects.InsertAllOnSubmit(
					from project in projects
					select new ItemProject { Item = c, ProjectId = project.Id });
			}
		}

		private async Task<IEnumerable<Commit>> GetCommits(string url)
		{
			using(var req = new HttpClient())
			{
				req.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue(TektConfig.GithubUserAgent, "1"));
				var json = await req.GetStringAsync(url);
				var array = JArray.Parse(json);
				return array.OfType<JObject>().Select(ReadCommit).ToArray();
			}
		}
		private static Commit ReadCommit(JObject obj)
		{
			var commitObj = (JObject)obj["commit"] ?? new JObject();
			var committerObj = (JObject)commitObj["committer"] ?? new JObject();
			return new Commit
			{
				Key = (string)obj["sha"],
				DetailUrl = (string)obj["html_url"],
				Date = (DateTime?)committerObj["date"],
				Title = (string)commitObj["message"],
				Updated = (DateTime)committerObj["date"]
			};
		}
	}
}
