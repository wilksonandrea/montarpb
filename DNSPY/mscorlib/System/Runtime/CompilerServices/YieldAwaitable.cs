using System;
using System.Diagnostics.Tracing;
using System.Security;
using System.Security.Permissions;
using System.Threading;
using System.Threading.Tasks;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020008FC RID: 2300
	[__DynamicallyInvokable]
	public struct YieldAwaitable
	{
		// Token: 0x06005E55 RID: 24149 RVA: 0x0014B304 File Offset: 0x00149504
		[__DynamicallyInvokable]
		public YieldAwaitable.YieldAwaiter GetAwaiter()
		{
			return default(YieldAwaitable.YieldAwaiter);
		}

		// Token: 0x02000C98 RID: 3224
		[__DynamicallyInvokable]
		[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
		public struct YieldAwaiter : ICriticalNotifyCompletion, INotifyCompletion
		{
			// Token: 0x17001362 RID: 4962
			// (get) Token: 0x0600710F RID: 28943 RVA: 0x00184FED File Offset: 0x001831ED
			[__DynamicallyInvokable]
			public bool IsCompleted
			{
				[__DynamicallyInvokable]
				get
				{
					return false;
				}
			}

			// Token: 0x06007110 RID: 28944 RVA: 0x00184FF0 File Offset: 0x001831F0
			[SecuritySafeCritical]
			[__DynamicallyInvokable]
			public void OnCompleted(Action continuation)
			{
				YieldAwaitable.YieldAwaiter.QueueContinuation(continuation, true);
			}

			// Token: 0x06007111 RID: 28945 RVA: 0x00184FF9 File Offset: 0x001831F9
			[SecurityCritical]
			[__DynamicallyInvokable]
			public void UnsafeOnCompleted(Action continuation)
			{
				YieldAwaitable.YieldAwaiter.QueueContinuation(continuation, false);
			}

			// Token: 0x06007112 RID: 28946 RVA: 0x00185004 File Offset: 0x00183204
			[SecurityCritical]
			private static void QueueContinuation(Action continuation, bool flowContext)
			{
				if (continuation == null)
				{
					throw new ArgumentNullException("continuation");
				}
				if (TplEtwProvider.Log.IsEnabled())
				{
					continuation = YieldAwaitable.YieldAwaiter.OutputCorrelationEtwEvent(continuation);
				}
				SynchronizationContext currentNoFlow = SynchronizationContext.CurrentNoFlow;
				if (currentNoFlow != null && currentNoFlow.GetType() != typeof(SynchronizationContext))
				{
					currentNoFlow.Post(YieldAwaitable.YieldAwaiter.s_sendOrPostCallbackRunAction, continuation);
					return;
				}
				TaskScheduler taskScheduler = TaskScheduler.Current;
				if (taskScheduler != TaskScheduler.Default)
				{
					Task.Factory.StartNew(continuation, default(CancellationToken), TaskCreationOptions.PreferFairness, taskScheduler);
					return;
				}
				if (flowContext)
				{
					ThreadPool.QueueUserWorkItem(YieldAwaitable.YieldAwaiter.s_waitCallbackRunAction, continuation);
					return;
				}
				ThreadPool.UnsafeQueueUserWorkItem(YieldAwaitable.YieldAwaiter.s_waitCallbackRunAction, continuation);
			}

			// Token: 0x06007113 RID: 28947 RVA: 0x001850A4 File Offset: 0x001832A4
			private static Action OutputCorrelationEtwEvent(Action continuation)
			{
				int continuationId = Task.NewId();
				Task internalCurrent = Task.InternalCurrent;
				TplEtwProvider.Log.AwaitTaskContinuationScheduled(TaskScheduler.Current.Id, (internalCurrent != null) ? internalCurrent.Id : 0, continuationId);
				return AsyncMethodBuilderCore.CreateContinuationWrapper(continuation, delegate
				{
					TplEtwProvider log = TplEtwProvider.Log;
					log.TaskWaitContinuationStarted(continuationId);
					Guid guid = default(Guid);
					if (log.TasksSetActivityIds)
					{
						EventSource.SetCurrentThreadActivityId(TplEtwProvider.CreateGuidForTaskID(continuationId), out guid);
					}
					continuation();
					if (log.TasksSetActivityIds)
					{
						EventSource.SetCurrentThreadActivityId(guid);
					}
					log.TaskWaitContinuationComplete(continuationId);
				}, null);
			}

			// Token: 0x06007114 RID: 28948 RVA: 0x0018510D File Offset: 0x0018330D
			private static void RunAction(object state)
			{
				((Action)state)();
			}

			// Token: 0x06007115 RID: 28949 RVA: 0x0018511A File Offset: 0x0018331A
			[__DynamicallyInvokable]
			public void GetResult()
			{
			}

			// Token: 0x06007116 RID: 28950 RVA: 0x0018511C File Offset: 0x0018331C
			// Note: this type is marked as 'beforefieldinit'.
			static YieldAwaiter()
			{
			}

			// Token: 0x0400385A RID: 14426
			private static readonly WaitCallback s_waitCallbackRunAction = new WaitCallback(YieldAwaitable.YieldAwaiter.RunAction);

			// Token: 0x0400385B RID: 14427
			private static readonly SendOrPostCallback s_sendOrPostCallbackRunAction = new SendOrPostCallback(YieldAwaitable.YieldAwaiter.RunAction);

			// Token: 0x02000D16 RID: 3350
			[CompilerGenerated]
			private sealed class <>c__DisplayClass5_0
			{
				// Token: 0x06007233 RID: 29235 RVA: 0x00189552 File Offset: 0x00187752
				public <>c__DisplayClass5_0()
				{
				}

				// Token: 0x06007234 RID: 29236 RVA: 0x0018955C File Offset: 0x0018775C
				internal void <OutputCorrelationEtwEvent>b__0()
				{
					TplEtwProvider log = TplEtwProvider.Log;
					log.TaskWaitContinuationStarted(this.continuationId);
					Guid guid = default(Guid);
					if (log.TasksSetActivityIds)
					{
						EventSource.SetCurrentThreadActivityId(TplEtwProvider.CreateGuidForTaskID(this.continuationId), out guid);
					}
					this.continuation();
					if (log.TasksSetActivityIds)
					{
						EventSource.SetCurrentThreadActivityId(guid);
					}
					log.TaskWaitContinuationComplete(this.continuationId);
				}

				// Token: 0x04003975 RID: 14709
				public int continuationId;

				// Token: 0x04003976 RID: 14710
				public Action continuation;
			}
		}
	}
}
