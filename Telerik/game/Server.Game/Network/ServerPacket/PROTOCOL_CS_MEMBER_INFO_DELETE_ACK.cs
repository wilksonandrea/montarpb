using Plugin.Core.Network;
using Server.Game.Network;
using System;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_CS_MEMBER_INFO_DELETE_ACK : GameServerPacket
	{
		private readonly long long_0;

		public PROTOCOL_CS_MEMBER_INFO_DELETE_ACK(long long_1)
		{
			this.long_0 = long_1;
		}

		public override void Write()
		{
			base.WriteH(849);
			base.WriteQ(this.long_0);
		}
	}
}