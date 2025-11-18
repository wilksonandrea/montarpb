using Plugin.Core.Network;
using Server.Game.Network;
using System;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_BASE_RANK_UP_ACK : GameServerPacket
	{
		private readonly int int_0;

		private readonly int int_1;

		public PROTOCOL_BASE_RANK_UP_ACK(int int_2, int int_3)
		{
			this.int_0 = int_2;
			this.int_1 = int_3;
		}

		public override void Write()
		{
			base.WriteH(2343);
			base.WriteD(this.int_0);
			base.WriteD(this.int_0);
			base.WriteD(this.int_1);
		}
	}
}