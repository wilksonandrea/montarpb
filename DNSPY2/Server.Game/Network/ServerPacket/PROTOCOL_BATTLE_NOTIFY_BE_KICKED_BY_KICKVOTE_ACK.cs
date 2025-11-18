using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x02000070 RID: 112
	public class PROTOCOL_BATTLE_NOTIFY_BE_KICKED_BY_KICKVOTE_ACK : GameServerPacket
	{
		// Token: 0x06000130 RID: 304 RVA: 0x00002672 File Offset: 0x00000872
		public PROTOCOL_BATTLE_NOTIFY_BE_KICKED_BY_KICKVOTE_ACK()
		{
		}

		// Token: 0x06000131 RID: 305 RVA: 0x0000346B File Offset: 0x0000166B
		public override void Write()
		{
			base.WriteH(3409);
		}
	}
}
