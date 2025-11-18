using Plugin.Core.Network;
using Server.Game.Network;
using System;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_CS_ROOM_INVITED_ACK : GameServerPacket
	{
		private readonly int int_0;

		public PROTOCOL_CS_ROOM_INVITED_ACK(int int_1)
		{
			this.int_0 = int_1;
		}

		public override void Write()
		{
			base.WriteH(1902);
			base.WriteD(this.int_0);
		}
	}
}