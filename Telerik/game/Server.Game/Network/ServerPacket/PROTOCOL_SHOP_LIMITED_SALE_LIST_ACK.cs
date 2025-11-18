using Plugin.Core.Network;
using Server.Game.Network;
using System;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_SHOP_LIMITED_SALE_LIST_ACK : GameServerPacket
	{
		public PROTOCOL_SHOP_LIMITED_SALE_LIST_ACK()
		{
		}

		public override void Write()
		{
			base.WriteH(1096);
			base.WriteD(1);
			base.WriteD(1);
			base.WriteD(1);
			base.WriteD(1);
			base.WriteC(1);
			base.WriteD(63266001);
			base.WriteC(1);
			base.WriteD(1512052359);
			base.WriteC(1);
		}
	}
}