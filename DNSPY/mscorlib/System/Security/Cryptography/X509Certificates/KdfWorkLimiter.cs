using System;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020002BB RID: 699
	internal static class KdfWorkLimiter
	{
		// Token: 0x060024FC RID: 9468 RVA: 0x0008599C File Offset: 0x00083B9C
		internal static void SetIterationLimit(ulong workLimit)
		{
			KdfWorkLimiter.t_State = new KdfWorkLimiter.State
			{
				RemainingAllowedWork = workLimit
			};
		}

		// Token: 0x060024FD RID: 9469 RVA: 0x000859BC File Offset: 0x00083BBC
		internal static bool WasWorkLimitExceeded()
		{
			return KdfWorkLimiter.t_State.WorkLimitWasExceeded;
		}

		// Token: 0x060024FE RID: 9470 RVA: 0x000859C8 File Offset: 0x00083BC8
		internal static void ResetIterationLimit()
		{
			KdfWorkLimiter.t_State = null;
		}

		// Token: 0x060024FF RID: 9471 RVA: 0x000859D0 File Offset: 0x00083BD0
		internal static void RecordIterations(int workCount)
		{
			KdfWorkLimiter.RecordIterations((long)workCount);
		}

		// Token: 0x06002500 RID: 9472 RVA: 0x000859DC File Offset: 0x00083BDC
		internal static void RecordIterations(long workCount)
		{
			KdfWorkLimiter.State state = KdfWorkLimiter.t_State;
			bool flag = false;
			checked
			{
				try
				{
					if (!state.WorkLimitWasExceeded)
					{
						state.RemainingAllowedWork -= (ulong)workCount;
						flag = true;
					}
				}
				finally
				{
					if (!flag)
					{
						state.RemainingAllowedWork = 0UL;
						state.WorkLimitWasExceeded = true;
						throw new CryptographicException();
					}
				}
			}
		}

		// Token: 0x04000DCE RID: 3534
		[ThreadStatic]
		private static KdfWorkLimiter.State t_State;

		// Token: 0x02000B52 RID: 2898
		private sealed class State
		{
			// Token: 0x06006BB2 RID: 27570 RVA: 0x0017479F File Offset: 0x0017299F
			public State()
			{
			}

			// Token: 0x040033E6 RID: 13286
			internal ulong RemainingAllowedWork;

			// Token: 0x040033E7 RID: 13287
			internal bool WorkLimitWasExceeded;
		}
	}
}
