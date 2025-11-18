using Plugin.Core.Network;
using Plugin.Core.Utility;
using Server.Game.Network;
using System;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_SHOP_LIMITED_SALE_SYNC_ACK : GameServerPacket
	{
		public PROTOCOL_SHOP_LIMITED_SALE_SYNC_ACK()
		{
		}

		public override void Write()
		{
			base.WriteH(1098);
			base.WriteC(1);
			base.WriteD(uint.Parse(DateTimeUtil.Now("yyMMddHHmm")));
		}
	}
}