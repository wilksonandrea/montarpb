using System;
using System.Collections.ObjectModel;
using System.Diagnostics.Tracing;
using System.Runtime.ExceptionServices;
using System.Security;
using System.Security.Permissions;
using System.Threading;
using System.Threading.Tasks;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020008F8 RID: 2296
	[__DynamicallyInvokable]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	public struct TaskAwaiter : ICriticalNotifyCompletion, INotifyCompletion
	{
		// Token: 0x06005E42 RID: 24130 RVA: 0x0014B097 File Offset: 0x00149297
		internal TaskAwaiter(Task task)
		{
			this.m_task = task;
		}

		// Token: 0x1700102E RID: 4142
		// (get) Token: 0x06005E43 RID: 24131 RVA: 0x0014B0A0 File Offset: 0x001492A0
		[__DynamicallyInvokable]
		public bool IsCompleted
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_task.IsCompleted;
			}
		}

		// Token: 0x06005E44 RID: 24132 RVA: 0x0014B0AD File Offset: 0x001492AD
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public void OnCompleted(Action continuation)
		{
			TaskAwaiter.OnCompletedInternal(this.m_task, continuation, true, true);
		}

		// Token: 0x06005E45 RID: 24133 RVA: 0x0014B0BD File Offset: 0x001492BD
		[SecurityCritical]
		[__DynamicallyInvokable]
		public void UnsafeOnCompleted(Action continuation)
		{
			TaskAwaiter.OnCompletedInternal(this.m_task, continuation, true, false);
		}

		// Token: 0x06005E46 RID: 24134 RVA: 0x0014B0CD File Offset: 0x001492CD
		[__DynamicallyInvokable]
		public void GetResult()
		{
			TaskAwaiter.ValidateEnd(this.m_task);
		}

		// Token: 0x06005E47 RID: 24135 RVA: 0x0014B0DA File Offset: 0x001492DA
		internal static void ValidateEnd(Task task)
		{
			if (task.IsWaitNotificationEnabledOrNotRanToCompletion)
			{
				TaskAwaiter.HandleNonSuccessAndDebuggerNotification(task);
			}
		}

		// Token: 0x06005E48 RID: 24136 RVA: 0x0014B0EC File Offset: 0x001492EC
		private static void HandleNonSuccessAndDebuggerNotification(Task task)
		{
			if (!task.IsCompleted)
			{
				bool flag = task.InternalWait(-1, default(CancellationToken));
			}
			task.NotifyDebuggerOfWaitCompletionIfNecessary();
			if (!task.IsRanToCompletion)
			{
				TaskAwaiter.ThrowForNonSuccess(task);
			}
		}

		// Token: 0x06005E49 RID: 24137 RVA: 0x0014B128 File Offset: 0x00149328
		private static void ThrowForNonSuccess(Task task)
		{
			TaskStatus status = task.Status;
			if (status == TaskStatus.Canceled)
			{
				ExceptionDispatchInfo cancellationExceptionDispatchInfo = task.GetCancellationExceptionDispatchInfo();
				if (cancellationExceptionDispatchInfo != null)
				{
					cancellationExceptionDispatchInfo.Throw();
				}
				throw new TaskCanceledException(task);
			}
			if (status != TaskStatus.Faulted)
			{
				return;
			}
			ReadOnlyCollection<ExceptionDispatchInfo> exceptionDispatchInfos = task.GetExceptionDispatchInfos();
			if (exceptionDispatchInfos.Count > 0)
			{
				exceptionDispatchInfos[0].Throw();
				return;
			}
			throw task.Exception;
		}

		// Token: 0x06005E4A RID: 24138 RVA: 0x0014B180 File Offset: 0x00149380
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static void OnCompletedInternal(Task task, Action continuation, bool continueOnCapturedContext, bool flowExecutionContext)
		{
			if (continuation == null)
			{
				throw new ArgumentNullException("continuation");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			if (TplEtwProvider.Log.IsEnabled() || Task.s_asyncDebuggingEnabled)
			{
				continuation = TaskAwaiter.OutputWaitEtwEvents(task, continuation);
			}
			task.SetContinuationForAwait(continuation, continueOnCapturedContext, flowExecutionContext, ref stackCrawlMark);
		}

		// Token: 0x06005E4B RID: 24139 RVA: 0x0014B1C4 File Offset: 0x001493C4
		private static Action OutputWaitEtwEvents(Task task, Action continuation)
		{
			if (Task.s_asyncDebuggingEnabled)
			{
				Task.AddToActiveTasks(task);
			}
			TplEtwProvider etwLog = TplEtwProvider.Log;
			if (etwLog.IsEnabled())
			{
				Task internalCurrent = Task.InternalCurrent;
				Task task2 = AsyncMethodBuilderCore.TryGetContinuationTask(continuation);
				etwLog.TaskWaitBegin((internalCurrent != null) ? internalCurrent.m_taskScheduler.Id : TaskScheduler.Default.Id, (internalCurrent != null) ? internalCurrent.Id : 0, task.Id, TplEtwProvider.TaskWaitBehavior.Asynchronous, (task2 != null) ? task2.Id : 0, Thread.GetDomainID());
			}
			return AsyncMethodBuilderCore.CreateContinuationWrapper(continuation, delegate
			{
				if (Task.s_asyncDebuggingEnabled)
				{
					Task.RemoveFromActiveTasks(task.Id);
				}
				Guid guid = default(Guid);
				bool flag = etwLog.IsEnabled();
				if (flag)
				{
					Task internalCurrent2 = Task.InternalCurrent;
					etwLog.TaskWaitEnd((internalCurrent2 != null) ? internalCurrent2.m_taskScheduler.Id : TaskScheduler.Default.Id, (internalCurrent2 != null) ? internalCurrent2.Id : 0, task.Id);
					if (etwLog.TasksSetActivityIds && (task.Options & (TaskCreationOptions)1024) != TaskCreationOptions.None)
					{
						EventSource.SetCurrentThreadActivityId(TplEtwProvider.CreateGuidForTaskID(task.Id), out guid);
					}
				}
				continuation();
				if (flag)
				{
					etwLog.TaskWaitContinuationComplete(task.Id);
					if (etwLog.TasksSetActivityIds && (task.Options & (TaskCreationOptions)1024) != TaskCreationOptions.None)
					{
						EventSource.SetCurrentThreadActivityId(guid);
					}
				}
			}, null);
		}

		// Token: 0x04002A5D RID: 10845
		private readonly Task m_task;

		// Token: 0x02000C95 RID: 3221
		[CompilerGenerated]
		private sealed class <>c__DisplayClass11_0
		{
			// Token: 0x06007103 RID: 28931 RVA: 0x00184E32 File Offset: 0x00183032
			public <>c__DisplayClass11_0()
			{
			}

			// Token: 0x06007104 RID: 28932 RVA: 0x00184E3C File Offset: 0x0018303C
			internal void <OutputWaitEtwEvents>b__0()
			{
				if (Task.s_asyncDebuggingEnabled)
				{
					Task.RemoveFromActiveTasks(this.task.Id);
				}
				Guid guid = default(Guid);
				bool flag = this.etwLog.IsEnabled();
				if (flag)
				{
					Task internalCurrent = Task.InternalCurrent;
					this.etwLog.TaskWaitEnd((internalCurrent != null) ? internalCurrent.m_taskScheduler.Id : TaskScheduler.Default.Id, (internalCurrent != null) ? internalCurrent.Id : 0, this.task.Id);
					if (this.etwLog.TasksSetActivityIds && (this.task.Options & (TaskCreationOptions)1024) != TaskCreationOptions.None)
					{
						EventSource.SetCurrentThreadActivityId(TplEtwProvider.CreateGuidForTaskID(this.task.Id), out guid);
					}
				}
				this.continuation();
				if (flag)
				{
					this.etwLog.TaskWaitContinuationComplete(this.task.Id);
					if (this.etwLog.TasksSetActivityIds && (this.task.Options & (TaskCreationOptions)1024) != TaskCreationOptions.None)
					{
						EventSource.SetCurrentThreadActivityId(guid);
					}
				}
			}

			// Token: 0x04003853 RID: 14419
			public Task task;

			// Token: 0x04003854 RID: 14420
			public TplEtwProvider etwLog;

			// Token: 0x04003855 RID: 14421
			public Action continuation;
		}
	}
}
