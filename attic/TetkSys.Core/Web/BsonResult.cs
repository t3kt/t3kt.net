using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;

namespace TetkSys.Core.Web
{
	public class BsonResult<T> : ActionResult
	{
		private readonly T data;
		public BsonResult(T data)
		{
			this.data = data;
		}

		public override void ExecuteResult(ControllerContext context)
		{
			var response = context.HttpContext.Response;
			response.ContentType = "application/json";
			var writer = BsonWriter.Create(response.Output, new JsonWriterSettings { CloseOutput = true });
			BsonSerializer.Serialize(writer, data);
		}
	}
}
