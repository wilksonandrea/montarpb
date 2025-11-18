using Plugin.Core.Models;
using Plugin.Core.Network;
using Server.Game.Data.Models;
using Server.Game.Network;
using System;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_BATTLE_RECORD_ACK : GameServerPacket
	{
		private readonly RoomModel roomModel_0;

		public PROTOCOL_BATTLE_RECORD_ACK(RoomModel roomModel_1)
		{
			this.roomModel_0 = roomModel_1;
		}

		public override void Write()
		{
			base.WriteH(5163);
			base.WriteH((ushort)this.roomModel_0.FRKills);
			base.WriteH((ushort)this.roomModel_0.FRDeaths);
			base.WriteH((ushort)this.roomModel_0.FRAssists);
			base.WriteH((ushort)this.roomModel_0.CTKills);
			base.WriteH((ushort)this.roomModel_0.CTDeaths);
			base.WriteH((ushort)this.roomModel_0.CTAssists);
			SlotModel[] slots = this.roomModel_0.Slots;
			for (int i = 0; i < (int)slots.Length; i++)
			{
				SlotModel slotModel = slots[i];
				base.WriteH((ushort)slotModel.AllKills);
				base.WriteH((ushort)slotModel.AllDeaths);
				base.WriteH((ushort)slotModel.AllAssists);
			}
		}
	}
}