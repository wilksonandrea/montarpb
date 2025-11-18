using Plugin.Core.Network;
using Server.Game.Network;
using System;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_ROOM_GET_NAMECARD_ACK : GameServerPacket
	{
		public PROTOCOL_ROOM_GET_NAMECARD_ACK()
		{
		}

		public override void Write()
		{
			base.WriteH(3637);
		}
	}
}