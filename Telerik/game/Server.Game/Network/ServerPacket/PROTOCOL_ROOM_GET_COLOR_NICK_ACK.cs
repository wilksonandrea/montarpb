using Plugin.Core.Network;
using Server.Game.Network;
using System;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_ROOM_GET_COLOR_NICK_ACK : GameServerPacket
	{
		private readonly int int_0;

		private readonly int int_1;

		public PROTOCOL_ROOM_GET_COLOR_NICK_ACK(int int_2, int int_3)
		{
			this.int_0 = int_2;
			this.int_1 = int_3;
		}

		public override void Write()
		{
			base.WriteH(3628);
			base.WriteD(this.int_0);
			base.WriteC((byte)this.int_1);
		}
	}
}