using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x02000022 RID: 34
	public class PROTOCOL_BASE_TICKET_UPDATE_ACK : GameServerPacket
	{
		// Token: 0x06000080 RID: 128 RVA: 0x00002672 File Offset: 0x00000872
		public PROTOCOL_BASE_TICKET_UPDATE_ACK()
		{
		}

		// Token: 0x06000081 RID: 129 RVA: 0x000029A3 File Offset: 0x00000BA3
		public override void Write()
		{
			base.WriteH(2509);
			base.WriteH(0);
		}
	}
}
