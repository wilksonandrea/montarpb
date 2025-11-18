using Plugin.Core.Network;
using Server.Auth.Network;
using System;

namespace Server.Auth.Network.ServerPacket
{
	public class PROTOCOL_BASE_CHANNELTYPE_CONDITION_ACK : AuthServerPacket
	{
		public PROTOCOL_BASE_CHANNELTYPE_CONDITION_ACK()
		{
		}

		public override void Write()
		{
			base.WriteH(2486);
			base.WriteB(new byte[888]);
		}
	}
}