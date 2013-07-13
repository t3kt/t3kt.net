using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Xml.Linq;
using Tekt.Core.Entities;
using Tekt.Core.External;

namespace Tekt.Core.Data
{
	public class TektData
	{

		private static TektData instance;

		public static TektData Instance
		{
			get
			{
				return LazyInitializer.EnsureInitialized(ref instance,
					() => new TektData
					{
#if DEBUG
#else
						Cache = MemoryCache.Default,
#endif
						CacheDuration = TimeSpan.FromMinutes(5),
						MapPath = path => HttpContext.Current.Server.MapPath("~/Content/" + path)
					});
			}
		}

		private static readonly Regex projectCodeRegex = new Regex(@"^[a-zA-Z0-9\-]{2,100}$");

		public TimeSpan CacheDuration { get; set; }

		public ObjectCache Cache { get; set; }

		public Func<string, string> MapPath { get; set; }

		private string ResolvePath(string path)
		{
			if(String.IsNullOrWhiteSpace(path))
				return null;
			if(path.Contains("..") || path.StartsWith("/"))
				throw new ArgumentException();
			if(this.MapPath == null)
				return path;
			return this.MapPath(path);
		}

		public Project GetProject(string code)
		{
			if(String.IsNullOrWhiteSpace(code))
				return null;
			if(!projectCodeRegex.IsMatch(code))
				return null;
			var paths = this.GetProjectList();
			var entry = paths.FirstOrDefault(p => String.Equals(p.Id, code, StringComparison.OrdinalIgnoreCase));
			if(entry == null)
				return null;
			return this.GetProject(entry);
		}

		private Project GetProject(IndexEntry entry)
		{
			return this.GetItem("project-", entry, Project.ReadProject);
		}

		private T GetItem<T>(string prefix, IndexEntry entry, Func<XElement, T> read) where T : class
		{
			return this.GetCachedItem(prefix + entry.Id, () =>
				{
					var fullPath = this.ResolvePath(entry.Path);
					var root = XElement.Load(fullPath);
					return read(root);
				});
		}

		private T GetCachedItem<T>(string key, Func<T> load) where T : class
		{
			if(this.Cache == null)
				return load();
			var item = (T)this.Cache.Get(key);
			if(item != null)
				return item;
			item = load();
			return (T)this.Cache.AddOrGetExisting(key, item,
											  new CacheItemPolicy
												  {
													  AbsoluteExpiration = DateTimeOffset.UtcNow + this.CacheDuration
												  }) ?? item;
		}

		public IList<IndexEntry> GetProjectList()
		{
			return this.GetCachedItem("projectIndex", () =>
				{
					var filePath = this.ResolvePath("projects/index.xml");
					var root = XElement.Load(filePath);
					return IndexEntry.ReadEntries(root).ToList();
				});
		}

		public IEnumerable<Project> GetProjects()
		{
			return this.GetProjectList().Select(this.GetProject);
		}

		public IEnumerable<BlogEntry> GetBlogEntries()
		{
			return this.GetCachedItem("blogFeed", () =>
			{
				try
				{
					var root = XElement.Load(ConfigurationManager.AppSettings["BlogFeedURL"]);
					return BlogEntry.ReadEntries(root);
				}
				catch
				{
					return Enumerable.Empty<BlogEntry>();
				}
			});
		}

		public IEnumerable<BlogEntry> GetBlogEntries(string category)
		{
			return from entry in this.GetBlogEntries()
				   where entry.Categories != null && entry.Categories.Contains(category, StringComparer.OrdinalIgnoreCase)
				   select entry;
		}

		public IEnumerable<BlogEntry> GetProjectBlogEntries(Project project)
		{
			if(project.BlogCategories == null || project.BlogCategories.Count == 0)
				return Enumerable.Empty<BlogEntry>();
			return from entry in this.GetBlogEntries()
				   where entry.Categories != null && entry.Categories.Intersect(project.BlogCategories, StringComparer.OrdinalIgnoreCase).Any()
				   select entry;
		}


		internal static readonly XNamespace mediaNS = "http://search.yahoo.com/mrss/";
		internal static readonly XNamespace atomNS = XNamespace.Get("http://www.w3.org/2005/Atom");

		public IEnumerable<Image> GetProjectImages(Project project)
		{
			if(project.FlickrSetId == null)
				return Enumerable.Empty<Image>();
			return GetCachedItem("project-" + project.Id + "-images", () => TektFlickr.GetImages(project.FlickrSetId));
		}

		public IEnumerable<Video> GetProjectVideos(Project project)
		{
			if(project.VimeoAlbumId == null)
				return Enumerable.Empty<Video>();
			return GetCachedItem("project-" + project.Id + "-videos", () => TektVimeo.GetAlbumVideos(project.VimeoAlbumId));
		}

		public IEnumerable<Commit> GetProjectCommits(Project project)
		{
			if(project.GithubRepo == null)
				return Enumerable.Empty<Commit>();
			return GetCachedItem("project-" + project.Id + "-commits", () => TektGithub.GetRepoCommits(project.GithubRepo));
		}

		public IEnumerable<TektEntity> GetProjectItems(Project project)
		{
			return ((IEnumerable<TektEntity>)GetProjectImages(project))
				.Concat(GetProjectVideos(project))
				.Concat(GetProjectBlogEntries(project))
				.Concat(GetProjectCommits(project))
				.OrderByDescending(x => x.Posted).ThenBy(x => x.Title ?? String.Empty, StringComparer.OrdinalIgnoreCase);
		}
	}
}
