using Plugin.Core.Network;
using Server.Game.Network;
using System;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_COMMUNITY_USER_REPORT_ACK : GameServerPacket
	{
		private readonly uint uint_0;

		public PROTOCOL_COMMUNITY_USER_REPORT_ACK(uint uint_1)
		{
			this.uint_0 = uint_1;
		}

		public override void Write()
		{
			base.WriteD(3851);
			base.WriteD(this.uint_0);
		}
	}
}