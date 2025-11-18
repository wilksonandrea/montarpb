using System;
using System.Collections.Generic;
using System.Security;

namespace System.Threading.Tasks
{
	// Token: 0x02000578 RID: 1400
	internal sealed class SynchronizationContextTaskScheduler : TaskScheduler
	{
		// Token: 0x0600421C RID: 16924 RVA: 0x000F6278 File Offset: 0x000F4478
		internal SynchronizationContextTaskScheduler()
		{
			SynchronizationContext synchronizationContext = SynchronizationContext.Current;
			if (synchronizationContext == null)
			{
				throw new InvalidOperationException(Environment.GetResourceString("TaskScheduler_FromCurrentSynchronizationContext_NoCurrent"));
			}
			this.m_synchronizationContext = synchronizationContext;
		}

		// Token: 0x0600421D RID: 16925 RVA: 0x000F62AB File Offset: 0x000F44AB
		[SecurityCritical]
		protected internal override void QueueTask(Task task)
		{
			this.m_synchronizationContext.Post(SynchronizationContextTaskScheduler.s_postCallback, task);
		}

		// Token: 0x0600421E RID: 16926 RVA: 0x000F62BE File Offset: 0x000F44BE
		[SecurityCritical]
		protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
		{
			return SynchronizationContext.Current == this.m_synchronizationContext && base.TryExecuteTask(task);
		}

		// Token: 0x0600421F RID: 16927 RVA: 0x000F62D6 File Offset: 0x000F44D6
		[SecurityCritical]
		protected override IEnumerable<Task> GetScheduledTasks()
		{
			return null;
		}

		// Token: 0x170009CF RID: 2511
		// (get) Token: 0x06004220 RID: 16928 RVA: 0x000F62D9 File Offset: 0x000F44D9
		public override int MaximumConcurrencyLevel
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x06004221 RID: 16929 RVA: 0x000F62DC File Offset: 0x000F44DC
		private static void PostCallback(object obj)
		{
			Task task = (Task)obj;
			task.ExecuteEntry(true);
		}

		// Token: 0x06004222 RID: 16930 RVA: 0x000F62F8 File Offset: 0x000F44F8
		// Note: this type is marked as 'beforefieldinit'.
		static SynchronizationContextTaskScheduler()
		{
		}

		// Token: 0x04001B74 RID: 7028
		private SynchronizationContext m_synchronizationContext;

		// Token: 0x04001B75 RID: 7029
		private static SendOrPostCallback s_postCallback = new SendOrPostCallback(SynchronizationContextTaskScheduler.PostCallback);
	}
}
