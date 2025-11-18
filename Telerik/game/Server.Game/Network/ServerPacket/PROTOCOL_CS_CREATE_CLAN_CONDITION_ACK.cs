using Plugin.Core;
using Plugin.Core.Network;
using Server.Game.Network;
using System;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_CS_CREATE_CLAN_CONDITION_ACK : GameServerPacket
	{
		public PROTOCOL_CS_CREATE_CLAN_CONDITION_ACK()
		{
		}

		public override void Write()
		{
			base.WriteH(915);
			base.WriteC((byte)ConfigLoader.MinCreateRank);
			base.WriteD(ConfigLoader.MinCreateGold);
		}
	}
}