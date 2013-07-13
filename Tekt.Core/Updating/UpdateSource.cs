using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekt.Core.Data;
using Tekt.Core.Data.Entities;

namespace Tekt.Core.Updating
{
	internal abstract class UpdateSource
	{

		protected TektDataContext DataContext { get; private set; }

		protected IList<Project> Projects { get; private set; }

		public Task Update()
		{
			this.DataContext = TektDataContext.GetInstance();
			this.Projects = DataContext.Projects.ToList();
			return UpdateInternal();
		}

		protected abstract Task UpdateInternal();

	}
}
