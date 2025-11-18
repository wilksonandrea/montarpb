using System;

namespace Server.Match.Data.Models.Event
{
	// Token: 0x02000051 RID: 81
	public class FireDataInfo
	{
		// Token: 0x060001BC RID: 444 RVA: 0x000020A2 File Offset: 0x000002A2
		public FireDataInfo()
		{
		}

		// Token: 0x040000FD RID: 253
		public byte Effect;

		// Token: 0x040000FE RID: 254
		public byte Part;

		// Token: 0x040000FF RID: 255
		public byte Extensions;

		// Token: 0x04000100 RID: 256
		public byte Accessory;

		// Token: 0x04000101 RID: 257
		public short Index;

		// Token: 0x04000102 RID: 258
		public ushort X;

		// Token: 0x04000103 RID: 259
		public ushort Y;

		// Token: 0x04000104 RID: 260
		public ushort Z;

		// Token: 0x04000105 RID: 261
		public int WeaponId;
	}
}
