using Plugin.Core.Network;
using Server.Game.Network;
using System;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_AUTH_FIND_USER_ACK : GameServerPacket
	{
		private readonly uint uint_0;

		public PROTOCOL_AUTH_FIND_USER_ACK(uint uint_1)
		{
			this.uint_0 = uint_1;
		}

		public override void Write()
		{
			base.WriteH(1834);
			base.WriteH(0);
			base.WriteD(this.uint_0);
		}
	}
}