using System;
using Plugin.Core.Enums;

namespace Server.Match.Data.Models.Event
{
	// Token: 0x0200004F RID: 79
	public class CharaFireNHitDataInfo
	{
		// Token: 0x060001BA RID: 442 RVA: 0x000020A2 File Offset: 0x000002A2
		public CharaFireNHitDataInfo()
		{
		}

		// Token: 0x040000E9 RID: 233
		public byte Extensions;

		// Token: 0x040000EA RID: 234
		public byte Accessory;

		// Token: 0x040000EB RID: 235
		public ushort X;

		// Token: 0x040000EC RID: 236
		public ushort Y;

		// Token: 0x040000ED RID: 237
		public ushort Z;

		// Token: 0x040000EE RID: 238
		public uint HitInfo;

		// Token: 0x040000EF RID: 239
		public int WeaponId;

		// Token: 0x040000F0 RID: 240
		public short Unk;

		// Token: 0x040000F1 RID: 241
		public ClassType WeaponClass;
	}
}
