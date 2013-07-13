using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;

namespace TetkSys.Core.Data
{
	public sealed class ProjectInfo : EntityBase
	{
		//public int ID { get; set; }
		[BsonId]
		public string Code { get; set; }
		public string Title { get; set; }
		public int Order { get; set; }
		public string Description { get; set; }
		public string Summary { get; set; }
		public string ImagePath { get; set; }
		public string ThumbPath { get; set; }

		[BsonIgnore]
		public ImageInfo ProjectImage
		{
			get { return new ImageInfo { AltText = Title, Path = ImagePath, ThumbPath = ThumbPath, ProjectCode = Code }; }
		}

		public IEnumerable<ImageInfo> GetImages()
		{
			return TetkDB.Instance.GetImagesByProject(this.Code);
		}
		public IEnumerable<VideoInfo> GetVideos()
		{
			return TetkDB.Instance.GetVideosByProject(this.Code);
		}
	}
}
