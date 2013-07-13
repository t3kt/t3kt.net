using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TetkSys.Core.Data
{
	public sealed class ImageInfo : MediaInfo
	{
		public string Path { get; set; }
		public string AltText { get; set; }
	}
}
