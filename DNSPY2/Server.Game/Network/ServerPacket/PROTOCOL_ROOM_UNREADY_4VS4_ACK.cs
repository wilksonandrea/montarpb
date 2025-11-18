using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x02000106 RID: 262
	public class PROTOCOL_ROOM_UNREADY_4VS4_ACK : GameServerPacket
	{
		// Token: 0x06000281 RID: 641 RVA: 0x00002672 File Offset: 0x00000872
		public PROTOCOL_ROOM_UNREADY_4VS4_ACK()
		{
		}

		// Token: 0x06000282 RID: 642 RVA: 0x000049B0 File Offset: 0x00002BB0
		public override void Write()
		{
			base.WriteH(3624);
			base.WriteD(0);
		}
	}
}
