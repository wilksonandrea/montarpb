using Plugin.Core.Network;
using Server.Game.Network;
using System;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_SHOP_TAG_INFO_ACK : GameServerPacket
	{
		public PROTOCOL_SHOP_TAG_INFO_ACK()
		{
		}

		public override void Write()
		{
			base.WriteH(1099);
			base.WriteH(0);
			base.WriteC(7);
			base.WriteC(5);
			base.WriteH(0);
			base.WriteC(0);
			base.WriteD(0);
			base.WriteH(0);
			base.WriteC(3);
			base.WriteQ(0L);
			base.WriteC(0);
			base.WriteC(4);
			base.WriteQ(0L);
			base.WriteC(0);
			base.WriteC(2);
			base.WriteQ(0L);
			base.WriteC(0);
			base.WriteC(6);
			base.WriteQ(0L);
			base.WriteC(0);
			base.WriteC(1);
			base.WriteQ(0L);
			base.WriteD(0);
			base.WriteC(0);
			base.WriteC(255);
			base.WriteC(255);
			base.WriteC(255);
			base.WriteC(0);
			base.WriteC(255);
			base.WriteC(1);
			base.WriteC(7);
			base.WriteC(2);
		}
	}
}