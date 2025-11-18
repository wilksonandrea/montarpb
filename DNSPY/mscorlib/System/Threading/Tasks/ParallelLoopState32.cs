using System;

namespace System.Threading.Tasks
{
	// Token: 0x02000556 RID: 1366
	internal class ParallelLoopState32 : ParallelLoopState
	{
		// Token: 0x06004054 RID: 16468 RVA: 0x000F004C File Offset: 0x000EE24C
		internal ParallelLoopState32(ParallelLoopStateFlags32 sharedParallelStateFlags)
			: base(sharedParallelStateFlags)
		{
			this.m_sharedParallelStateFlags = sharedParallelStateFlags;
		}

		// Token: 0x17000987 RID: 2439
		// (get) Token: 0x06004055 RID: 16469 RVA: 0x000F005C File Offset: 0x000EE25C
		// (set) Token: 0x06004056 RID: 16470 RVA: 0x000F0064 File Offset: 0x000EE264
		internal int CurrentIteration
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

		// Token: 0x17000988 RID: 2440
		// (get) Token: 0x06004057 RID: 16471 RVA: 0x000F006D File Offset: 0x000EE26D
		internal override bool InternalShouldExitCurrentIteration
		{
			get
			{
				return this.m_sharedParallelStateFlags.ShouldExitLoop(this.CurrentIteration);
			}
		}

		// Token: 0x17000989 RID: 2441
		// (get) Token: 0x06004058 RID: 16472 RVA: 0x000F0080 File Offset: 0x000EE280
		internal override long? InternalLowestBreakIteration
		{
			get
			{
				return this.m_sharedParallelStateFlags.NullableLowestBreakIteration;
			}
		}

		// Token: 0x06004059 RID: 16473 RVA: 0x000F008D File Offset: 0x000EE28D
		internal override void InternalBreak()
		{
			ParallelLoopState.Break(this.CurrentIteration, this.m_sharedParallelStateFlags);
		}

		// Token: 0x04001AE0 RID: 6880
		private ParallelLoopStateFlags32 m_sharedParallelStateFlags;

		// Token: 0x04001AE1 RID: 6881
		private int m_currentIteration;
	}
}
