using System;

namespace System.Threading.Tasks
{
	// Token: 0x0200055A RID: 1370
	internal class ParallelLoopStateFlags64 : ParallelLoopStateFlags
	{
		// Token: 0x17000990 RID: 2448
		// (get) Token: 0x0600406D RID: 16493 RVA: 0x000F02BF File Offset: 0x000EE4BF
		internal long LowestBreakIteration
		{
			get
			{
				if (IntPtr.Size >= 8)
				{
					return this.m_lowestBreakIteration;
				}
				return Interlocked.Read(ref this.m_lowestBreakIteration);
			}
		}

		// Token: 0x17000991 RID: 2449
		// (get) Token: 0x0600406E RID: 16494 RVA: 0x000F02DC File Offset: 0x000EE4DC
		internal long? NullableLowestBreakIteration
		{
			get
			{
				if (this.m_lowestBreakIteration == 9223372036854775807L)
				{
					return null;
				}
				if (IntPtr.Size >= 8)
				{
					return new long?(this.m_lowestBreakIteration);
				}
				return new long?(Interlocked.Read(ref this.m_lowestBreakIteration));
			}
		}

		// Token: 0x0600406F RID: 16495 RVA: 0x000F0328 File Offset: 0x000EE528
		internal bool ShouldExitLoop(long CallerIteration)
		{
			int loopStateFlags = base.LoopStateFlags;
			return loopStateFlags != ParallelLoopStateFlags.PLS_NONE && ((loopStateFlags & (ParallelLoopStateFlags.PLS_EXCEPTIONAL | ParallelLoopStateFlags.PLS_STOPPED | ParallelLoopStateFlags.PLS_CANCELED)) != 0 || ((loopStateFlags & ParallelLoopStateFlags.PLS_BROKEN) != 0 && CallerIteration > this.LowestBreakIteration));
		}

		// Token: 0x06004070 RID: 16496 RVA: 0x000F0374 File Offset: 0x000EE574
		internal bool ShouldExitLoop()
		{
			int loopStateFlags = base.LoopStateFlags;
			return loopStateFlags != ParallelLoopStateFlags.PLS_NONE && (loopStateFlags & (ParallelLoopStateFlags.PLS_EXCEPTIONAL | ParallelLoopStateFlags.PLS_CANCELED)) != 0;
		}

		// Token: 0x06004071 RID: 16497 RVA: 0x000F03A2 File Offset: 0x000EE5A2
		public ParallelLoopStateFlags64()
		{
		}

		// Token: 0x04001AEB RID: 6891
		internal long m_lowestBreakIteration = long.MaxValue;
	}
}
