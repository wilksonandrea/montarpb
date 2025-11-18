using Plugin.Core.Network;
using Server.Game.Network;
using System;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_MESSENGER_NOTE_SEND_ACK : GameServerPacket
	{
		private readonly uint uint_0;

		public PROTOCOL_MESSENGER_NOTE_SEND_ACK(uint uint_1)
		{
			this.uint_0 = uint_1;
		}

		public override void Write()
		{
			base.WriteH(1922);
			base.WriteD(this.uint_0);
		}
	}
}