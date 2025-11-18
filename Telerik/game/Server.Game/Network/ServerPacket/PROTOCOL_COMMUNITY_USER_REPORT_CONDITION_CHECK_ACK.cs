using Plugin.Core.Network;
using Server.Game.Network;
using System;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_COMMUNITY_USER_REPORT_CONDITION_CHECK_ACK : GameServerPacket
	{
		private readonly int int_0;

		public PROTOCOL_COMMUNITY_USER_REPORT_CONDITION_CHECK_ACK(int int_1)
		{
			this.int_0 = int_1;
		}

		public override void Write()
		{
			base.WriteD(3853);
			base.WriteD(this.int_0);
		}
	}
}