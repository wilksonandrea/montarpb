using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x0200000E RID: 14
	public class PROTOCOL_BASE_GET_UID_ROOM_ACK : GameServerPacket
	{
		// Token: 0x06000058 RID: 88 RVA: 0x00002672 File Offset: 0x00000872
		public PROTOCOL_BASE_GET_UID_ROOM_ACK()
		{
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002708 File Offset: 0x00000908
		public override void Write()
		{
			base.WriteH(2444);
		}
	}
}
