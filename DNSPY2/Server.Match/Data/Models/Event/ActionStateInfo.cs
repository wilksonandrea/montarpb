using System;
using Server.Match.Data.Enums;

namespace Server.Match.Data.Models.Event
{
	// Token: 0x0200004D RID: 77
	public class ActionStateInfo
	{
		// Token: 0x060001B8 RID: 440 RVA: 0x000020A2 File Offset: 0x000002A2
		public ActionStateInfo()
		{
		}

		// Token: 0x040000E5 RID: 229
		public ActionFlag Action;

		// Token: 0x040000E6 RID: 230
		public byte Value;

		// Token: 0x040000E7 RID: 231
		public WeaponSyncType Flag;
	}
}
