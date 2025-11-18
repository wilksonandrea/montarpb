using Plugin.Core.Network;
using Server.Game.Network;
using System;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_CS_CANCEL_REQUEST_ACK : GameServerPacket
	{
		private readonly uint uint_0;

		public PROTOCOL_CS_CANCEL_REQUEST_ACK(uint uint_1)
		{
			this.uint_0 = uint_1;
		}

		public override void Write()
		{
			base.WriteH(815);
			base.WriteD(this.uint_0);
		}
	}
}