using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x02000073 RID: 115
	public class PROTOCOL_BATTLE_NOTIFY_KICKVOTE_CANCEL_ACK : GameServerPacket
	{
		// Token: 0x06000136 RID: 310 RVA: 0x00002672 File Offset: 0x00000872
		public PROTOCOL_BATTLE_NOTIFY_KICKVOTE_CANCEL_ACK()
		{
		}

		// Token: 0x06000137 RID: 311 RVA: 0x000034EF File Offset: 0x000016EF
		public override void Write()
		{
			base.WriteH(3405);
		}
	}
}
