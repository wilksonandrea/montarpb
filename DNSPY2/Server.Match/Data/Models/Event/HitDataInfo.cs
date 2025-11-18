using System;
using System.Collections.Generic;
using Plugin.Core.Enums;
using Plugin.Core.SharpDX;
using Server.Match.Data.Enums;

namespace Server.Match.Data.Models.Event
{
	// Token: 0x02000055 RID: 85
	public class HitDataInfo
	{
		// Token: 0x060001C0 RID: 448 RVA: 0x000020A2 File Offset: 0x000002A2
		public HitDataInfo()
		{
		}

		// Token: 0x04000118 RID: 280
		public byte Extensions;

		// Token: 0x04000119 RID: 281
		public byte Accessory;

		// Token: 0x0400011A RID: 282
		public ushort BoomInfo;

		// Token: 0x0400011B RID: 283
		public ushort ObjectId;

		// Token: 0x0400011C RID: 284
		public uint HitIndex;

		// Token: 0x0400011D RID: 285
		public int WeaponId;

		// Token: 0x0400011E RID: 286
		public Half3 StartBullet;

		// Token: 0x0400011F RID: 287
		public Half3 EndBullet;

		// Token: 0x04000120 RID: 288
		public Half3 BulletPos;

		// Token: 0x04000121 RID: 289
		public List<int> BoomPlayers;

		// Token: 0x04000122 RID: 290
		public HitType HitEnum;

		// Token: 0x04000123 RID: 291
		public ClassType WeaponClass;
	}
}
