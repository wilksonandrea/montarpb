using System;

namespace Server.Match.Data.Enums
{
	// Token: 0x02000066 RID: 102
	[Flags]
	public enum CharaMoves
	{
		// Token: 0x040001BE RID: 446
		Stop = 0,
		// Token: 0x040001BF RID: 447
		Left = 1,
		// Token: 0x040001C0 RID: 448
		Back = 2,
		// Token: 0x040001C1 RID: 449
		Right = 4,
		// Token: 0x040001C2 RID: 450
		Front = 8,
		// Token: 0x040001C3 RID: 451
		HeliInMove = 16,
		// Token: 0x040001C4 RID: 452
		HeliUnknown = 32,
		// Token: 0x040001C5 RID: 453
		HeliLeave = 64,
		// Token: 0x040001C6 RID: 454
		HeliStopped = 128
	}
}
