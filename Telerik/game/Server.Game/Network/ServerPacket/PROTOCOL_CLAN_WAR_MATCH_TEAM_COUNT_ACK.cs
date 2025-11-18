using Plugin.Core.Network;
using Server.Game.Network;
using System;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_CLAN_WAR_MATCH_TEAM_COUNT_ACK : GameServerPacket
	{
		private readonly int int_0;

		public PROTOCOL_CLAN_WAR_MATCH_TEAM_COUNT_ACK(int int_1)
		{
			this.int_0 = int_1;
		}

		public override void Write()
		{
			base.WriteH(6915);
			base.WriteH((short)this.int_0);
			base.WriteC(13);
			base.WriteH((short)Math.Ceiling((double)this.int_0 / 13));
		}
	}
}