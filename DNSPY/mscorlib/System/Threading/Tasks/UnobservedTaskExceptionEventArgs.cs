using System;

namespace System.Threading.Tasks
{
	// Token: 0x02000579 RID: 1401
	[__DynamicallyInvokable]
	public class UnobservedTaskExceptionEventArgs : EventArgs
	{
		// Token: 0x06004223 RID: 16931 RVA: 0x000F630B File Offset: 0x000F450B
		[__DynamicallyInvokable]
		public UnobservedTaskExceptionEventArgs(AggregateException exception)
		{
			this.m_exception = exception;
		}

		// Token: 0x06004224 RID: 16932 RVA: 0x000F631A File Offset: 0x000F451A
		[__DynamicallyInvokable]
		public void SetObserved()
		{
			this.m_observed = true;
		}

		// Token: 0x170009D0 RID: 2512
		// (get) Token: 0x06004225 RID: 16933 RVA: 0x000F6323 File Offset: 0x000F4523
		[__DynamicallyInvokable]
		public bool Observed
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_observed;
			}
		}

		// Token: 0x170009D1 RID: 2513
		// (get) Token: 0x06004226 RID: 16934 RVA: 0x000F632B File Offset: 0x000F452B
		[__DynamicallyInvokable]
		public AggregateException Exception
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_exception;
			}
		}

		// Token: 0x04001B76 RID: 7030
		private AggregateException m_exception;

		// Token: 0x04001B77 RID: 7031
		internal bool m_observed;
	}
}
