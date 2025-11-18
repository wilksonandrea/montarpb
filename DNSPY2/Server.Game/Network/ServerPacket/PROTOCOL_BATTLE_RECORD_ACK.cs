using System;
using Plugin.Core.Models;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x02000077 RID: 119
	public class PROTOCOL_BATTLE_RECORD_ACK : GameServerPacket
	{
		// Token: 0x06000140 RID: 320 RVA: 0x00003548 File Offset: 0x00001748
		public PROTOCOL_BATTLE_RECORD_ACK(RoomModel roomModel_1)
		{
			this.roomModel_0 = roomModel_1;
		}

		// Token: 0x06000141 RID: 321 RVA: 0x0000D944 File Offset: 0x0000BB44
		public override void Write()
		{
			base.WriteH(5163);
			base.WriteH((ushort)this.roomModel_0.FRKills);
			base.WriteH((ushort)this.roomModel_0.FRDeaths);
			base.WriteH((ushort)this.roomModel_0.FRAssists);
			base.WriteH((ushort)this.roomModel_0.CTKills);
			base.WriteH((ushort)this.roomModel_0.CTDeaths);
			base.WriteH((ushort)this.roomModel_0.CTAssists);
			foreach (SlotModel slotModel in this.roomModel_0.Slots)
			{
				base.WriteH((ushort)slotModel.AllKills);
				base.WriteH((ushort)slotModel.AllDeaths);
				base.WriteH((ushort)slotModel.AllAssists);
			}
		}

		// Token: 0x040000E9 RID: 233
		private readonly RoomModel roomModel_0;
	}
}
