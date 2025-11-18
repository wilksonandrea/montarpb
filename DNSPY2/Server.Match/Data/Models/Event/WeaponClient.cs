using System;

namespace Server.Match.Data.Models.Event
{
	// Token: 0x0200005B RID: 91
	public class WeaponClient
	{
		// Token: 0x060001C6 RID: 454 RVA: 0x000020A2 File Offset: 0x000002A2
		public WeaponClient()
		{
		}

		// Token: 0x04000139 RID: 313
		public byte Flags;

		// Token: 0x0400013A RID: 314
		public byte Extensions;

		// Token: 0x0400013B RID: 315
		public byte Accessory;

		// Token: 0x0400013C RID: 316
		public ushort AmmoPrin;

		// Token: 0x0400013D RID: 317
		public ushort AmmoDual;

		// Token: 0x0400013E RID: 318
		public ushort AmmoTotal;

		// Token: 0x0400013F RID: 319
		public ushort Unk1;

		// Token: 0x04000140 RID: 320
		public ushort Unk2;

		// Token: 0x04000141 RID: 321
		public ushort Unk3;

		// Token: 0x04000142 RID: 322
		public int WeaponId;
	}
}
