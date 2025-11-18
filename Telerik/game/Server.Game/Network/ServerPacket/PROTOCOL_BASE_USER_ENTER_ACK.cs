using Plugin.Core.Network;
using Server.Game.Network;
using System;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_BASE_USER_ENTER_ACK : GameServerPacket
	{
		private readonly uint uint_0;

		public PROTOCOL_BASE_USER_ENTER_ACK(uint uint_1)
		{
			this.uint_0 = uint_1;
		}

		public override void Write()
		{
			base.WriteH(2331);
			base.WriteD(this.uint_0);
		}
	}
}