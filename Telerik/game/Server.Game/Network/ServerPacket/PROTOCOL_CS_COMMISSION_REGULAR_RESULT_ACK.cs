using Plugin.Core.Network;
using Server.Game.Network;
using System;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_CS_COMMISSION_REGULAR_RESULT_ACK : GameServerPacket
	{
		public PROTOCOL_CS_COMMISSION_REGULAR_RESULT_ACK()
		{
		}

		public override void Write()
		{
			base.WriteH(841);
		}
	}
}