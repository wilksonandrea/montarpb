using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x0200000D RID: 13
	public class PROTOCOL_BASE_GET_UID_LOBBY_ACK : GameServerPacket
	{
		// Token: 0x06000056 RID: 86 RVA: 0x00002672 File Offset: 0x00000872
		public PROTOCOL_BASE_GET_UID_LOBBY_ACK()
		{
		}

		// Token: 0x06000057 RID: 87 RVA: 0x000026FB File Offset: 0x000008FB
		public override void Write()
		{
			base.WriteH(2442);
		}
	}
}
