using Plugin.Core.Models;
using Plugin.Core.Network;
using Server.Game.Data.Models;
using Server.Game.Network;
using System;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_BATTLE_MISSION_GENERATOR_INFO_ACK : GameServerPacket
	{
		private readonly RoomModel roomModel_0;

		public PROTOCOL_BATTLE_MISSION_GENERATOR_INFO_ACK(RoomModel roomModel_1)
		{
			this.roomModel_0 = roomModel_1;
		}

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
	}
}