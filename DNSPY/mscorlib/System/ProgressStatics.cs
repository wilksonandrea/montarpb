using System;
using System.Threading;

namespace System
{
	// Token: 0x02000125 RID: 293
	internal static class ProgressStatics
	{
		// Token: 0x060010F7 RID: 4343 RVA: 0x0003306B File Offset: 0x0003126B
		// Note: this type is marked as 'beforefieldinit'.
		static ProgressStatics()
		{
		}

		// Token: 0x040005EF RID: 1519
		internal static readonly SynchronizationContext DefaultContext = new SynchronizationContext();
	}
}
