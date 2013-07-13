using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tekt.Core.Updating
{
	public class Updater
	{

		private readonly UpdateSource[] updateSources = new UpdateSource[]
			{
				new FlickrUpdateSource(),
				new VimeoUpdateSource(),
				new GithubUpdateSource(),
				new BloggerUpdateSource(),
			};

		public void Update()
		{
			var tasks = updateSources.Select(u => u.Update()).ToArray();
			Task.WaitAll(tasks);
		}

	}
}
