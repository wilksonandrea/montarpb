using System;
using Plugin.Core.Enums;
using Plugin.Core.SharpDX;

namespace Server.Match.Data.Models.Event
{
	// Token: 0x02000059 RID: 89
	public class SuicideInfo
	{
		// Token: 0x060001C4 RID: 452 RVA: 0x000020A2 File Offset: 0x000002A2
		public SuicideInfo()
		{
		}

		// Token: 0x04000130 RID: 304
		public uint HitInfo;

		// Token: 0x04000131 RID: 305
		public Half3 PlayerPos;

		// Token: 0x04000132 RID: 306
		public ClassType WeaponClass;

		// Token: 0x04000133 RID: 307
		public byte Extensions;

		// Token: 0x04000134 RID: 308
		public byte Accessory;

		// Token: 0x04000135 RID: 309
		public int WeaponId;
	}
}
