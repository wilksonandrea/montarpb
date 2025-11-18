using Plugin.Core.Network;
using Server.Auth.Network;
using System;

namespace Server.Auth.Network.ServerPacket
{
	public class PROTOCOL_MESSENGER_NOTE_LIST_ACK : AuthServerPacket
	{
		private readonly int int_0;

		private readonly int int_1;

		private readonly byte[] byte_0;

		private readonly byte[] byte_1;

		public PROTOCOL_MESSENGER_NOTE_LIST_ACK(int int_2, int int_3, byte[] byte_2, byte[] byte_3)
		{
			this.int_1 = int_2;
			this.int_0 = int_3;
			this.byte_0 = byte_2;
			this.byte_1 = byte_3;
		}

		public override void Write()
		{
			base.WriteH(1925);
			base.WriteC((byte)this.int_0);
			base.WriteC((byte)this.int_1);
			base.WriteB(this.byte_0);
			base.WriteB(this.byte_1);
		}
	}
}