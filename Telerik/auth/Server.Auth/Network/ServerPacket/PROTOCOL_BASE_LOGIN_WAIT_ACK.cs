using Plugin.Core.Network;
using Server.Auth.Network;
using System;

namespace Server.Auth.Network.ServerPacket
{
	public class PROTOCOL_BASE_LOGIN_WAIT_ACK : AuthServerPacket
	{
		private readonly int int_0;

		public PROTOCOL_BASE_LOGIN_WAIT_ACK(int int_1)
		{
			this.int_0 = int_1;
		}

		public override void Write()
		{
			base.WriteH(2313);
			base.WriteC(3);
			base.WriteH(68);
			base.WriteD(this.int_0);
		}
	}
}