using Plugin.Core.Network;
using Server.Game.Network;
using System;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_CLAN_WAR_CANCEL_MATCHMAKING_ACK : GameServerPacket
	{
		public PROTOCOL_CLAN_WAR_CANCEL_MATCHMAKING_ACK()
		{
		}

		public override void Write()
		{
			base.WriteH(6935);
		}
	}
}