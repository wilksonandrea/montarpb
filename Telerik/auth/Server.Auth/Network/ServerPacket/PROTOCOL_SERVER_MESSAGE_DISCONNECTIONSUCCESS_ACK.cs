using Plugin.Core.Network;
using Plugin.Core.Utility;
using Server.Auth.Network;
using System;

namespace Server.Auth.Network.ServerPacket
{
	public class PROTOCOL_SERVER_MESSAGE_DISCONNECTIONSUCCESS_ACK : AuthServerPacket
	{
		private readonly uint uint_0;

		private readonly bool bool_0;

		public PROTOCOL_SERVER_MESSAGE_DISCONNECTIONSUCCESS_ACK(uint uint_1, bool bool_1)
		{
			this.uint_0 = uint_1;
			this.bool_0 = bool_1;
		}

		public override void Write()
		{
			base.WriteH(3074);
			base.WriteD(uint.Parse(DateTimeUtil.Now("yyMMddHHmm")));
			base.WriteD(this.uint_0);
			base.WriteD(this.bool_0);
			if (this.bool_0)
			{
				base.WriteD(0);
			}
		}
	}
}