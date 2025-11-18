using Plugin.Core.Network;
using Server.Auth.Network;
using System;

namespace Server.Auth.Network.ServerPacket
{
	public class PROTOCOL_BASE_OPTION_SAVE_ACK : AuthServerPacket
	{
		public PROTOCOL_BASE_OPTION_SAVE_ACK()
		{
		}

		public override void Write()
		{
			base.WriteH(2323);
			base.WriteD(0);
		}
	}
}