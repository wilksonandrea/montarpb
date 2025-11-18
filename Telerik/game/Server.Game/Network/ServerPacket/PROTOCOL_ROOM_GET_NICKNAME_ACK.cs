using Plugin.Core.Network;
using Server.Game.Network;
using System;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_ROOM_GET_NICKNAME_ACK : GameServerPacket
	{
		private readonly int int_0;

		private readonly string string_0;

		public PROTOCOL_ROOM_GET_NICKNAME_ACK(int int_1, string string_1)
		{
			this.int_0 = int_1;
			this.string_0 = string_1;
		}

		public override void Write()
		{
			base.WriteH(3599);
			base.WriteD(this.int_0);
			base.WriteU(this.string_0, 66);
		}
	}
}