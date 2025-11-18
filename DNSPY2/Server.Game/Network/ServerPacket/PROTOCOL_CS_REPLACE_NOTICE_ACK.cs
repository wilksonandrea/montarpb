using System;
using Plugin.Core.Enums;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x020000C8 RID: 200
	public class PROTOCOL_CS_REPLACE_NOTICE_ACK : GameServerPacket
	{
		// Token: 0x060001F1 RID: 497 RVA: 0x000040B6 File Offset: 0x000022B6
		public PROTOCOL_CS_REPLACE_NOTICE_ACK(EventErrorEnum eventErrorEnum_1)
		{
			this.eventErrorEnum_0 = eventErrorEnum_1;
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x000040C5 File Offset: 0x000022C5
		public override void Write()
		{
			base.WriteH(859);
			base.WriteD((uint)this.eventErrorEnum_0);
		}

		// Token: 0x0400016A RID: 362
		private readonly EventErrorEnum eventErrorEnum_0;
	}
}
