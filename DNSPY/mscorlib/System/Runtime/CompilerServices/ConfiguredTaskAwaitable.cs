using System;
using System.Security;
using System.Security.Permissions;
using System.Threading.Tasks;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020008FA RID: 2298
	[__DynamicallyInvokable]
	public struct ConfiguredTaskAwaitable
	{
		// Token: 0x06005E51 RID: 24145 RVA: 0x0014B2D6 File Offset: 0x001494D6
		internal ConfiguredTaskAwaitable(Task task, bool continueOnCapturedContext)
		{
			this.m_configuredTaskAwaiter = new ConfiguredTaskAwaitable.ConfiguredTaskAwaiter(task, continueOnCapturedContext);
		}

		// Token: 0x06005E52 RID: 24146 RVA: 0x0014B2E5 File Offset: 0x001494E5
		[__DynamicallyInvokable]
		public ConfiguredTaskAwaitable.ConfiguredTaskAwaiter GetAwaiter()
		{
			return this.m_configuredTaskAwaiter;
		}

		// Token: 0x04002A5F RID: 10847
		private readonly ConfiguredTaskAwaitable.ConfiguredTaskAwaiter m_configuredTaskAwaiter;

		// Token: 0x02000C96 RID: 3222
		[__DynamicallyInvokable]
		[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
		public struct ConfiguredTaskAwaiter : ICriticalNotifyCompletion, INotifyCompletion
		{
			// Token: 0x06007105 RID: 28933 RVA: 0x00184F3A File Offset: 0x0018313A
			internal ConfiguredTaskAwaiter(Task task, bool continueOnCapturedContext)
			{
				this.m_task = task;
				this.m_continueOnCapturedContext = continueOnCapturedContext;
			}

			// Token: 0x17001360 RID: 4960
			// (get) Token: 0x06007106 RID: 28934 RVA: 0x00184F4A File Offset: 0x0018314A
			[__DynamicallyInvokable]
			public bool IsCompleted
			{
				[__DynamicallyInvokable]
				get
				{
					return this.m_task.IsCompleted;
				}
			}

			// Token: 0x06007107 RID: 28935 RVA: 0x00184F57 File Offset: 0x00183157
			[SecuritySafeCritical]
			[__DynamicallyInvokable]
			public void OnCompleted(Action continuation)
			{
				TaskAwaiter.OnCompletedInternal(this.m_task, continuation, this.m_continueOnCapturedContext, true);
			}

			// Token: 0x06007108 RID: 28936 RVA: 0x00184F6C File Offset: 0x0018316C
			[SecurityCritical]
			[__DynamicallyInvokable]
			public void UnsafeOnCompleted(Action continuation)
			{
				TaskAwaiter.OnCompletedInternal(this.m_task, continuation, this.m_continueOnCapturedContext, false);
			}

			// Token: 0x06007109 RID: 28937 RVA: 0x00184F81 File Offset: 0x00183181
			[__DynamicallyInvokable]
			public void GetResult()
			{
				TaskAwaiter.ValidateEnd(this.m_task);
			}

			// Token: 0x04003856 RID: 14422
			private readonly Task m_task;

			// Token: 0x04003857 RID: 14423
			private readonly bool m_continueOnCapturedContext;
		}
	}
}
