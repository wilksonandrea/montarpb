using System;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x0200006D RID: 109
	public class PROTOCOL_BATTLE_MISSION_TOUCHDOWN_COUNT_ACK : GameServerPacket
	{
		// Token: 0x06000127 RID: 295 RVA: 0x000033D8 File Offset: 0x000015D8
		public PROTOCOL_BATTLE_MISSION_TOUCHDOWN_COUNT_ACK(RoomModel roomModel_1)
		{
			this.roomModel_0 = roomModel_1;
		}

		// Token: 0x06000128 RID: 296 RVA: 0x000033E7 File Offset: 0x000015E7
		public override void Write()
		{
			base.WriteH(5185);
			base.WriteH((ushort)this.roomModel_0.FRDino);
			base.WriteH((ushort)this.roomModel_0.CTDino);
		}

		// Token: 0x040000DB RID: 219
		private readonly RoomModel roomModel_0;
	}
}
