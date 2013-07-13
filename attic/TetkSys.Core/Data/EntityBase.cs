using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TetkSys.Core.Data
{
	public abstract class EntityBase
	{
		[BsonExtraElements]
		public BsonDocument ExtraElements { get; set; }
	}
}
