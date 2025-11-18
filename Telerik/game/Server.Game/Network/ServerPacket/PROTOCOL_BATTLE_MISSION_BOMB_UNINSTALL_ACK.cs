using Plugin.Core.Network;
using Server.Game.Network;
using System;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_BATTLE_MISSION_BOMB_UNINSTALL_ACK : GameServerPacket
	{
		private readonly int int_0;

		public PROTOCOL_BATTLE_MISSION_BOMB_UNINSTALL_ACK(int int_1)
		{
			this.int_0 = int_1;
		}

		public override void Write()
		{
			base.WriteH(5159);
			base.WriteD(this.int_0);
		}
	}
}