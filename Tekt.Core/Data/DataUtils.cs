using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Tekt.Core.Data
{
	internal static class DataUtils
	{
		public static JObject SafeParseJObject(string json)
		{
			if(String.IsNullOrWhiteSpace(json))
				return new JObject();
			try
			{
				return JObject.Parse(json);
			}
			catch(JsonException)
			{
				return new JObject();
			}
		}

		public static JObject ObjOrEmpty(this JObject obj, string key)
		{
			if (obj == null)
				return new JObject();
			return (JObject) obj[key] ?? new JObject();
		}
		
		public static JArray ArrOrEmpty(this JObject obj, string key)
		{
			if (obj == null)
				return new JArray();
			return (JArray) obj[key] ?? new JArray();
		}

		public static IEnumerable<string> AsStringsOrEmpty(this JArray arr)
		{
			if (arr == null)
				return Enumerable.Empty<string>();
			return arr.Select(x => (string) x);
		}
	}
}
