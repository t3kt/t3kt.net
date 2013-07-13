using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetkSys.Core.Data
{
	public class TetkDbContext : DbContext
	{
		public DbSet<ProjectInfo> Projects { get; set; }

		public DbSet<ImageInfo> Images { get; set; }

		public DbSet<VideoInfo> Videos { get; set; }

		public TetkDbContext(string connName)
			: base(connName)
		{
		}
	}
}
