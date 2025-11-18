using Plugin.Core.Network;
using Server.Game.Network;
using System;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_AUTH_ACCOUNT_KICK_ACK : GameServerPacket
	{
		private readonly int int_0;

		public PROTOCOL_AUTH_ACCOUNT_KICK_ACK(int int_1)
		{
			this.int_0 = int_1;
		}

		public override void Write()
		{
			base.WriteH(1989);
			base.WriteC((byte)this.int_0);
		}
	}
}