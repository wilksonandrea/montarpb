using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace Plugin.Core.Colorful
{
	// Token: 0x02000114 RID: 276
	public class TaskQueue
	{
		// Token: 0x060009F9 RID: 2553 RVA: 0x00007EE0 File Offset: 0x000060E0
		public TaskQueue()
		{
			this.semaphoreSlim_0 = new SemaphoreSlim(1);
		}

		// Token: 0x060009FA RID: 2554 RVA: 0x00022200 File Offset: 0x00020400
		public async Task<T> Enqueue<T>(Func<Task<T>> taskGenerator)
		{
			TaskAwaiter taskAwaiter = this.semaphoreSlim_0.WaitAsync().GetAwaiter();
			if (!taskAwaiter.IsCompleted)
			{
				goto IL_D3;
			}
			IL_50:
			taskAwaiter.GetResult();
			try
			{
				return await taskGenerator();
			}
			finally
			{
				this.semaphoreSlim_0.Release();
			}
			IL_D3:
			await taskAwaiter;
			TaskAwaiter taskAwaiter2;
			taskAwaiter = taskAwaiter2;
			taskAwaiter2 = default(TaskAwaiter);
			goto IL_50;
		}

		// Token: 0x060009FB RID: 2555 RVA: 0x0002224C File Offset: 0x0002044C
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

		// Token: 0x04000729 RID: 1833
		private readonly SemaphoreSlim semaphoreSlim_0;

		// Token: 0x02000115 RID: 277
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct Struct4<T> : IAsyncStateMachine
		{
			// Token: 0x060009FC RID: 2556 RVA: 0x00022298 File Offset: 0x00020498
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				int num = num2;
				TaskQueue taskQueue = this;
				T result;
				try
				{
					TaskAwaiter taskAwaiter3;
					if (num != 0)
					{
						if (num == 1)
						{
							goto IL_57;
						}
						taskAwaiter3 = taskQueue.semaphoreSlim_0.WaitAsync().GetAwaiter();
						if (!taskAwaiter3.IsCompleted)
						{
							goto IL_D3;
						}
					}
					else
					{
						taskAwaiter3 = taskAwaiter2;
						taskAwaiter2 = default(TaskAwaiter);
						int num3 = -1;
						num = -1;
						num2 = num3;
					}
					taskAwaiter3.GetResult();
					IL_57:
					try
					{
						TaskAwaiter<T> taskAwaiter4;
						if (num != 1)
						{
							taskAwaiter4 = taskGenerator().GetAwaiter();
							if (!taskAwaiter4.IsCompleted)
							{
								int num4 = 1;
								num = 1;
								num2 = num4;
								TaskAwaiter<T> taskAwaiter5 = taskAwaiter4;
								this.asyncTaskMethodBuilder_0.AwaitUnsafeOnCompleted<TaskAwaiter<T>, TaskQueue.Struct4<T>>(ref taskAwaiter4, ref this);
								return;
							}
						}
						else
						{
							TaskAwaiter<T> taskAwaiter5;
							taskAwaiter4 = taskAwaiter5;
							taskAwaiter5 = default(TaskAwaiter<T>);
							int num5 = -1;
							num = -1;
							num2 = num5;
						}
						result = taskAwaiter4.GetResult();
						goto IL_10C;
					}
					finally
					{
						if (num < 0)
						{
							taskQueue.semaphoreSlim_0.Release();
						}
					}
					IL_D3:
					int num6 = 0;
					num = 0;
					num2 = num6;
					taskAwaiter2 = taskAwaiter3;
					this.asyncTaskMethodBuilder_0.AwaitUnsafeOnCompleted<TaskAwaiter, TaskQueue.Struct4<T>>(ref taskAwaiter3, ref this);
					return;
				}
				catch (Exception ex)
				{
					num2 = -2;
					this.asyncTaskMethodBuilder_0.SetException(ex);
					return;
				}
				IL_10C:
				num2 = -2;
				this.asyncTaskMethodBuilder_0.SetResult(result);
			}

			// Token: 0x060009FD RID: 2557 RVA: 0x00007EF4 File Offset: 0x000060F4
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.asyncTaskMethodBuilder_0.SetStateMachine(stateMachine);
			}

			// Token: 0x0400072A RID: 1834
			public int int_0;

			// Token: 0x0400072B RID: 1835
			public AsyncTaskMethodBuilder<T> asyncTaskMethodBuilder_0;

			// Token: 0x0400072C RID: 1836
			public TaskQueue taskQueue_0;

			// Token: 0x0400072D RID: 1837
			public Func<Task<T>> func_0;

			// Token: 0x0400072E RID: 1838
			private TaskAwaiter taskAwaiter_0;

			// Token: 0x0400072F RID: 1839
			private TaskAwaiter<T> taskAwaiter_1;
		}

		// Token: 0x02000116 RID: 278
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct Struct5 : IAsyncStateMachine
		{
			// Token: 0x060009FE RID: 2558 RVA: 0x000223E4 File Offset: 0x000205E4
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				int num = num2;
				TaskQueue taskQueue = this;
				try
				{
					TaskAwaiter taskAwaiter;
					TaskAwaiter taskAwaiter2;
					if (num != 0)
					{
						if (num == 1)
						{
							goto IL_75;
						}
						taskAwaiter = taskQueue.semaphoreSlim_0.WaitAsync().GetAwaiter();
						if (!taskAwaiter.IsCompleted)
						{
							int num3 = 0;
							num = 0;
							num2 = num3;
							taskAwaiter2 = taskAwaiter;
							this.asyncTaskMethodBuilder_0.AwaitUnsafeOnCompleted<TaskAwaiter, TaskQueue.Struct5>(ref taskAwaiter, ref this);
							return;
						}
					}
					else
					{
						taskAwaiter = taskAwaiter2;
						taskAwaiter2 = default(TaskAwaiter);
						int num4 = -1;
						num = -1;
						num2 = num4;
					}
					taskAwaiter.GetResult();
					IL_75:
					try
					{
						if (num != 1)
						{
							taskAwaiter = taskGenerator().GetAwaiter();
							if (!taskAwaiter.IsCompleted)
							{
								int num5 = 1;
								num = 1;
								num2 = num5;
								taskAwaiter2 = taskAwaiter;
								this.asyncTaskMethodBuilder_0.AwaitUnsafeOnCompleted<TaskAwaiter, TaskQueue.Struct5>(ref taskAwaiter, ref this);
								return;
							}
						}
						else
						{
							taskAwaiter = taskAwaiter2;
							taskAwaiter2 = default(TaskAwaiter);
							int num6 = -1;
							num = -1;
							num2 = num6;
						}
						taskAwaiter.GetResult();
					}
					finally
					{
						if (num < 0)
						{
							taskQueue.semaphoreSlim_0.Release();
						}
					}
				}
				catch (Exception ex)
				{
					num2 = -2;
					this.asyncTaskMethodBuilder_0.SetException(ex);
					return;
				}
				num2 = -2;
				this.asyncTaskMethodBuilder_0.SetResult();
			}

			// Token: 0x060009FF RID: 2559 RVA: 0x00007F02 File Offset: 0x00006102
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.asyncTaskMethodBuilder_0.SetStateMachine(stateMachine);
			}

			// Token: 0x04000730 RID: 1840
			public int int_0;

			// Token: 0x04000731 RID: 1841
			public AsyncTaskMethodBuilder asyncTaskMethodBuilder_0;

			// Token: 0x04000732 RID: 1842
			public TaskQueue taskQueue_0;

			// Token: 0x04000733 RID: 1843
			public Func<Task> func_0;

			// Token: 0x04000734 RID: 1844
			private TaskAwaiter taskAwaiter_0;
		}
	}
}
