using Plugin.Core.Network;
using Server.Game.Network;
using System;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_BASE_LOGOUT_ACK : GameServerPacket
	{
		public PROTOCOL_BASE_LOGOUT_ACK()
		{
		}

		public override void Write()
		{
			base.WriteH(2308);
			base.WriteH(0);
		}
	}
}