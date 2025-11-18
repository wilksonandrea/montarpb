using System;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x02000064 RID: 100
	public class PROTOCOL_BATTLE_LEAVEP2PSERVER_ACK : GameServerPacket
	{
		// Token: 0x06000111 RID: 273 RVA: 0x000032D8 File Offset: 0x000014D8
		public PROTOCOL_BATTLE_LEAVEP2PSERVER_ACK(RoomModel roomModel_1)
		{
			this.roomModel_0 = roomModel_1;
		}

		// Token: 0x06000112 RID: 274 RVA: 0x000032E7 File Offset: 0x000014E7
		public override void Write()
		{
			base.WriteH(5149);
			base.WriteD(this.roomModel_0.LeaderSlot);
		}

		// Token: 0x040000C9 RID: 201
		private readonly RoomModel roomModel_0;
	}
}
