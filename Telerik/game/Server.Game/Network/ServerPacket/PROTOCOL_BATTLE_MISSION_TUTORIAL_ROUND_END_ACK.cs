using Plugin.Core.Network;
using Server.Game.Data.Models;
using Server.Game.Network;
using System;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_BATTLE_MISSION_TUTORIAL_ROUND_END_ACK : GameServerPacket
	{
		private readonly RoomModel roomModel_0;

		public PROTOCOL_BATTLE_MISSION_TUTORIAL_ROUND_END_ACK(RoomModel roomModel_1)
		{
			this.roomModel_0 = roomModel_1;
		}

		public override void Write()
		{
			base.WriteH(5189);
			base.WriteC(3);
			base.WriteH((short)(this.roomModel_0.GetTimeByMask() * 60 - this.roomModel_0.GetInBattleTime()));
		}
	}
}