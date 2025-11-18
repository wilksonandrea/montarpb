using System;

namespace System.Threading.Tasks
{
	// Token: 0x02000557 RID: 1367
	internal class ParallelLoopState64 : ParallelLoopState
	{
		// Token: 0x0600405A RID: 16474 RVA: 0x000F00A0 File Offset: 0x000EE2A0
		internal ParallelLoopState64(ParallelLoopStateFlags64 sharedParallelStateFlags)
			: base(sharedParallelStateFlags)
		{
			this.m_sharedParallelStateFlags = sharedParallelStateFlags;
		}

		// Token: 0x1700098A RID: 2442
		// (get) Token: 0x0600405B RID: 16475 RVA: 0x000F00B0 File Offset: 0x000EE2B0
		// (set) Token: 0x0600405C RID: 16476 RVA: 0x000F00B8 File Offset: 0x000EE2B8
		internal long CurrentIteration
		{
			get
			{
				return this.m_currentIteration;
			}
			set
			{
				this.m_currentIteration = value;
			}
		}

		// Token: 0x1700098B RID: 2443
		// (get) Token: 0x0600405D RID: 16477 RVA: 0x000F00C1 File Offset: 0x000EE2C1
		internal override bool InternalShouldExitCurrentIteration
		{
			get
			{
				return this.m_sharedParallelStateFlags.ShouldExitLoop(this.CurrentIteration);
			}
		}

		// Token: 0x1700098C RID: 2444
		// (get) Token: 0x0600405E RID: 16478 RVA: 0x000F00D4 File Offset: 0x000EE2D4
		internal override long? InternalLowestBreakIteration
		{
			get
			{
				return this.m_sharedParallelStateFlags.NullableLowestBreakIteration;
			}
		}

		// Token: 0x0600405F RID: 16479 RVA: 0x000F00E1 File Offset: 0x000EE2E1
		internal override void InternalBreak()
		{
			ParallelLoopState.Break(this.CurrentIteration, this.m_sharedParallelStateFlags);
		}

		// Token: 0x04001AE2 RID: 6882
		private ParallelLoopStateFlags64 m_sharedParallelStateFlags;

		// Token: 0x04001AE3 RID: 6883
		private long m_currentIteration;
	}
}
