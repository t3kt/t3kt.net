using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlickrNet;
using Tekt.Core.Data;
using Tekt.Core.Data.Entities;

namespace Tekt.Core.Updating
{
	class FlickrUpdateSource : UpdateSource
	{
		private readonly Flickr flickrApi = new Flickr(TektConfig.FlickrApiKey) { InstanceCacheDisabled = true };

		protected override async Task UpdateInternal()
		{
			var allPhotosetIds = Projects.SelectMany(p => p.FlickrSetIds).Distinct();
			foreach (var photosetId in allPhotosetIds)
			{
				var projects = Projects.Where(p => p.FlickrSetIds.Contains(photosetId)).ToList();
				var images = await GetPhotosetImages(photosetId);
				foreach (var image in images)
				{
					UpdateItem(image, projects);
				}
			}
			throw new NotImplementedException();
		}

		private void UpdateItem(Image remoteImage, IEnumerable<Project> projects)
		{
			var storedImage = DataContext.GetItemByKey<Image>(remoteImage.Key);
			if (storedImage == null)
			{
				DataContext.Items.InsertOnSubmit(remoteImage);
				DataContext.ItemProjects.InsertAllOnSubmit(
					from project in projects
					select new ItemProject {Item = remoteImage, ProjectId = project.Id});
			}
			else
			{
				//TODO: ???
			}
		}

		private static Image PhotoToImage(Photo photo)
		{
			return new Image
			{
				Key = photo.PhotoId,
				Height = photo.LargeHeight,
				Width = photo.LargeWidth,
				ContentUrl = photo.LargeUrl,
				Date = photo.DateTaken,
				ThumbHeight = photo.SmallHeight,
				ThumbWidth = photo.SmallWidth,
				ThumbUrl = photo.SmallUrl,
				Title = photo.Title,
				DetailUrl = photo.WebUrl,
				Updated = photo.LastUpdated
			};
		}

		private async Task<IEnumerable<Image>> GetPhotosetImages(string photosetId)
		{
			var photosResult = await UpdateUtils.EventBasedTask<string, FlickrResult<PhotosetPhotoCollection>>(flickrApi.PhotosetsGetPhotosAsync, photosetId);
			return from photo in photosResult.Result
				   select PhotoToImage(photo);
		}
	}
}
