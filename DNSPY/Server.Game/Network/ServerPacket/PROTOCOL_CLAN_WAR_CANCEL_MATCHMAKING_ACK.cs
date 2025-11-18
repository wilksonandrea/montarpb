using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x02000088 RID: 136
	public class PROTOCOL_CLAN_WAR_CANCEL_MATCHMAKING_ACK : GameServerPacket
	{
		// Token: 0x06000168 RID: 360 RVA: 0x00002672 File Offset: 0x00000872
		public PROTOCOL_CLAN_WAR_CANCEL_MATCHMAKING_ACK()
		{
		}

		// Token: 0x06000169 RID: 361 RVA: 0x0000372C File Offset: 0x0000192C
		public override void Write()
		{
			base.WriteH(6935);
		}
	}
}
