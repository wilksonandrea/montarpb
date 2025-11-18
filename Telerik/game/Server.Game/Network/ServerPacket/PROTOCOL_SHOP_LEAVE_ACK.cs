using Plugin.Core.Network;
using Server.Game.Network;
using System;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_SHOP_LEAVE_ACK : GameServerPacket
	{
		public PROTOCOL_SHOP_LEAVE_ACK()
		{
		}

		public override void Write()
		{
			base.WriteH(1028);
			base.WriteH(0);
			base.WriteD(0);
		}
	}
}