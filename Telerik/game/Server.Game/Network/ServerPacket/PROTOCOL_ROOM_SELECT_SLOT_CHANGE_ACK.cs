using Plugin.Core.Network;
using Server.Game.Network;
using System;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_ROOM_SELECT_SLOT_CHANGE_ACK : GameServerPacket
	{
		public PROTOCOL_ROOM_SELECT_SLOT_CHANGE_ACK()
		{
		}

		public override void Write()
		{
			base.WriteH(3683);
			base.WriteC(0);
			base.WriteC(0);
			base.WriteC(0);
			base.WriteC(0);
			base.WriteC(0);
			base.WriteC(0);
		}
	}
}