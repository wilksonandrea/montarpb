using Plugin.Core.Network;
using Server.Game.Network;
using System;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_CLAN_WAR_ACCEPT_BATTLE_ACK : GameServerPacket
	{
		private readonly uint uint_0;

		public PROTOCOL_CLAN_WAR_ACCEPT_BATTLE_ACK(uint uint_1)
		{
			this.uint_0 = uint_1;
		}

		public override void Write()
		{
			base.WriteH(1559);
			base.WriteD(this.uint_0);
		}
	}
}