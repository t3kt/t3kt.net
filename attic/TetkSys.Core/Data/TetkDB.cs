using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace TetkSys.Core.Data
{
	public class TetkDB
	{

		private static TetkDB instance;

		public static TetkDB Instance
		{
			get
			{
				if(instance == null)
				{
					var appSettings = ConfigurationManager.AppSettings;
					var settings = MongoClientSettings.FromConnectionStringBuilder(
						new MongoConnectionStringBuilder
							{
								Server = new MongoServerAddress(appSettings["tetksysdb_host"], Int32.Parse(appSettings["tetksysdb_port"])),
								Username = appSettings["tetksysdb_user"],
								Password = appSettings["tetksysdb_pass"],
								AuthenticationSource = "tetksys"
							});
					var settings2 = new MongoClient(appSettings["tetksysdburi"]).Settings;
					instance = new TetkDB(new MongoClient(settings));
				}
				return instance;
			}
		}

		private readonly MongoDatabase database;
		public TetkDB(string uri)
			: this(new MongoClient(uri))
		{
		}
		public TetkDB(MongoClient client)
		{
			this.database = client.GetServer().GetDatabase(ConfigurationManager.AppSettings["tetksysdbname"]);
		}
		public MongoDatabase Database { get { return database; } }

		public MongoCollection<ProjectInfo> Projects
		{
			get { return this.Database.GetCollection<ProjectInfo>("projects"); }
		}

		public MongoCollection<VideoInfo> Videos
		{
			get { return this.Database.GetCollection<VideoInfo>("videos"); }
		}
		public MongoCollection<ImageInfo> Images
		{
			get { return this.Database.GetCollection<ImageInfo>("images"); }
		}

		public IEnumerable<ImageInfo> GetImagesByProject(string projectCode)
		{
			return
				this.Images.Find(Query<ImageInfo>.EQ(i => i.ProjectCode, projectCode))
					.SetSortOrder(SortBy<ImageInfo>.Descending(i => i.Posted));
		}

		public IEnumerable<VideoInfo> GetVideosByProject(string projectCode)
		{
			return
				this.Videos.Find(Query<VideoInfo>.EQ(i => i.ProjectCode, projectCode))
					.SetSortOrder(SortBy<VideoInfo>.Descending(i => i.Posted));
		}

		public ProjectInfo GetProject(string projectCode)
		{
			return this.Projects.FindOneById(new BsonString(projectCode));
		}

		public IEnumerable<ProjectInfo> GetProjects()
		{
			return this.Projects.FindAll().SetSortOrder(SortBy<ProjectInfo>.Descending(p => p.Order));
		}
	}
}
