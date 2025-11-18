using Plugin.Core.Network;
using Server.Game.Network;
using System;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_CS_CLAN_LIST_FILTER_ACK : GameServerPacket
	{
		private readonly byte byte_0;

		private readonly int int_0;

		private readonly byte[] byte_1;

		public PROTOCOL_CS_CLAN_LIST_FILTER_ACK(byte byte_2, int int_1, byte[] byte_3)
		{
			this.byte_0 = byte_2;
			this.int_0 = int_1;
			this.byte_1 = byte_3;
		}

		public override void Write()
		{
			base.WriteH(998);
			base.WriteH(0);
			base.WriteC(0);
			base.WriteC(this.byte_0);
			base.WriteH((ushort)this.int_0);
			base.WriteD(this.int_0);
			base.WriteB(this.byte_1);
		}
	}
}