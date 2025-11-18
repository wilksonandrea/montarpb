using System;
using Plugin.Core.Enums;

namespace Server.Match.Data.Models.SubHead
{
	// Token: 0x02000044 RID: 68
	public class GrenadeInfo
	{
		// Token: 0x060001AF RID: 431 RVA: 0x000020A2 File Offset: 0x000002A2
		public GrenadeInfo()
		{
		}

		// Token: 0x040000B6 RID: 182
		public byte Accessory;

		// Token: 0x040000B7 RID: 183
		public byte Extensions;

		// Token: 0x040000B8 RID: 184
		public byte Unk1;

		// Token: 0x040000B9 RID: 185
		public byte Unk2;

		// Token: 0x040000BA RID: 186
		public byte Unk3;

		// Token: 0x040000BB RID: 187
		public byte Unk4;

		// Token: 0x040000BC RID: 188
		public int WeaponId;

		// Token: 0x040000BD RID: 189
		public int Unk5;

		// Token: 0x040000BE RID: 190
		public int Unk6;

		// Token: 0x040000BF RID: 191
		public int Unk7;

		// Token: 0x040000C0 RID: 192
		public ClassType WeaponClass;

		// Token: 0x040000C1 RID: 193
		public ushort ObjPosX;

		// Token: 0x040000C2 RID: 194
		public ushort ObjPosY;

		// Token: 0x040000C3 RID: 195
		public ushort ObjPosZ;

		// Token: 0x040000C4 RID: 196
		public ushort BoomInfo;

		// Token: 0x040000C5 RID: 197
		public ushort GrenadesCount;
	}
}
