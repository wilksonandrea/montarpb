using System;
using System.Collections.Generic;
using Plugin.Core.Enums;
using Plugin.Core.SharpDX;
using Server.Match.Data.Enums;

namespace Server.Match.Data.Models.Event
{
	// Token: 0x02000054 RID: 84
	public class GrenadeHitInfo
	{
		// Token: 0x060001BF RID: 447 RVA: 0x000020A2 File Offset: 0x000002A2
		public GrenadeHitInfo()
		{
		}

		// Token: 0x0400010A RID: 266
		public byte Extensions;

		// Token: 0x0400010B RID: 267
		public byte Accessory;

		// Token: 0x0400010C RID: 268
		public ushort BoomInfo;

		// Token: 0x0400010D RID: 269
		public ushort GrenadesCount;

		// Token: 0x0400010E RID: 270
		public ushort ObjectId;

		// Token: 0x0400010F RID: 271
		public uint HitInfo;

		// Token: 0x04000110 RID: 272
		public int WeaponId;

		// Token: 0x04000111 RID: 273
		public List<int> BoomPlayers;

		// Token: 0x04000112 RID: 274
		public CharaDeath DeathType;

		// Token: 0x04000113 RID: 275
		public Half3 FirePos;

		// Token: 0x04000114 RID: 276
		public Half3 HitPos;

		// Token: 0x04000115 RID: 277
		public Half3 PlayerPos;

		// Token: 0x04000116 RID: 278
		public HitType HitEnum;

		// Token: 0x04000117 RID: 279
		public ClassType WeaponClass;
	}
}
