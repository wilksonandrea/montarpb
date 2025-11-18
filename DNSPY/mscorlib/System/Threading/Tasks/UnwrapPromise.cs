using System;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Security;

namespace System.Threading.Tasks
{
	// Token: 0x02000569 RID: 1385
	internal sealed class UnwrapPromise<TResult> : Task<TResult>, ITaskCompletionAction
	{
		// Token: 0x06004162 RID: 16738 RVA: 0x000F3D88 File Offset: 0x000F1F88
		public UnwrapPromise(Task outerTask, bool lookForOce)
			: base(null, outerTask.CreationOptions & TaskCreationOptions.AttachedToParent)
		{
			this._lookForOce = lookForOce;
			this._state = 0;
			if (AsyncCausalityTracer.LoggingOn)
			{
				AsyncCausalityTracer.TraceOperationCreation(CausalityTraceLevel.Required, base.Id, "Task.Unwrap", 0UL);
			}
			if (Task.s_asyncDebuggingEnabled)
			{
				Task.AddToActiveTasks(this);
			}
			if (outerTask.IsCompleted)
			{
				this.ProcessCompletedOuterTask(outerTask);
				return;
			}
			outerTask.AddCompletionAction(this);
		}

		// Token: 0x06004163 RID: 16739 RVA: 0x000F3DF4 File Offset: 0x000F1FF4
		public void Invoke(Task completingTask)
		{
			StackGuard currentStackGuard = Task.CurrentStackGuard;
			if (currentStackGuard.TryBeginInliningScope())
			{
				try
				{
					this.InvokeCore(completingTask);
					return;
				}
				finally
				{
					currentStackGuard.EndInliningScope();
				}
			}
			this.InvokeCoreAsync(completingTask);
		}

		// Token: 0x06004164 RID: 16740 RVA: 0x000F3E38 File Offset: 0x000F2038
		private void InvokeCore(Task completingTask)
		{
			byte state = this._state;
			if (state == 0)
			{
				this.ProcessCompletedOuterTask(completingTask);
				return;
			}
			if (state != 1)
			{
				return;
			}
			bool flag = this.TrySetFromTask(completingTask, false);
			this._state = 2;
		}

		// Token: 0x06004165 RID: 16741 RVA: 0x000F3E6C File Offset: 0x000F206C
		[SecuritySafeCritical]
		private void InvokeCoreAsync(Task completingTask)
		{
			ThreadPool.UnsafeQueueUserWorkItem(delegate(object state)
			{
				Tuple<UnwrapPromise<TResult>, Task> tuple = (Tuple<UnwrapPromise<TResult>, Task>)state;
				tuple.Item1.InvokeCore(tuple.Item2);
			}, Tuple.Create<UnwrapPromise<TResult>, Task>(this, completingTask));
		}

		// Token: 0x06004166 RID: 16742 RVA: 0x000F3E9C File Offset: 0x000F209C
		private void ProcessCompletedOuterTask(Task task)
		{
			this._state = 1;
			TaskStatus status = task.Status;
			if (status != TaskStatus.RanToCompletion)
			{
				if (status - TaskStatus.Canceled <= 1)
				{
					bool flag = this.TrySetFromTask(task, this._lookForOce);
					return;
				}
			}
			else
			{
				Task<Task<TResult>> task2 = task as Task<Task<TResult>>;
				this.ProcessInnerTask((task2 != null) ? task2.Result : ((Task<Task>)task).Result);
			}
		}

		// Token: 0x06004167 RID: 16743 RVA: 0x000F3EF4 File Offset: 0x000F20F4
		private bool TrySetFromTask(Task task, bool lookForOce)
		{
			if (AsyncCausalityTracer.LoggingOn)
			{
				AsyncCausalityTracer.TraceOperationRelation(CausalityTraceLevel.Important, base.Id, CausalityRelation.Join);
			}
			bool flag = false;
			switch (task.Status)
			{
			case TaskStatus.RanToCompletion:
			{
				Task<TResult> task2 = task as Task<TResult>;
				if (AsyncCausalityTracer.LoggingOn)
				{
					AsyncCausalityTracer.TraceOperationCompletion(CausalityTraceLevel.Required, base.Id, AsyncCausalityStatus.Completed);
				}
				if (Task.s_asyncDebuggingEnabled)
				{
					Task.RemoveFromActiveTasks(base.Id);
				}
				flag = base.TrySetResult((task2 != null) ? task2.Result : default(TResult));
				break;
			}
			case TaskStatus.Canceled:
				flag = base.TrySetCanceled(task.CancellationToken, task.GetCancellationExceptionDispatchInfo());
				break;
			case TaskStatus.Faulted:
			{
				ReadOnlyCollection<ExceptionDispatchInfo> exceptionDispatchInfos = task.GetExceptionDispatchInfos();
				ExceptionDispatchInfo exceptionDispatchInfo;
				OperationCanceledException ex;
				if (lookForOce && exceptionDispatchInfos.Count > 0 && (exceptionDispatchInfo = exceptionDispatchInfos[0]) != null && (ex = exceptionDispatchInfo.SourceException as OperationCanceledException) != null)
				{
					flag = base.TrySetCanceled(ex.CancellationToken, exceptionDispatchInfo);
				}
				else
				{
					flag = base.TrySetException(exceptionDispatchInfos);
				}
				break;
			}
			}
			return flag;
		}

		// Token: 0x06004168 RID: 16744 RVA: 0x000F3FE8 File Offset: 0x000F21E8
		private void ProcessInnerTask(Task task)
		{
			if (task == null)
			{
				base.TrySetCanceled(default(CancellationToken));
				this._state = 2;
				return;
			}
			if (task.IsCompleted)
			{
				this.TrySetFromTask(task, false);
				this._state = 2;
				return;
			}
			task.AddCompletionAction(this);
		}

		// Token: 0x04001B4E RID: 6990
		private const byte STATE_WAITING_ON_OUTER_TASK = 0;

		// Token: 0x04001B4F RID: 6991
		private const byte STATE_WAITING_ON_INNER_TASK = 1;

		// Token: 0x04001B50 RID: 6992
		private const byte STATE_DONE = 2;

		// Token: 0x04001B51 RID: 6993
		private byte _state;

		// Token: 0x04001B52 RID: 6994
		private readonly bool _lookForOce;

		// Token: 0x02000C1D RID: 3101
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06006FF2 RID: 28658 RVA: 0x001820C7 File Offset: 0x001802C7
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06006FF3 RID: 28659 RVA: 0x001820D3 File Offset: 0x001802D3
			public <>c()
			{
			}

			// Token: 0x06006FF4 RID: 28660 RVA: 0x001820DC File Offset: 0x001802DC
			internal void <InvokeCoreAsync>b__8_0(object state)
			{
				Tuple<UnwrapPromise<TResult>, Task> tuple = (Tuple<UnwrapPromise<TResult>, Task>)state;
				tuple.Item1.InvokeCore(tuple.Item2);
			}

			// Token: 0x040036D0 RID: 14032
			public static readonly UnwrapPromise<TResult>.<>c <>9 = new UnwrapPromise<TResult>.<>c();

			// Token: 0x040036D1 RID: 14033
			public static WaitCallback <>9__8_0;
		}
	}
}
