using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System;
using System.Xml.Linq;
using Tekt.Core.Entities;

namespace Tekt.Core.External
{
	internal static class TektVimeo
	{
		internal static IEnumerable<Video> GetAlbumVideos(string albumId)
		{
			try
			{
				var url = String.Format(TektConfig.VimeoAlbumUrlFormat, albumId);
				var root = XElement.Load(url);
				return root.Elements("video").Select(ReadVideo);
			}
			catch(Exception)
			{
				return Enumerable.Empty<Video>();
			}
		}

		private static Video ReadVideo(XElement elem)
		{
			return new Video
			{
				Id = (string)elem.Element("guid"),
				Title = (string)elem.Element("title"),
				Posted = (DateTime?)elem.Element("upload_date"),
				EmbedWidth = (int?)elem.Element("width"),
				EmbedHeight = (int?)elem.Element("height"),
				ThumbPath = (string)elem.Element("thumbnail_medium"),
				Embed = GetEmbedHtml((string)elem.Element("url")),
				DetailUrl = (string)elem.Element("url")
			};
		}

		private static string GetEmbedHtml(string videoUrl)
		{
			try
			{
				var url = String.Format(TektConfig.VimeoOEmbedUrlFormat, Uri.EscapeUriString(videoUrl));
				var elem = XElement.Load(url);
				return (string)elem.Element("html");
			}
			catch(Exception)
			{
				return null;
			}
		}
	}
}
