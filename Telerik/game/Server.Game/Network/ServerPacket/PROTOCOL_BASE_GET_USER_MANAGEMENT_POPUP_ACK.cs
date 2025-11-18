using Plugin.Core.Network;
using Server.Game.Network;
using System;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_BASE_GET_USER_MANAGEMENT_POPUP_ACK : GameServerPacket
	{
		public PROTOCOL_BASE_GET_USER_MANAGEMENT_POPUP_ACK()
		{
		}

		public override void Write()
		{
			base.WriteH(6658);
			base.WriteH(0);
		}
	}
}