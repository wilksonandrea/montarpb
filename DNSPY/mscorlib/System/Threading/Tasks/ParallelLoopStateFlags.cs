using System;

namespace System.Threading.Tasks
{
	// Token: 0x02000558 RID: 1368
	internal class ParallelLoopStateFlags
	{
		// Token: 0x1700098D RID: 2445
		// (get) Token: 0x06004060 RID: 16480 RVA: 0x000F00F4 File Offset: 0x000EE2F4
		internal int LoopStateFlags
		{
			get
			{
				return this.m_LoopStateFlags;
			}
		}

		// Token: 0x06004061 RID: 16481 RVA: 0x000F0100 File Offset: 0x000EE300
		internal bool AtomicLoopStateUpdate(int newState, int illegalStates)
		{
			int num = 0;
			return this.AtomicLoopStateUpdate(newState, illegalStates, ref num);
		}

		// Token: 0x06004062 RID: 16482 RVA: 0x000F011C File Offset: 0x000EE31C
		internal bool AtomicLoopStateUpdate(int newState, int illegalStates, ref int oldState)
		{
			SpinWait spinWait = default(SpinWait);
			for (;;)
			{
				oldState = this.m_LoopStateFlags;
				if ((oldState & illegalStates) != 0)
				{
					break;
				}
				if (Interlocked.CompareExchange(ref this.m_LoopStateFlags, oldState | newState, oldState) == oldState)
				{
					return true;
				}
				spinWait.SpinOnce();
			}
			return false;
		}

		// Token: 0x06004063 RID: 16483 RVA: 0x000F0162 File Offset: 0x000EE362
		internal void SetExceptional()
		{
			this.AtomicLoopStateUpdate(ParallelLoopStateFlags.PLS_EXCEPTIONAL, ParallelLoopStateFlags.PLS_NONE);
		}

		// Token: 0x06004064 RID: 16484 RVA: 0x000F0175 File Offset: 0x000EE375
		internal void Stop()
		{
			if (!this.AtomicLoopStateUpdate(ParallelLoopStateFlags.PLS_STOPPED, ParallelLoopStateFlags.PLS_BROKEN))
			{
				throw new InvalidOperationException(Environment.GetResourceString("ParallelState_Stop_InvalidOperationException_StopAfterBreak"));
			}
		}

		// Token: 0x06004065 RID: 16485 RVA: 0x000F0199 File Offset: 0x000EE399
		internal bool Cancel()
		{
			return this.AtomicLoopStateUpdate(ParallelLoopStateFlags.PLS_CANCELED, ParallelLoopStateFlags.PLS_NONE);
		}

		// Token: 0x06004066 RID: 16486 RVA: 0x000F01AB File Offset: 0x000EE3AB
		public ParallelLoopStateFlags()
		{
		}

		// Token: 0x06004067 RID: 16487 RVA: 0x000F01C0 File Offset: 0x000EE3C0
		// Note: this type is marked as 'beforefieldinit'.
		static ParallelLoopStateFlags()
		{
		}

		// Token: 0x04001AE4 RID: 6884
		internal static int PLS_NONE;

		// Token: 0x04001AE5 RID: 6885
		internal static int PLS_EXCEPTIONAL = 1;

		// Token: 0x04001AE6 RID: 6886
		internal static int PLS_BROKEN = 2;

		// Token: 0x04001AE7 RID: 6887
		internal static int PLS_STOPPED = 4;

		// Token: 0x04001AE8 RID: 6888
		internal static int PLS_CANCELED = 8;

		// Token: 0x04001AE9 RID: 6889
		private volatile int m_LoopStateFlags = ParallelLoopStateFlags.PLS_NONE;
	}
}
