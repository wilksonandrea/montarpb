using Plugin.Core.Network;
using Server.Game.Network;
using System;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_CS_CLIENT_LEAVE_ACK : GameServerPacket
	{
		public PROTOCOL_CS_CLIENT_LEAVE_ACK()
		{
		}

		public override void Write()
		{
			base.WriteH(772);
			base.WriteD(0);
		}
	}
}