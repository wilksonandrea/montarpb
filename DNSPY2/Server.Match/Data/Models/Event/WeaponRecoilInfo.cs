using System;

namespace Server.Match.Data.Models.Event
{
	// Token: 0x0200004C RID: 76
	public class WeaponRecoilInfo
	{
		// Token: 0x060001B7 RID: 439 RVA: 0x000020A2 File Offset: 0x000002A2
		public WeaponRecoilInfo()
		{
		}

		// Token: 0x040000DC RID: 220
		public float RecoilHorzAngle;

		// Token: 0x040000DD RID: 221
		public float RecoilHorzMax;

		// Token: 0x040000DE RID: 222
		public float RecoilVertAngle;

		// Token: 0x040000DF RID: 223
		public float RecoilVertMax;

		// Token: 0x040000E0 RID: 224
		public float Deviation;

		// Token: 0x040000E1 RID: 225
		public int WeaponId;

		// Token: 0x040000E2 RID: 226
		public byte Extensions;

		// Token: 0x040000E3 RID: 227
		public byte Accessory;

		// Token: 0x040000E4 RID: 228
		public byte RecoilHorzCount;
	}
}
