using System;
using System.Threading;
using System.Threading.Tasks;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020008F3 RID: 2291
	internal static class AsyncTaskCache
	{
		// Token: 0x06005E33 RID: 24115 RVA: 0x0014AD84 File Offset: 0x00148F84
		private static Task<int>[] CreateInt32Tasks()
		{
			Task<int>[] array = new Task<int>[10];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = AsyncTaskCache.CreateCacheableTask<int>(i + -1);
			}
			return array;
		}

		// Token: 0x06005E34 RID: 24116 RVA: 0x0014ADB4 File Offset: 0x00148FB4
		internal static Task<TResult> CreateCacheableTask<TResult>(TResult result)
		{
			return new Task<TResult>(false, result, (TaskCreationOptions)16384, default(CancellationToken));
		}

		// Token: 0x06005E35 RID: 24117 RVA: 0x0014ADD6 File Offset: 0x00148FD6
		// Note: this type is marked as 'beforefieldinit'.
		static AsyncTaskCache()
		{
		}

		// Token: 0x04002A56 RID: 10838
		internal static readonly Task<bool> TrueTask = AsyncTaskCache.CreateCacheableTask<bool>(true);

		// Token: 0x04002A57 RID: 10839
		internal static readonly Task<bool> FalseTask = AsyncTaskCache.CreateCacheableTask<bool>(false);

		// Token: 0x04002A58 RID: 10840
		internal static readonly Task<int>[] Int32Tasks = AsyncTaskCache.CreateInt32Tasks();

		// Token: 0x04002A59 RID: 10841
		internal const int INCLUSIVE_INT32_MIN = -1;

		// Token: 0x04002A5A RID: 10842
		internal const int EXCLUSIVE_INT32_MAX = 9;
	}
}
