using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace System.Threading.Tasks
{
	// Token: 0x02000589 RID: 1417
	internal static class TaskToApm
	{
		// Token: 0x060042A6 RID: 17062 RVA: 0x000F8284 File Offset: 0x000F6484
		public static IAsyncResult Begin(Task task, AsyncCallback callback, object state)
		{
			IAsyncResult asyncResult;
			if (task.IsCompleted)
			{
				asyncResult = new TaskToApm.TaskWrapperAsyncResult(task, state, true);
				if (callback != null)
				{
					callback(asyncResult);
				}
			}
			else
			{
				IAsyncResult asyncResult3;
				if (task.AsyncState != state)
				{
					IAsyncResult asyncResult2 = new TaskToApm.TaskWrapperAsyncResult(task, state, false);
					asyncResult3 = asyncResult2;
				}
				else
				{
					asyncResult3 = task;
				}
				asyncResult = asyncResult3;
				if (callback != null)
				{
					TaskToApm.InvokeCallbackWhenTaskCompletes(task, callback, asyncResult);
				}
			}
			return asyncResult;
		}

		// Token: 0x060042A7 RID: 17063 RVA: 0x000F82D4 File Offset: 0x000F64D4
		public static void End(IAsyncResult asyncResult)
		{
			TaskToApm.TaskWrapperAsyncResult taskWrapperAsyncResult = asyncResult as TaskToApm.TaskWrapperAsyncResult;
			Task task;
			if (taskWrapperAsyncResult != null)
			{
				task = taskWrapperAsyncResult.Task;
			}
			else
			{
				task = asyncResult as Task;
			}
			if (task == null)
			{
				__Error.WrongAsyncResult();
			}
			task.GetAwaiter().GetResult();
		}

		// Token: 0x060042A8 RID: 17064 RVA: 0x000F8314 File Offset: 0x000F6514
		public static TResult End<TResult>(IAsyncResult asyncResult)
		{
			TaskToApm.TaskWrapperAsyncResult taskWrapperAsyncResult = asyncResult as TaskToApm.TaskWrapperAsyncResult;
			Task<TResult> task;
			if (taskWrapperAsyncResult != null)
			{
				task = taskWrapperAsyncResult.Task as Task<TResult>;
			}
			else
			{
				task = asyncResult as Task<TResult>;
			}
			if (task == null)
			{
				__Error.WrongAsyncResult();
			}
			return task.GetAwaiter().GetResult();
		}

		// Token: 0x060042A9 RID: 17065 RVA: 0x000F8358 File Offset: 0x000F6558
		private static void InvokeCallbackWhenTaskCompletes(Task antecedent, AsyncCallback callback, IAsyncResult asyncResult)
		{
			antecedent.ConfigureAwait(false).GetAwaiter().OnCompleted(delegate
			{
				callback(asyncResult);
			});
		}

		// Token: 0x02000C36 RID: 3126
		private sealed class TaskWrapperAsyncResult : IAsyncResult
		{
			// Token: 0x0600703A RID: 28730 RVA: 0x00182ADE File Offset: 0x00180CDE
			internal TaskWrapperAsyncResult(Task task, object state, bool completedSynchronously)
			{
				this.Task = task;
				this.m_state = state;
				this.m_completedSynchronously = completedSynchronously;
			}

			// Token: 0x1700133A RID: 4922
			// (get) Token: 0x0600703B RID: 28731 RVA: 0x00182AFB File Offset: 0x00180CFB
			object IAsyncResult.AsyncState
			{
				get
				{
					return this.m_state;
				}
			}

			// Token: 0x1700133B RID: 4923
			// (get) Token: 0x0600703C RID: 28732 RVA: 0x00182B03 File Offset: 0x00180D03
			bool IAsyncResult.CompletedSynchronously
			{
				get
				{
					return this.m_completedSynchronously;
				}
			}

			// Token: 0x1700133C RID: 4924
			// (get) Token: 0x0600703D RID: 28733 RVA: 0x00182B0B File Offset: 0x00180D0B
			bool IAsyncResult.IsCompleted
			{
				get
				{
					return this.Task.IsCompleted;
				}
			}

			// Token: 0x1700133D RID: 4925
			// (get) Token: 0x0600703E RID: 28734 RVA: 0x00182B18 File Offset: 0x00180D18
			WaitHandle IAsyncResult.AsyncWaitHandle
			{
				get
				{
					return ((IAsyncResult)this.Task).AsyncWaitHandle;
				}
			}

			// Token: 0x04003729 RID: 14121
			internal readonly Task Task;

			// Token: 0x0400372A RID: 14122
			private readonly object m_state;

			// Token: 0x0400372B RID: 14123
			private readonly bool m_completedSynchronously;
		}

		// Token: 0x02000C37 RID: 3127
		[CompilerGenerated]
		private sealed class <>c__DisplayClass3_0
		{
			// Token: 0x0600703F RID: 28735 RVA: 0x00182B25 File Offset: 0x00180D25
			public <>c__DisplayClass3_0()
			{
			}

			// Token: 0x06007040 RID: 28736 RVA: 0x00182B2D File Offset: 0x00180D2D
			internal void <InvokeCallbackWhenTaskCompletes>b__0()
			{
				this.callback(this.asyncResult);
			}

			// Token: 0x0400372C RID: 14124
			public AsyncCallback callback;

			// Token: 0x0400372D RID: 14125
			public IAsyncResult asyncResult;
		}
	}
}
