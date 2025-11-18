using System;
using System.Security;
using System.Security.Permissions;
using System.Threading.Tasks;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020008F9 RID: 2297
	[__DynamicallyInvokable]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	public struct TaskAwaiter<TResult> : ICriticalNotifyCompletion, INotifyCompletion
	{
		// Token: 0x06005E4C RID: 24140 RVA: 0x0014B288 File Offset: 0x00149488
		internal TaskAwaiter(Task<TResult> task)
		{
			this.m_task = task;
		}

		// Token: 0x1700102F RID: 4143
		// (get) Token: 0x06005E4D RID: 24141 RVA: 0x0014B291 File Offset: 0x00149491
		[__DynamicallyInvokable]
		public bool IsCompleted
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_task.IsCompleted;
			}
		}

		// Token: 0x06005E4E RID: 24142 RVA: 0x0014B29E File Offset: 0x0014949E
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public void OnCompleted(Action continuation)
		{
			TaskAwaiter.OnCompletedInternal(this.m_task, continuation, true, true);
		}

		// Token: 0x06005E4F RID: 24143 RVA: 0x0014B2AE File Offset: 0x001494AE
		[SecurityCritical]
		[__DynamicallyInvokable]
		public void UnsafeOnCompleted(Action continuation)
		{
			TaskAwaiter.OnCompletedInternal(this.m_task, continuation, true, false);
		}

		// Token: 0x06005E50 RID: 24144 RVA: 0x0014B2BE File Offset: 0x001494BE
		[__DynamicallyInvokable]
		public TResult GetResult()
		{
			TaskAwaiter.ValidateEnd(this.m_task);
			return this.m_task.ResultOnSuccess;
		}

		// Token: 0x04002A5E RID: 10846
		private readonly Task<TResult> m_task;
	}
}
