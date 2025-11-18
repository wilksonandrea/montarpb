using System;

namespace Plugin.Core.Enums
{
	// Token: 0x020000D9 RID: 217
	[Flags]
	public enum RoomStageFlag
	{
		// Token: 0x0400062E RID: 1582
		NONE = 0,
		// Token: 0x0400062F RID: 1583
		TEAM_SWAP = 1,
		// Token: 0x04000630 RID: 1584
		RANDOM_MAP = 2,
		// Token: 0x04000631 RID: 1585
		PASSWORD = 4,
		// Token: 0x04000632 RID: 1586
		OBSERVER_MODE = 8,
		// Token: 0x04000633 RID: 1587
		REAL_IP = 16,
		// Token: 0x04000634 RID: 1588
		TEAM_BALANCE = 32,
		// Token: 0x04000635 RID: 1589
		OBSERVER = 64,
		// Token: 0x04000636 RID: 1590
		INTER_ENTER = 128
	}
}
