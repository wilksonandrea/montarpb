using System;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Threading.Tasks
{
	// Token: 0x02000571 RID: 1393
	internal sealed class TaskSchedulerAwaitTaskContinuation : AwaitTaskContinuation
	{
		// Token: 0x0600417E RID: 16766 RVA: 0x000F45F9 File Offset: 0x000F27F9
		[SecurityCritical]
		internal TaskSchedulerAwaitTaskContinuation(TaskScheduler scheduler, Action action, bool flowExecutionContext, ref StackCrawlMark stackMark)
			: base(action, flowExecutionContext, ref stackMark)
		{
			this.m_scheduler = scheduler;
		}

		// Token: 0x0600417F RID: 16767 RVA: 0x000F460C File Offset: 0x000F280C
		internal sealed override void Run(Task ignored, bool canInlineContinuationTask)
		{
			if (this.m_scheduler == TaskScheduler.Default)
			{
				base.Run(ignored, canInlineContinuationTask);
				return;
			}
			bool flag = canInlineContinuationTask && (TaskScheduler.InternalCurrent == this.m_scheduler || Thread.CurrentThread.IsThreadPoolThread);
			Task task = base.CreateTask(delegate(object state)
			{
				try
				{
					((Action)state)();
				}
				catch (Exception ex)
				{
					AwaitTaskContinuation.ThrowAsyncIfNecessary(ex);
				}
			}, this.m_action, this.m_scheduler);
			if (flag)
			{
				TaskContinuation.InlineIfPossibleOrElseQueue(task, false);
				return;
			}
			try
			{
				task.ScheduleAndStart(false);
			}
			catch (TaskSchedulerException)
			{
			}
		}

		// Token: 0x04001B5D RID: 7005
		private readonly TaskScheduler m_scheduler;

		// Token: 0x02000C20 RID: 3104
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06006FFA RID: 28666 RVA: 0x00182175 File Offset: 0x00180375
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06006FFB RID: 28667 RVA: 0x00182181 File Offset: 0x00180381
			public <>c()
			{
			}

			// Token: 0x06006FFC RID: 28668 RVA: 0x0018218C File Offset: 0x0018038C
			internal void <Run>b__2_0(object state)
			{
				try
				{
					((Action)state)();
				}
				catch (Exception ex)
				{
					AwaitTaskContinuation.ThrowAsyncIfNecessary(ex);
				}
			}

			// Token: 0x040036D5 RID: 14037
			public static readonly TaskSchedulerAwaitTaskContinuation.<>c <>9 = new TaskSchedulerAwaitTaskContinuation.<>c();

			// Token: 0x040036D6 RID: 14038
			public static Action<object> <>9__2_0;
		}
	}
}
