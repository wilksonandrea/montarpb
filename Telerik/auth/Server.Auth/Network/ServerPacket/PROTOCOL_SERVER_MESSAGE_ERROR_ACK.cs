using Plugin.Core.Network;
using Server.Auth.Network;
using System;

namespace Server.Auth.Network.ServerPacket
{
	public class PROTOCOL_SERVER_MESSAGE_ERROR_ACK : AuthServerPacket
	{
		private readonly uint uint_0;

		public PROTOCOL_SERVER_MESSAGE_ERROR_ACK(uint uint_1)
		{
			this.uint_0 = uint_1;
		}

		public override void Write()
		{
			base.WriteH(3078);
			base.WriteD(this.uint_0);
		}
	}
}