using Plugin.Core.Network;
using Server.Auth.Network;
using System;

namespace Server.Auth.Network.ServerPacket
{
	public class PROTOCOL_MATCH_SERVER_IDX_ACK : AuthServerPacket
	{
		private readonly short short_0;

		public PROTOCOL_MATCH_SERVER_IDX_ACK(short short_1)
		{
			this.short_0 = short_1;
		}

		public override void Write()
		{
			base.WriteH(7682);
			base.WriteH(0);
		}
	}
}