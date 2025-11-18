using System;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x02000068 RID: 104
	public class PROTOCOL_BATTLE_MISSION_GENERATOR_INFO_ACK : GameServerPacket
	{
		// Token: 0x06000119 RID: 281 RVA: 0x00003371 File Offset: 0x00001571
		public PROTOCOL_BATTLE_MISSION_GENERATOR_INFO_ACK(RoomModel roomModel_1)
		{
			this.roomModel_0 = roomModel_1;
		}

		// Token: 0x0600011A RID: 282 RVA: 0x0000D0A4 File Offset: 0x0000B2A4
		public override void Write()
		{
			base.WriteH(5167);
			base.WriteH((ushort)this.roomModel_0.Bar1);
			base.WriteH((ushort)this.roomModel_0.Bar2);
			for (int i = 0; i < 18; i++)
			{
				base.WriteH(this.roomModel_0.Slots[i].DamageBar1);
			}
		}

		// Token: 0x040000D2 RID: 210
		private readonly RoomModel roomModel_0;
	}
}
