using System;

namespace System.Threading.Tasks
{
	// Token: 0x02000559 RID: 1369
	internal class ParallelLoopStateFlags32 : ParallelLoopStateFlags
	{
		// Token: 0x1700098E RID: 2446
		// (get) Token: 0x06004068 RID: 16488 RVA: 0x000F01DA File Offset: 0x000EE3DA
		internal int LowestBreakIteration
		{
			get
			{
				return this.m_lowestBreakIteration;
			}
		}

		// Token: 0x1700098F RID: 2447
		// (get) Token: 0x06004069 RID: 16489 RVA: 0x000F01E4 File Offset: 0x000EE3E4
		internal long? NullableLowestBreakIteration
		{
			get
			{
				if (this.m_lowestBreakIteration == 2147483647)
				{
					return null;
				}
				long num = (long)this.m_lowestBreakIteration;
				if (IntPtr.Size >= 8)
				{
					return new long?(num);
				}
				return new long?(Interlocked.Read(ref num));
			}
		}

		// Token: 0x0600406A RID: 16490 RVA: 0x000F0230 File Offset: 0x000EE430
		internal bool ShouldExitLoop(int CallerIteration)
		{
			int loopStateFlags = base.LoopStateFlags;
			return loopStateFlags != ParallelLoopStateFlags.PLS_NONE && ((loopStateFlags & (ParallelLoopStateFlags.PLS_EXCEPTIONAL | ParallelLoopStateFlags.PLS_STOPPED | ParallelLoopStateFlags.PLS_CANCELED)) != 0 || ((loopStateFlags & ParallelLoopStateFlags.PLS_BROKEN) != 0 && CallerIteration > this.LowestBreakIteration));
		}

		// Token: 0x0600406B RID: 16491 RVA: 0x000F027C File Offset: 0x000EE47C
		internal bool ShouldExitLoop()
		{
			int loopStateFlags = base.LoopStateFlags;
			return loopStateFlags != ParallelLoopStateFlags.PLS_NONE && (loopStateFlags & (ParallelLoopStateFlags.PLS_EXCEPTIONAL | ParallelLoopStateFlags.PLS_CANCELED)) != 0;
		}

		// Token: 0x0600406C RID: 16492 RVA: 0x000F02AA File Offset: 0x000EE4AA
		public ParallelLoopStateFlags32()
		{
		}

		// Token: 0x04001AEA RID: 6890
		internal volatile int m_lowestBreakIteration = int.MaxValue;
	}
}
