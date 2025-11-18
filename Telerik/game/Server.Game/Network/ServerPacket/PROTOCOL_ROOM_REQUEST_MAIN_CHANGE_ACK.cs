using Plugin.Core.Network;
using Server.Game.Network;
using System;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_ROOM_REQUEST_MAIN_CHANGE_ACK : GameServerPacket
	{
		private readonly uint uint_0;

		public PROTOCOL_ROOM_REQUEST_MAIN_CHANGE_ACK(uint uint_1)
		{
			this.uint_0 = uint_1;
		}

		public PROTOCOL_ROOM_REQUEST_MAIN_CHANGE_ACK(int int_0)
		{
			this.uint_0 = (uint)int_0;
		}

		public override void Write()
		{
			base.WriteH(3614);
			base.WriteD(this.uint_0);
		}
	}
}