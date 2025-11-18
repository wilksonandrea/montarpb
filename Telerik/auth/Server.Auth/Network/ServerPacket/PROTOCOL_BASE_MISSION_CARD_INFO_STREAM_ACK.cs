using Plugin.Core.Network;
using Server.Auth.Network;
using System;

namespace Server.Auth.Network.ServerPacket
{
	public class PROTOCOL_BASE_MISSION_CARD_INFO_STREAM_ACK : AuthServerPacket
	{
		private readonly byte[] byte_0;

		private readonly byte byte_1;

		private readonly short short_0;

		public PROTOCOL_BASE_MISSION_CARD_INFO_STREAM_ACK(byte[] byte_2, short short_1, byte byte_3)
		{
			this.byte_0 = byte_2;
			this.short_0 = short_1;
			this.byte_1 = byte_3;
		}

		public override void Write()
		{
			base.WriteH(2517);
			base.WriteH(this.short_0);
			base.WriteC(this.byte_1);
			base.WriteB(this.byte_0);
		}
	}
}