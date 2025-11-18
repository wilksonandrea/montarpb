using System;

namespace System.Threading.Tasks
{
	// Token: 0x0200055B RID: 1371
	[__DynamicallyInvokable]
	public struct ParallelLoopResult
	{
		// Token: 0x17000992 RID: 2450
		// (get) Token: 0x06004072 RID: 16498 RVA: 0x000F03B9 File Offset: 0x000EE5B9
		[__DynamicallyInvokable]
		public bool IsCompleted
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_completed;
			}
		}

		// Token: 0x17000993 RID: 2451
		// (get) Token: 0x06004073 RID: 16499 RVA: 0x000F03C1 File Offset: 0x000EE5C1
		[__DynamicallyInvokable]
		public long? LowestBreakIteration
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_lowestBreakIteration;
			}
		}

		// Token: 0x04001AEC RID: 6892
		internal bool m_completed;

		// Token: 0x04001AED RID: 6893
		internal long? m_lowestBreakIteration;
	}
}
