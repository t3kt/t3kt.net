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
	class VimeoUpdateSource : UpdateSource
	{

		protected override Task UpdateInternal()
		{
			throw new NotImplementedException();
		}

		private async Task<IEnumerable<Video>> GetVideosInternal(string url)
		{
			var xml = await new HttpClient().GetStringAsync(url);
			var root = XElement.Parse(xml);
			var parseTasks =
				from elem in root.Elements("video").AsParallel().AsUnordered()
				select ParseVideo(elem);
			return await Task.WhenAll(parseTasks);
		}

		private async Task<Video> ParseVideo(XElement elem)
		{
			return new Video
			{
				Key = (string)elem.Element("guid"),
				Title = (string)elem.Element("title"),
				Date = (DateTime?)elem.Element("upload_date"),
				Width = (int?)elem.Element("width"),
				Height = (int?)elem.Element("height"),
				//ThumbPath = (string)elem.Element("thumbnail_medium"),
				Content = await GetEmbedHtml((string)elem.Element("url")),
				DetailUrl = (string)elem.Element("url")
			};
		}

		private async Task<string> GetEmbedHtml(string videoUrl)
		{
			var url = String.Format(TektConfig.VimeoOEmbedUrlFormat, Uri.EscapeUriString(videoUrl));
			var xml = await new HttpClient().GetStringAsync(url);
			var elem = XElement.Parse(xml);
			return (string) elem.Element("html");
		}
	}
}
