using Plugin.Core.Network;
using Server.Auth.Network;
using System;

namespace Server.Auth.Network.ServerPacket
{
	public class PROTOCOL_BASE_USER_LEAVE_ACK : AuthServerPacket
	{
		private readonly int int_0;

		public PROTOCOL_BASE_USER_LEAVE_ACK(int int_1)
		{
			this.int_0 = int_1;
		}

		public override void Write()
		{
			base.WriteH(2329);
			base.WriteD(this.int_0);
			base.WriteH(0);
		}
	}
}