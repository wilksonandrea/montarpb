using Plugin.Core.Network;
using Server.Game.Network;
using System;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_SERVER_MESSAGE_ITEM_RECEIVE_ACK : GameServerPacket
	{
		private readonly uint uint_0;

		public PROTOCOL_SERVER_MESSAGE_ITEM_RECEIVE_ACK(uint uint_1)
		{
			this.uint_0 = uint_1;
		}

		public override void Write()
		{
			base.WriteH(3084);
			base.WriteD(this.uint_0);
		}
	}
}