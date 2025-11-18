using System;

namespace Server.Match.Data.Models.Event
{
	// Token: 0x02000050 RID: 80
	public class DropWeaponInfo
	{
		// Token: 0x060001BB RID: 443 RVA: 0x000020A2 File Offset: 0x000002A2
		public DropWeaponInfo()
		{
		}

		// Token: 0x040000F2 RID: 242
		public byte Flags;

		// Token: 0x040000F3 RID: 243
		public byte Extensions;

		// Token: 0x040000F4 RID: 244
		public byte Accessory;

		// Token: 0x040000F5 RID: 245
		public byte Counter;

		// Token: 0x040000F6 RID: 246
		public ushort AmmoPrin;

		// Token: 0x040000F7 RID: 247
		public ushort AmmoDual;

		// Token: 0x040000F8 RID: 248
		public ushort AmmoTotal;

		// Token: 0x040000F9 RID: 249
		public ushort Unk1;

		// Token: 0x040000FA RID: 250
		public ushort Unk2;

		// Token: 0x040000FB RID: 251
		public ushort Unk3;

		// Token: 0x040000FC RID: 252
		public int WeaponId;
	}
}
