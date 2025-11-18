using Plugin.Core.Network;
using Server.Auth.Network;
using System;

namespace Server.Auth.Network.ServerPacket
{
	public class PROTOCOL_BASE_LOGOUT_ACK : AuthServerPacket
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