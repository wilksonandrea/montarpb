using Plugin.Core.Models;
using Plugin.Core.Network;
using Server.Game.Data.Models;
using Server.Game.Network;
using System;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_CLAN_WAR_ENEMY_INFO_ACK : GameServerPacket
	{
		public readonly MatchModel match;

		public PROTOCOL_CLAN_WAR_ENEMY_INFO_ACK(MatchModel matchModel_0)
		{
			this.match = matchModel_0;
		}

		public override void Write()
		{
			base.WriteH(1574);
			base.WriteH((short)this.match.GetServerInfo());
			base.WriteC((byte)this.match.MatchId);
			base.WriteC((byte)this.match.FriendId);
			base.WriteC((byte)this.match.Training);
			base.WriteC((byte)this.match.GetCountPlayers());
			base.WriteD(this.match.Leader);
			base.WriteC(0);
			base.WriteD(this.match.Clan.Id);
			base.WriteC((byte)this.match.Clan.Rank);
			base.WriteD(this.match.Clan.Logo);
			base.WriteS(this.match.Clan.Name, 17);
			base.WriteT(this.match.Clan.Points);
			base.WriteC((byte)this.match.Clan.NameColor);
		}
	}
}