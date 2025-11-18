using Plugin.Core.Network;
using Server.Auth.Network;
using System;

namespace Server.Auth.Network.ServerPacket
{
	public class PROTOCOL_BASE_BOOSTEVENT_INFO_ACK : AuthServerPacket
	{
		public PROTOCOL_BASE_BOOSTEVENT_INFO_ACK()
		{
		}

		public override void Write()
		{
			base.WriteH(2469);
			base.WriteD(1);
			base.WriteD(0);
			base.WriteC(0);
		}
	}
}