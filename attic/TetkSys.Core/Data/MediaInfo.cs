using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TetkSys.Core.Data
{
	public class MediaInfo : EntityBase
	{
		[BsonId]
		public ObjectId Id { get; set; }

		public string ThumbPath { get; set; }
		public DateTime Posted { get; set; }
		public string ProjectCode { get; set; }
	}
}