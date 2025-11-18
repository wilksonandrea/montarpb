using System;
using Plugin.Core.SharpDX;

namespace Server.Match.Data.Models.SubHead
{
	// Token: 0x02000043 RID: 67
	public class DropedWeaponInfo
	{
		// Token: 0x060001AE RID: 430 RVA: 0x000020A2 File Offset: 0x000002A2
		public DropedWeaponInfo()
		{
		}

		// Token: 0x040000AF RID: 175
		public byte WeaponFlag;

		// Token: 0x040000B0 RID: 176
		public ushort Unk1;

		// Token: 0x040000B1 RID: 177
		public ushort Unk2;

		// Token: 0x040000B2 RID: 178
		public ushort Unk3;

		// Token: 0x040000B3 RID: 179
		public ushort Unk4;

		// Token: 0x040000B4 RID: 180
		public Half3 WeaponPos;

		// Token: 0x040000B5 RID: 181
		public byte[] Unks;
	}
}
