using System;

namespace Plugin.Core.Enums
{
	// Token: 0x020000D2 RID: 210
	[Flags]
	public enum KillingMessage
	{
		// Token: 0x04000555 RID: 1365
		None = 0,
		// Token: 0x04000556 RID: 1366
		PiercingShot = 1,
		// Token: 0x04000557 RID: 1367
		MassKill = 2,
		// Token: 0x04000558 RID: 1368
		ChainStopper = 4,
		// Token: 0x04000559 RID: 1369
		Headshot = 8,
		// Token: 0x0400055A RID: 1370
		ChainHeadshot = 16,
		// Token: 0x0400055B RID: 1371
		ChainSlugger = 32,
		// Token: 0x0400055C RID: 1372
		Suicide = 64,
		// Token: 0x0400055D RID: 1373
		ObjectDefense = 128
	}
}
