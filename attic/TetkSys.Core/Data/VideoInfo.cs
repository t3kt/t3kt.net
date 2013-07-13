using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TetkSys.Core.Data
{
	public sealed class VideoInfo : MediaInfo
	{
		public string Title { get; set; }
		public string EmbedUrl { get; set; }
		public int Width { get; set; }
		public int Height { get; set; }
	}
}
