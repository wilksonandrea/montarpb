using Plugin.Core.Network;
using Server.Game.Network;
using System;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_LOBBY_ENTER_ACK : GameServerPacket
	{
		private readonly uint uint_0;

		public PROTOCOL_LOBBY_ENTER_ACK(uint uint_1)
		{
			this.uint_0 = uint_1;
		}

		public override void Write()
		{
			base.WriteH(2584);
			base.WriteH(0);
			base.WriteD(this.uint_0);
			base.WriteC(0);
			base.WriteQ(0L);
		}
	}
}