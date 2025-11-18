using Plugin.Core.Network;
using Server.Game.Network;
using System;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_BASE_USER_TITLE_EQUIP_ACK : GameServerPacket
	{
		private readonly uint uint_0;

		private readonly int int_0;

		private readonly int int_1;

		public PROTOCOL_BASE_USER_TITLE_EQUIP_ACK(uint uint_1, int int_2, int int_3)
		{
			this.uint_0 = uint_1;
			this.int_0 = int_2;
			this.int_1 = int_3;
		}

		public override void Write()
		{
			base.WriteH(2379);
			base.WriteD(this.uint_0);
			base.WriteC((byte)this.int_0);
			base.WriteC((byte)this.int_1);
		}
	}
}