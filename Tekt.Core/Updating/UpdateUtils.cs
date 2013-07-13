using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tekt.Core.Updating
{
	internal static class UpdateUtils
	{

		public static Task<TResult> EventBasedTask<TResult>(Action<Action<TResult>> method)
		{
			var completionSource = new TaskCompletionSource<TResult>();
			method(completionSource.SetResult);
			return completionSource.Task;
		}

		public static Task<TResult> EventBasedTask<T1, TResult>(Action<T1, Action<TResult>> method, T1 arg1)
		{
			var completionSource = new TaskCompletionSource<TResult>();
			method(arg1, completionSource.SetResult);
			return completionSource.Task;
		}

	}
}
