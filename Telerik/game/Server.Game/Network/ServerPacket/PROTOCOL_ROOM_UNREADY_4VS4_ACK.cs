using Plugin.Core.Network;
using Server.Game.Network;
using System;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_ROOM_UNREADY_4VS4_ACK : GameServerPacket
	{
		public PROTOCOL_ROOM_UNREADY_4VS4_ACK()
		{
		}

		public override void Write()
		{
			base.WriteH(3624);
			base.WriteD(0);
		}
	}
}