using Plugin.Core.Network;
using Plugin.Core.Utility;
using Server.Game.Network;
using System;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_SHOP_ENTER_ACK : GameServerPacket
	{
		public PROTOCOL_SHOP_ENTER_ACK()
		{
		}

		public override void Write()
		{
			base.WriteH(1026);
			base.WriteD(0);
			base.WriteC(0);
			base.WriteD(uint.Parse(DateTimeUtil.Now("yyMMddHHmm")));
		}
	}
}