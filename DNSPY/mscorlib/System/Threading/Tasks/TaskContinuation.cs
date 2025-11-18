using System;
using System.Security;

namespace System.Threading.Tasks
{
	// Token: 0x0200056E RID: 1390
	internal abstract class TaskContinuation
	{
		// Token: 0x06004171 RID: 16753
		internal abstract void Run(Task completedTask, bool bCanInlineContinuationTask);

		// Token: 0x06004172 RID: 16754 RVA: 0x000F4298 File Offset: 0x000F2498
		[SecuritySafeCritical]
		protected static void InlineIfPossibleOrElseQueue(Task task, bool needsProtection)
		{
			if (needsProtection)
			{
				if (!task.MarkStarted())
				{
					return;
				}
			}
			else
			{
				task.m_stateFlags |= 65536;
			}
			try
			{
				if (!task.m_taskScheduler.TryRunInline(task, false))
				{
					task.m_taskScheduler.InternalQueueTask(task);
				}
			}
			catch (Exception ex)
			{
				if (!(ex is ThreadAbortException) || (task.m_stateFlags & 134217728) == 0)
				{
					TaskSchedulerException ex2 = new TaskSchedulerException(ex);
					task.AddException(ex2);
					task.Finish(false);
				}
			}
		}

		// Token: 0x06004173 RID: 16755
		internal abstract Delegate[] GetDelegateContinuationsForDebugger();

		// Token: 0x06004174 RID: 16756 RVA: 0x000F4328 File Offset: 0x000F2528
		protected TaskContinuation()
		{
		}
	}
}
