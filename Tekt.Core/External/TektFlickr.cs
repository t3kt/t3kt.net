using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System;
using FlickrNet;
using Tekt.Core.Entities;

namespace Tekt.Core.External
{
	internal static class TektFlickr
	{
		private static Flickr GetFlickr()
		{
			return new Flickr(TektConfig.FlickrApiKey) { InstanceCacheDisabled = true };
		}
		private static Image PhotoToImage(Photo photo)
		{
			return new Image
			{
				Id = photo.PhotoId,
				Height = photo.LargeHeight,
				Width = photo.LargeWidth,
				Path = photo.LargeUrl,
				Posted = photo.DateTaken,
				ThumbHeight = photo.SmallHeight,
				ThumbWidth = photo.SmallWidth,
				ThumbPath = photo.SmallUrl,
				Title = photo.Title,
				DetailUrl = photo.WebUrl
			};
		}
		public static IEnumerable<Image> GetImages(string photosetId)
		{
			try
			{
				var flickr = GetFlickr();
				var photos = flickr.PhotosetsGetPhotos(photosetId, PhotoSearchExtras.All, PrivacyFilter.PublicPhotos);
				return photos.Select(PhotoToImage);
			}
			catch(Exception)
			{
				return Enumerable.Empty<Image>();
			}
		}
	}
}
