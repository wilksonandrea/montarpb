using Plugin.Core.Network;
using Server.Game.Network;
using System;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_ROOM_PERSONAL_TEAM_CHANGE_ACK : GameServerPacket
	{
		public PROTOCOL_ROOM_PERSONAL_TEAM_CHANGE_ACK()
		{
		}

		public override void Write()
		{
			base.WriteH(3610);
			base.WriteC(2);
			base.WriteC(0);
			base.WriteC(0);
			base.WriteC(0);
			base.WriteC(0);
			base.WriteC(2);
			base.WriteC(0);
			base.WriteC(8);
		}
	}
}