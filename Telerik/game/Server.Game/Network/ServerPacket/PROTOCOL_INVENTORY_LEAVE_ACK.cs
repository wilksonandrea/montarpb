using Plugin.Core.Network;
using Server.Game.Network;
using System;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_INVENTORY_LEAVE_ACK : GameServerPacket
	{
		public PROTOCOL_INVENTORY_LEAVE_ACK()
		{
		}

		public override void Write()
		{
			base.WriteH(3332);
			base.WriteH(0);
			base.WriteD(0);
		}
	}
}