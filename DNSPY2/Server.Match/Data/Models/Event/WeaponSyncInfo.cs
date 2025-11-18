using System;
using Plugin.Core.Enums;

namespace Server.Match.Data.Models.Event
{
	// Token: 0x0200005D RID: 93
	public class WeaponSyncInfo
	{
		// Token: 0x060001C8 RID: 456 RVA: 0x000020A2 File Offset: 0x000002A2
		public WeaponSyncInfo()
		{
		}

		// Token: 0x0400014A RID: 330
		public int WeaponId;

		// Token: 0x0400014B RID: 331
		public byte Accessory;

		// Token: 0x0400014C RID: 332
		public byte Extensions;

		// Token: 0x0400014D RID: 333
		public ClassType WeaponClass;
	}
}
