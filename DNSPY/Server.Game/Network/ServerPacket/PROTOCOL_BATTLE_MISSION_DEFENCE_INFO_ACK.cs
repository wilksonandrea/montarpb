using System;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x02000067 RID: 103
	public class PROTOCOL_BATTLE_MISSION_DEFENCE_INFO_ACK : GameServerPacket
	{
		// Token: 0x06000117 RID: 279 RVA: 0x00003362 File Offset: 0x00001562
		public PROTOCOL_BATTLE_MISSION_DEFENCE_INFO_ACK(RoomModel roomModel_1)
		{
			this.roomModel_0 = roomModel_1;
		}

		// Token: 0x06000118 RID: 280 RVA: 0x0000D01C File Offset: 0x0000B21C
		public override void Write()
		{
			base.WriteH(5181);
			base.WriteH((ushort)this.roomModel_0.Bar1);
			base.WriteH((ushort)this.roomModel_0.Bar2);
			for (int i = 0; i < 18; i++)
			{
				base.WriteH(this.roomModel_0.Slots[i].DamageBar1);
			}
			for (int j = 0; j < 18; j++)
			{
				base.WriteH(this.roomModel_0.Slots[j].DamageBar2);
			}
		}

		// Token: 0x040000D1 RID: 209
		private readonly RoomModel roomModel_0;
	}
}
