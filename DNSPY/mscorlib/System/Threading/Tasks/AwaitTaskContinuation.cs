using System;
using System.Diagnostics.Tracing;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security;

namespace System.Threading.Tasks
{
	// Token: 0x02000572 RID: 1394
	internal class AwaitTaskContinuation : TaskContinuation, IThreadPoolWorkItem
	{
		// Token: 0x06004180 RID: 16768 RVA: 0x000F46AC File Offset: 0x000F28AC
		[SecurityCritical]
		internal AwaitTaskContinuation(Action action, bool flowExecutionContext, ref StackCrawlMark stackMark)
		{
			this.m_action = action;
			if (flowExecutionContext)
			{
				this.m_capturedContext = ExecutionContext.Capture(ref stackMark, ExecutionContext.CaptureOptions.IgnoreSyncCtx | ExecutionContext.CaptureOptions.OptimizeDefaultCase);
			}
		}

		// Token: 0x06004181 RID: 16769 RVA: 0x000F46CB File Offset: 0x000F28CB
		[SecurityCritical]
		internal AwaitTaskContinuation(Action action, bool flowExecutionContext)
		{
			this.m_action = action;
			if (flowExecutionContext)
			{
				this.m_capturedContext = ExecutionContext.FastCapture();
			}
		}

		// Token: 0x06004182 RID: 16770 RVA: 0x000F46E8 File Offset: 0x000F28E8
		protected Task CreateTask(Action<object> action, object state, TaskScheduler scheduler)
		{
			return new Task(action, state, null, default(CancellationToken), TaskCreationOptions.None, InternalTaskOptions.QueuedByRuntime, scheduler)
			{
				CapturedContext = this.m_capturedContext
			};
		}

		// Token: 0x06004183 RID: 16771 RVA: 0x000F471C File Offset: 0x000F291C
		[SecuritySafeCritical]
		internal override void Run(Task task, bool canInlineContinuationTask)
		{
			if (canInlineContinuationTask && AwaitTaskContinuation.IsValidLocationForInlining)
			{
				this.RunCallback(AwaitTaskContinuation.GetInvokeActionCallback(), this.m_action, ref Task.t_currentTask);
				return;
			}
			TplEtwProvider log = TplEtwProvider.Log;
			if (log.IsEnabled())
			{
				this.m_continuationId = Task.NewId();
				log.AwaitTaskContinuationScheduled((task.ExecutingTaskScheduler ?? TaskScheduler.Default).Id, task.Id, this.m_continuationId);
			}
			ThreadPool.UnsafeQueueCustomWorkItem(this, false);
		}

		// Token: 0x170009C1 RID: 2497
		// (get) Token: 0x06004184 RID: 16772 RVA: 0x000F4790 File Offset: 0x000F2990
		internal static bool IsValidLocationForInlining
		{
			get
			{
				SynchronizationContext currentNoFlow = SynchronizationContext.CurrentNoFlow;
				if (currentNoFlow != null && currentNoFlow.GetType() != typeof(SynchronizationContext))
				{
					return false;
				}
				TaskScheduler internalCurrent = TaskScheduler.InternalCurrent;
				return internalCurrent == null || internalCurrent == TaskScheduler.Default;
			}
		}

		// Token: 0x06004185 RID: 16773 RVA: 0x000F47D4 File Offset: 0x000F29D4
		[SecurityCritical]
		private void ExecuteWorkItemHelper()
		{
			TplEtwProvider log = TplEtwProvider.Log;
			Guid empty = Guid.Empty;
			if (log.TasksSetActivityIds && this.m_continuationId != 0)
			{
				Guid guid = TplEtwProvider.CreateGuidForTaskID(this.m_continuationId);
				EventSource.SetCurrentThreadActivityId(guid, out empty);
			}
			try
			{
				if (this.m_capturedContext == null)
				{
					this.m_action();
				}
				else
				{
					try
					{
						ExecutionContext.Run(this.m_capturedContext, AwaitTaskContinuation.GetInvokeActionCallback(), this.m_action, true);
					}
					finally
					{
						this.m_capturedContext.Dispose();
					}
				}
			}
			finally
			{
				if (log.TasksSetActivityIds && this.m_continuationId != 0)
				{
					EventSource.SetCurrentThreadActivityId(empty);
				}
			}
		}

		// Token: 0x06004186 RID: 16774 RVA: 0x000F4880 File Offset: 0x000F2A80
		[SecurityCritical]
		void IThreadPoolWorkItem.ExecuteWorkItem()
		{
			if (this.m_capturedContext == null && !TplEtwProvider.Log.IsEnabled())
			{
				this.m_action();
				return;
			}
			this.ExecuteWorkItemHelper();
		}

		// Token: 0x06004187 RID: 16775 RVA: 0x000F48A8 File Offset: 0x000F2AA8
		[SecurityCritical]
		void IThreadPoolWorkItem.MarkAborted(ThreadAbortException tae)
		{
		}

		// Token: 0x06004188 RID: 16776 RVA: 0x000F48AA File Offset: 0x000F2AAA
		[SecurityCritical]
		private static void InvokeAction(object state)
		{
			((Action)state)();
		}

