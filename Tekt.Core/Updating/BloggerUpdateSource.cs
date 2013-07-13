using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Tekt.Core.Data;
using Tekt.Core.Data.Entities;

namespace Tekt.Core.Updating
{
	internal class BloggerUpdateSource : UpdateSource
	{

		protected override async Task UpdateInternal()
		{
			var remoteEntries = await this.GetBlogEntries();
			foreach(var remoteEntry in remoteEntries)
			{
				UpdateItem(remoteEntry);
			}
			DataContext.SubmitChanges();
		}

		private void UpdateItem(BlogEntry remoteEntry)
		{
			var storedEntry = DataContext.GetItemByKey<BlogEntry>(remoteEntry.Key);
			if(storedEntry == null)
			{
				DataContext.Items.InsertOnSubmit(remoteEntry);
				DataContext.ItemProjects.InsertAllOnSubmit(
					from projectId in GetItemProjectIds(remoteEntry)
					select new ItemProject { Item = remoteEntry, ProjectId = projectId });
			}
			else if(storedEntry.Updated < remoteEntry.Updated)
			{
				storedEntry.CopyFrom(remoteEntry);
				var remoteProjectIds = GetItemProjectIds(remoteEntry).ToList();
				DataContext.ItemProjects.DeleteAllOnSubmit(storedEntry.ItemProjects.Where(i => !remoteProjectIds.Contains(i.ProjectId)));
				DataContext.ItemProjects.InsertAllOnSubmit(
					from projectId in remoteProjectIds
					where storedEntry.ItemProjects.All(i => i.ProjectId != projectId)
					select new ItemProject { Item = storedEntry, ProjectId = projectId });
			}
		}

		private IEnumerable<int> GetItemProjectIds(BlogEntry remoteEntry)
		{
			return Projects.Where(p => p.BlogCategories.Intersect(remoteEntry.Categories).Any()).Select(p => p.Id);
		}

		private async Task<IEnumerable<BlogEntry>> GetBlogEntries()
		{
			var xml = await new HttpClient().GetStringAsync(TektConfig.BlogFeedURL);
			var root = XElement.Parse(xml);
			return root.Elements(TektData.atomNS + "entry").Select(ReadEntry);
		}

		private BlogEntry ReadEntry(XElement elem)
		{
			return new BlogEntry
			{
				Key = (string)elem.Element(TektData.atomNS + "id"),
				Date = (DateTime)elem.Element(TektData.atomNS + "published"),
				Categories = elem.Elements(TektData.atomNS + "category").Select(e => (string)e.Attribute("term")).ToList(),
				Title = (string)elem.Element(TektData.atomNS + "title"),
				Content = (string)elem.Element(TektData.atomNS + "content"),
				Updated = (DateTime)elem.Element(TektData.atomNS + "updated")
			};
		}

	}
}
