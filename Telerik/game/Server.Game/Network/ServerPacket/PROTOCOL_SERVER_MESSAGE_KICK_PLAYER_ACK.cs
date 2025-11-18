using Plugin.Core.Network;
using Server.Game.Network;
using System;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_SERVER_MESSAGE_KICK_PLAYER_ACK : GameServerPacket
	{
		public PROTOCOL_SERVER_MESSAGE_KICK_PLAYER_ACK()
		{
		}

		public override void Write()
		{
			base.WriteH(3075);
			base.WriteC(0);
		}
	}
}