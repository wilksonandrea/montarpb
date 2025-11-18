using Plugin.Core.Network;
using Server.Game.Network;
using System;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_CS_JOIN_REQUEST_ACK : GameServerPacket
	{
		private readonly int int_0;

		private readonly uint uint_0;

		public PROTOCOL_CS_JOIN_REQUEST_ACK(uint uint_1, int int_1)
		{
			this.uint_0 = uint_1;
			this.int_0 = int_1;
		}

		public override void Write()
		{
			base.WriteH(813);
			base.WriteD(this.uint_0);
			if (this.uint_0 == 0)
			{
				base.WriteD(this.int_0);
			}
		}
	}
}