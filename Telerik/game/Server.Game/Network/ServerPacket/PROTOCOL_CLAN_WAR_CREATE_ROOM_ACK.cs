using Plugin.Core.Network;
using Server.Game.Data.Models;
using Server.Game.Network;
using System;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_CLAN_WAR_CREATE_ROOM_ACK : GameServerPacket
	{
		public readonly MatchModel match;

		public PROTOCOL_CLAN_WAR_CREATE_ROOM_ACK(MatchModel matchModel_0)
		{
			this.match = matchModel_0;
		}

		public override void Write()
		{
			base.WriteH(1564);
			base.WriteH((short)this.match.MatchId);
			base.WriteD(this.match.GetServerInfo());
			base.WriteH((short)this.match.GetServerInfo());
			base.WriteC(10);
		}
	}
}