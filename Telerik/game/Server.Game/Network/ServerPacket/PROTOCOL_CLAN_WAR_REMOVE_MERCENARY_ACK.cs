using Plugin.Core.Network;
using Server.Game.Network;
using System;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_CLAN_WAR_REMOVE_MERCENARY_ACK : GameServerPacket
	{
		public PROTOCOL_CLAN_WAR_REMOVE_MERCENARY_ACK()
		{
		}

		public override void Write()
		{
			base.WriteH(6941);
			base.WriteD(0);
		}
	}
}