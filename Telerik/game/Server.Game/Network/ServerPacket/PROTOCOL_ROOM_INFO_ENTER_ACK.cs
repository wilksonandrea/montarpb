using Plugin.Core.Network;
using Server.Game.Network;
using System;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_ROOM_INFO_ENTER_ACK : GameServerPacket
	{
		public PROTOCOL_ROOM_INFO_ENTER_ACK()
		{
		}

		public override void Write()
		{
			base.WriteH(3672);
			base.WriteD(0);
			base.WriteC(68);
		}
	}
}