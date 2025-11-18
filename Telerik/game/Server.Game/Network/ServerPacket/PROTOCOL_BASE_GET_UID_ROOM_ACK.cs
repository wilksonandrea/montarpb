using Plugin.Core.Network;
using Server.Game.Network;
using System;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_BASE_GET_UID_ROOM_ACK : GameServerPacket
	{
		public PROTOCOL_BASE_GET_UID_ROOM_ACK()
		{
		}

		public override void Write()
		{
			base.WriteH(2444);
		}
	}
}