using Plugin.Core.Network;
using Server.Game.Network;
using System;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_AUTH_SHOP_JACKPOT_ACK : GameServerPacket
	{
		private readonly string string_0;

		private readonly int int_0;

		private readonly int int_1;

		public PROTOCOL_AUTH_SHOP_JACKPOT_ACK(string string_1, int int_2, int int_3)
		{
			this.string_0 = string_1;
			this.int_0 = int_2;
			this.int_1 = int_3;
		}

		public override void Write()
		{
			base.WriteH(1068);
			base.WriteH(0);
			base.WriteC((byte)this.int_1);
			base.WriteD(this.int_0);
			base.WriteC((byte)this.string_0.Length);
			base.WriteN(this.string_0, this.string_0.Length, "UTF-16LE");
		}
	}
}