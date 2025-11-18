using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x02000113 RID: 275
	public class PROTOCOL_SERVER_MESSAGE_KICK_PLAYER_ACK : GameServerPacket
	{
		// Token: 0x0600029B RID: 667 RVA: 0x00002672 File Offset: 0x00000872
		public PROTOCOL_SERVER_MESSAGE_KICK_PLAYER_ACK()
		{
		}

		// Token: 0x0600029C RID: 668 RVA: 0x00004AB7 File Offset: 0x00002CB7
		public override void Write()
		{
			base.WriteH(3075);
			base.WriteC(0);
		}
	}
}
