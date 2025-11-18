using System;
using System.Diagnostics.Tracing;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Threading.Tasks
{
	// Token: 0x02000570 RID: 1392
	internal sealed class SynchronizationContextAwaitTaskContinuation : AwaitTaskContinuation
	{
		// Token: 0x06004178 RID: 16760 RVA: 0x000F448C File Offset: 0x000F268C
		[SecurityCritical]
		internal SynchronizationContextAwaitTaskContinuation(SynchronizationContext context, Action action, bool flowExecutionContext, ref StackCrawlMark stackMark)
			: base(action, flowExecutionContext, ref stackMark)
		{
			this.m_syncContext = context;
		}

		// Token: 0x06004179 RID: 16761 RVA: 0x000F44A0 File Offset: 0x000F26A0
		[SecuritySafeCritical]
		internal sealed override void Run(Task task, bool canInlineContinuationTask)
		{
			if (canInlineContinuationTask && this.m_syncContext == SynchronizationContext.CurrentNoFlow)
			{
				base.RunCallback(AwaitTaskContinuation.GetInvokeActionCallback(), this.m_action, ref Task.t_currentTask);
				return;
			}
			TplEtwProvider log = TplEtwProvider.Log;
			if (log.IsEnabled())
			{
				this.m_continuationId = Task.NewId();
				log.AwaitTaskContinuationScheduled((task.ExecutingTaskScheduler ?? TaskScheduler.Default).Id, task.Id, this.m_continuationId);
			}
			base.RunCallback(SynchronizationContextAwaitTaskContinuation.GetPostActionCallback(), this, ref Task.t_currentTask);
		}

		// Token: 0x0600417A RID: 16762 RVA: 0x000F4524 File Offset: 0x000F2724
		[SecurityCritical]
		private static void PostAction(object state)
		{
			SynchronizationContextAwaitTaskContinuation synchronizationContextAwaitTaskContinuation = (SynchronizationContextAwaitTaskContinuation)state;
			TplEtwProvider log = TplEtwProvider.Log;
			if (log.TasksSetActivityIds && synchronizationContextAwaitTaskContinuation.m_continuationId != 0)
			{
				synchronizationContextAwaitTaskContinuation.m_syncContext.Post(SynchronizationContextAwaitTaskContinuation.s_postCallback, SynchronizationContextAwaitTaskContinuation.GetActionLogDelegate(synchronizationContextAwaitTaskContinuation.m_continuationId, synchronizationContextAwaitTaskContinuation.m_action));
				return;
			}
			synchronizationContextAwaitTaskContinuation.m_syncContext.Post(SynchronizationContextAwaitTaskContinuation.s_postCallback, synchronizationContextAwaitTaskContinuation.m_action);
		}

		// Token: 0x0600417B RID: 16763 RVA: 0x000F4588 File Offset: 0x000F2788
		private static Action GetActionLogDelegate(int continuationId, Action action)
		{
			return delegate
			{
				Guid guid = TplEtwProvider.CreateGuidForTaskID(continuationId);
				Guid guid2;
				EventSource.SetCurrentThreadActivityId(guid, out guid2);
				try
				{
					action();
				}
				finally
				{
					EventSource.SetCurrentThreadActivityId(guid2);
				}
			};
		}

		// Token: 0x0600417C RID: 16764 RVA: 0x000F45B8 File Offset: 0x000F27B8
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static ContextCallback GetPostActionCallback()
		{
			ContextCallback contextCallback = SynchronizationContextAwaitTaskContinuation.s_postActionCallback;
			if (contextCallback == null)
			{
				contextCallback = (SynchronizationContextAwaitTaskContinuation.s_postActionCallback = new ContextCallback(SynchronizationContextAwaitTaskContinuation.PostAction));
			}
			return contextCallback;
		}

		// Token: 0x0600417D RID: 16765 RVA: 0x000F45E2 File Offset: 0x000F27E2
		// Note: this type is marked as 'beforefieldinit'.
		static SynchronizationContextAwaitTaskContinuation()
		{
		}

		// Token: 0x04001B5A RID: 7002
		private static readonly SendOrPostCallback s_postCallback = delegate(object state)
		{
			((Action)state)();
		};

		// Token: 0x04001B5B RID: 7003
		[SecurityCritical]
		private static ContextCallback s_postActionCallback;

		// Token: 0x04001B5C RID: 7004
		private readonly SynchronizationContext m_syncContext;

		// Token: 0x02000C1E RID: 3102
		[CompilerGenerated]
		private sealed class <>c__DisplayClass6_0
		{
			// Token: 0x06006FF5 RID: 28661 RVA: 0x00182101 File Offset: 0x00180301
			public <>c__DisplayClass6_0()
			{
			}

			// Token: 0x06006FF6 RID: 28662 RVA: 0x0018210C File Offset: 0x0018030C
			internal void <GetActionLogDelegate>b__0()
			{
				Guid guid = TplEtwProvider.CreateGuidForTaskID(this.continuationId);
				Guid guid2;
				EventSource.SetCurrentThreadActivityId(guid, out guid2);
				try
				{
					this.action();
				}
				finally
				{
					EventSource.SetCurrentThreadActivityId(guid2);
				}
			}

			// Token: 0x040036D2 RID: 14034
			public int continuationId;

			// Token: 0x040036D3 RID: 14035
			public Action action;
		}

		// Token: 0x02000C1F RID: 3103
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06006FF7 RID: 28663 RVA: 0x00182154 File Offset: 0x00180354
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06006FF8 RID: 28664 RVA: 0x00182160 File Offset: 0x00180360
			public <>c()
			{
			}

			// Token: 0x06006FF9 RID: 28665 RVA: 0x00182168 File Offset: 0x00180368
			internal void <.cctor>b__8_0(object state)
			{
				((Action)state)();
			}

			// Token: 0x040036D4 RID: 14036
			public static readonly SynchronizationContextAwaitTaskContinuation.<>c <>9 = new SynchronizationContextAwaitTaskContinuation.<>c();
		}
	}
}
