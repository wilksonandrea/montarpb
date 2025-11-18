using Plugin.Core.Network;
using Server.Game.Network;
using System;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_BATTLE_TIMEOUTCLIENT_ACK : GameServerPacket
	{
		public PROTOCOL_BATTLE_TIMEOUTCLIENT_ACK()
		{
		}

		public override void Write()
		{
			base.WriteH(5144);
			base.WriteH(0);
		}
	}
}