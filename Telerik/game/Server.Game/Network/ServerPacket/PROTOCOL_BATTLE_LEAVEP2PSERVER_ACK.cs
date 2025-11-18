using Plugin.Core.Network;
using Server.Game.Data.Models;
using Server.Game.Network;
using System;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_BATTLE_LEAVEP2PSERVER_ACK : GameServerPacket
	{
		private readonly RoomModel roomModel_0;

		public PROTOCOL_BATTLE_LEAVEP2PSERVER_ACK(RoomModel roomModel_1)
		{
			this.roomModel_0 = roomModel_1;
		}

		public override void Write()
		{
			base.WriteH(5149);
			base.WriteD(this.roomModel_0.LeaderSlot);
		}
	}
}