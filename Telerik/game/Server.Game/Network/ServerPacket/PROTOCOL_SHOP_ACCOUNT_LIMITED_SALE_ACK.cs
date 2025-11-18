using Plugin.Core.Network;
using Server.Game.Network;
using System;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_SHOP_ACCOUNT_LIMITED_SALE_ACK : GameServerPacket
	{
		public PROTOCOL_SHOP_ACCOUNT_LIMITED_SALE_ACK()
		{
		}

		public override void Write()
		{
			base.WriteH(1101);
			base.WriteD(1);
			base.WriteD(1);
			base.WriteD(1);
			base.WriteD(1);
		}
	}
}