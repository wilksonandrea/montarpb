using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Plugin.Core.Colorful
{
	public class TaskQueue
	{
		private readonly SemaphoreSlim semaphoreSlim_0;

		public TaskQueue()
		{
			this.semaphoreSlim_0 = new SemaphoreSlim(1);
		}

		public async Task<T> Enqueue<T>(Func<Task<T>> taskGenerator)
		{
			TaskQueue.Struct4<T> variable = new TaskQueue.Struct4<T>();
			variable.asyncTaskMethodBuilder_0 = AsyncTaskMethodBuilder<T>.Create();
			variable.taskQueue_0 = this;
			variable.func_0 = taskGenerator;
			variable.int_0 = -1;
			variable.asyncTaskMethodBuilder_0.Start<TaskQueue.Struct4<T>>(ref variable);
			return variable.asyncTaskMethodBuilder_0.Task;
		}

		public async Task Enqueue(Func<Task> taskGenerator)
		{
			await this.semaphoreSlim_0.WaitAsync();
			try
			{
				await taskGenerator();
			}
			finally
			{
				this.semaphoreSlim_0.Release();
			}
		}
	}
}