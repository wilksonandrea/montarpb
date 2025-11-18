using Plugin.Core.Network;
using Server.Game.Network;
using System;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_SERVER_MESSAGE_USER_EVENT_START_ACK : GameServerPacket
	{
		public PROTOCOL_SERVER_MESSAGE_USER_EVENT_START_ACK()
		{
		}

		public override void Write()
		{
			base.WriteH(3086);
		}
	}
}