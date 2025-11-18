using Plugin.Core.Network;
using Plugin.Core.Utility;
using Server.Game.Network;
using System;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_BASE_RANDOMBOX_LIST_ACK : GameServerPacket
	{
		private readonly bool bool_0;

		public PROTOCOL_BASE_RANDOMBOX_LIST_ACK(bool bool_1)
		{
			this.bool_0 = bool_1;
		}

		public override void Write()
		{
			base.WriteH(2499);
			base.WriteC((byte)this.bool_0);
			base.WriteD(uint.Parse(DateTimeUtil.Now("yyMMddHHmm")));
		}
	}
}