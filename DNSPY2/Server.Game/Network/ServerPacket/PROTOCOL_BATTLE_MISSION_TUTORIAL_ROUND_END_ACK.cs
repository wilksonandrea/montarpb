using System;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x0200006E RID: 110
	public class PROTOCOL_BATTLE_MISSION_TUTORIAL_ROUND_END_ACK : GameServerPacket
	{
		// Token: 0x06000129 RID: 297 RVA: 0x00003418 File Offset: 0x00001618
		public PROTOCOL_BATTLE_MISSION_TUTORIAL_ROUND_END_ACK(RoomModel roomModel_1)
		{
			this.roomModel_0 = roomModel_1;
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00003427 File Offset: 0x00001627
		public override void Write()
		{
			base.WriteH(5189);
			base.WriteC(3);
			base.WriteH((short)(this.roomModel_0.GetTimeByMask() * 60 - this.roomModel_0.GetInBattleTime()));
		}

		// Token: 0x040000DC RID: 220
		private readonly RoomModel roomModel_0;
	}
}
