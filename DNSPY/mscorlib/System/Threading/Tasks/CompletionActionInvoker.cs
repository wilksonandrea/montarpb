using System;
using System.Security;

namespace System.Threading.Tasks
{
	// Token: 0x0200055F RID: 1375
	internal sealed class CompletionActionInvoker : IThreadPoolWorkItem
	{
		// Token: 0x06004149 RID: 16713 RVA: 0x000F3B05 File Offset: 0x000F1D05
		internal CompletionActionInvoker(ITaskCompletionAction action, Task completingTask)
		{
			this.m_action = action;
			this.m_completingTask = completingTask;
		}

		// Token: 0x0600414A RID: 16714 RVA: 0x000F3B1B File Offset: 0x000F1D1B
		[SecurityCritical]
		public void ExecuteWorkItem()
		{
			this.m_action.Invoke(this.m_completingTask);
		}

		// Token: 0x0600414B RID: 16715 RVA: 0x000F3B2E File Offset: 0x000F1D2E
		[SecurityCritical]
		public void MarkAborted(ThreadAbortException tae)
		{
		}

		// Token: 0x04001B20 RID: 6944
		private readonly ITaskCompletionAction m_action;

		// Token: 0x04001B21 RID: 6945
		private readonly Task m_completingTask;
	}
}