		// Token: 0x06004189 RID: 16777 RVA: 0x000F48B8 File Offset: 0x000F2AB8
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected static ContextCallback GetInvokeActionCallback()
		{
			ContextCallback contextCallback = AwaitTaskContinuation.s_invokeActionCallback;
			if (contextCallback == null)
			{
				contextCallback = (AwaitTaskContinuation.s_invokeActionCallback = new ContextCallback(AwaitTaskContinuation.InvokeAction));
			}
			return contextCallback;
		}

		// Token: 0x0600418A RID: 16778 RVA: 0x000F48E4 File Offset: 0x000F2AE4
		[SecurityCritical]
		protected void RunCallback(ContextCallback callback, object state, ref Task currentTask)
		{
			Task task = currentTask;
			try
			{
				if (task != null)
				{
					currentTask = null;
				}
				if (this.m_capturedContext == null)
				{
					callback(state);
				}
				else
				{
					ExecutionContext.Run(this.m_capturedContext, callback, state, true);
				}
			}
			catch (Exception ex)
			{
				AwaitTaskContinuation.ThrowAsyncIfNecessary(ex);
			}
			finally
			{
				if (task != null)
				{
					currentTask = task;
				}
				if (this.m_capturedContext != null)
				{
					this.m_capturedContext.Dispose();
				}
			}
		}

		// Token: 0x0600418B RID: 16779 RVA: 0x000F495C File Offset: 0x000F2B5C
		[SecurityCritical]
		internal static void RunOrScheduleAction(Action action, bool allowInlining, ref Task currentTask)
		{
			if (!allowInlining || !AwaitTaskContinuation.IsValidLocationForInlining)
			{
				AwaitTaskContinuation.UnsafeScheduleAction(action, currentTask);
				return;
			}
			Task task = currentTask;
			try
			{
				if (task != null)
				{
					currentTask = null;
				}
				action();
			}
			catch (Exception ex)
			{
				AwaitTaskContinuation.ThrowAsyncIfNecessary(ex);
			}
			finally
			{
				if (task != null)
				{
					currentTask = task;
				}
			}
		}

		// Token: 0x0600418C RID: 16780 RVA: 0x000F49BC File Offset: 0x000F2BBC
		[SecurityCritical]
		internal static void UnsafeScheduleAction(Action action, Task task)
		{
			AwaitTaskContinuation awaitTaskContinuation = new AwaitTaskContinuation(action, false);
			TplEtwProvider log = TplEtwProvider.Log;
			if (log.IsEnabled() && task != null)
			{
				awaitTaskContinuation.m_continuationId = Task.NewId();
				log.AwaitTaskContinuationScheduled((task.ExecutingTaskScheduler ?? TaskScheduler.Default).Id, task.Id, awaitTaskContinuation.m_continuationId);
			}
			ThreadPool.UnsafeQueueCustomWorkItem(awaitTaskContinuation, false);
		}

		// Token: 0x0600418D RID: 16781 RVA: 0x000F4A1C File Offset: 0x000F2C1C
		protected static void ThrowAsyncIfNecessary(Exception exc)
		{
			if (!(exc is ThreadAbortException) && !(exc is AppDomainUnloadedException) && !WindowsRuntimeMarshal.ReportUnhandledError(exc))
			{
				ExceptionDispatchInfo exceptionDispatchInfo = ExceptionDispatchInfo.Capture(exc);
				ThreadPool.QueueUserWorkItem(delegate(object s)
				{
					((ExceptionDispatchInfo)s).Throw();
				}, exceptionDispatchInfo);
			}
		}

		// Token: 0x0600418E RID: 16782 RVA: 0x000F4A6E File Offset: 0x000F2C6E
		internal override Delegate[] GetDelegateContinuationsForDebugger()
		{
			return new Delegate[] { AsyncMethodBuilderCore.TryGetStateMachineForDebugger(this.m_action) };
		}

		// Token: 0x04001B5E RID: 7006
		private readonly ExecutionContext m_capturedContext;

		// Token: 0x04001B5F RID: 7007
		protected readonly Action m_action;

		// Token: 0x04001B60 RID: 7008
		protected int m_continuationId;

		// Token: 0x04001B61 RID: 7009
		[SecurityCritical]
		private static ContextCallback s_invokeActionCallback;

		// Token: 0x02000C21 RID: 3105
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06006FFD RID: 28669 RVA: 0x001821C0 File Offset: 0x001803C0
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06006FFE RID: 28670 RVA: 0x001821CC File Offset: 0x001803CC
			public <>c()
			{
			}

			// Token: 0x06006FFF RID: 28671 RVA: 0x001821D4 File Offset: 0x001803D4
			internal void <ThrowAsyncIfNecessary>b__18_0(object s)
			{
				((ExceptionDispatchInfo)s).Throw();
			}

			// Token: 0x040036D7 RID: 14039
			public static readonly AwaitTaskContinuation.<>c <>9 = new AwaitTaskContinuation.<>c();

			// Token: 0x040036D8 RID: 14040
			public static WaitCallback <>9__18_0;
		}
	}
}
