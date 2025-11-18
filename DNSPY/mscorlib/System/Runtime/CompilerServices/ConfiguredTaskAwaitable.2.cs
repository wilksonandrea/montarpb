using System;
using System.Security;
using System.Security.Permissions;
using System.Threading.Tasks;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020008FB RID: 2299
	[__DynamicallyInvokable]
	public struct ConfiguredTaskAwaitable<TResult>
	{
		// Token: 0x06005E53 RID: 24147 RVA: 0x0014B2ED File Offset: 0x001494ED
		internal ConfiguredTaskAwaitable(Task<TResult> task, bool continueOnCapturedContext)
		{
			this.m_configuredTaskAwaiter = new ConfiguredTaskAwaitable<TResult>.ConfiguredTaskAwaiter(task, continueOnCapturedContext);
		}

		// Token: 0x06005E54 RID: 24148 RVA: 0x0014B2FC File Offset: 0x001494FC
		[__DynamicallyInvokable]
		public ConfiguredTaskAwaitable<TResult>.ConfiguredTaskAwaiter GetAwaiter()
		{
			return this.m_configuredTaskAwaiter;
		}

		// Token: 0x04002A60 RID: 10848
		private readonly ConfiguredTaskAwaitable<TResult>.ConfiguredTaskAwaiter m_configuredTaskAwaiter;

		// Token: 0x02000C97 RID: 3223
		[__DynamicallyInvokable]
		[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
		public struct ConfiguredTaskAwaiter : ICriticalNotifyCompletion, INotifyCompletion
		{
			// Token: 0x0600710A RID: 28938 RVA: 0x00184F8E File Offset: 0x0018318E
			internal ConfiguredTaskAwaiter(Task<TResult> task, bool continueOnCapturedContext)
			{
				this.m_task = task;
				this.m_continueOnCapturedContext = continueOnCapturedContext;
			}

			// Token: 0x17001361 RID: 4961
			// (get) Token: 0x0600710B RID: 28939 RVA: 0x00184F9E File Offset: 0x0018319E
			[__DynamicallyInvokable]
			public bool IsCompleted
			{
				[__DynamicallyInvokable]
				get
				{
					return this.m_task.IsCompleted;
				}
			}

			// Token: 0x0600710C RID: 28940 RVA: 0x00184FAB File Offset: 0x001831AB
			[SecuritySafeCritical]
			[__DynamicallyInvokable]
			public void OnCompleted(Action continuation)
			{
				TaskAwaiter.OnCompletedInternal(this.m_task, continuation, this.m_continueOnCapturedContext, true);
			}

			// Token: 0x0600710D RID: 28941 RVA: 0x00184FC0 File Offset: 0x001831C0
			[SecurityCritical]
			[__DynamicallyInvokable]
			public void UnsafeOnCompleted(Action continuation)
			{
				TaskAwaiter.OnCompletedInternal(this.m_task, continuation, this.m_continueOnCapturedContext, false);
			}

			// Token: 0x0600710E RID: 28942 RVA: 0x00184FD5 File Offset: 0x001831D5
			[__DynamicallyInvokable]
			public TResult GetResult()
			{
				TaskAwaiter.ValidateEnd(this.m_task);
				return this.m_task.ResultOnSuccess;
			}

			// Token: 0x04003858 RID: 14424
			private readonly Task<TResult> m_task;

			// Token: 0x04003859 RID: 14425
			private readonly bool m_continueOnCapturedContext;
		}
	}
}
