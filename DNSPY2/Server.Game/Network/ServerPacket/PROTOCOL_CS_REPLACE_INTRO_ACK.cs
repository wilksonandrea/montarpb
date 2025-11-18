using System;
using Plugin.Core.Enums;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x020000C2 RID: 194
	public class PROTOCOL_CS_REPLACE_INTRO_ACK : GameServerPacket
	{
		// Token: 0x060001E5 RID: 485 RVA: 0x00003FC5 File Offset: 0x000021C5
		public PROTOCOL_CS_REPLACE_INTRO_ACK(EventErrorEnum eventErrorEnum_1)
		{
			this.eventErrorEnum_0 = eventErrorEnum_1;
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x00003FD4 File Offset: 0x000021D4
		public override void Write()
		{
			base.WriteH(861);
			base.WriteD((uint)this.eventErrorEnum_0);
		}

		// Token: 0x04000163 RID: 355
		private readonly EventErrorEnum eventErrorEnum_0;
	}
}
