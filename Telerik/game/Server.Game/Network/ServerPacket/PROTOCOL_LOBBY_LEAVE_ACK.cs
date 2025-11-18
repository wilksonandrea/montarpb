using Plugin.Core.Network;
using Server.Game.Network;
using System;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_LOBBY_LEAVE_ACK : GameServerPacket
	{
		private readonly uint uint_0;

		public PROTOCOL_LOBBY_LEAVE_ACK(uint uint_1)
		{
			this.uint_0 = uint_1;
		}

		public override void Write()
		{
			base.WriteH(2562);
			base.WriteD(this.uint_0);
		}
	}
}