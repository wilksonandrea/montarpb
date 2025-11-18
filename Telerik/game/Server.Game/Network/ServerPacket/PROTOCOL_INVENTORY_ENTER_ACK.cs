using Plugin.Core.Network;
using Plugin.Core.Utility;
using Server.Game.Network;
using System;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_INVENTORY_ENTER_ACK : GameServerPacket
	{
		public PROTOCOL_INVENTORY_ENTER_ACK()
		{
		}

		public override void Write()
		{
			base.WriteH(3330);
			base.WriteD(0);
			base.WriteD(uint.Parse(DateTimeUtil.Now("yyMMddHHmm")));
		}
	}
}