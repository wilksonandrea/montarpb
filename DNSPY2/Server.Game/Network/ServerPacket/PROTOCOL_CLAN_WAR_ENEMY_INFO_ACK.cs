using System;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x0200008C RID: 140
	public class PROTOCOL_CLAN_WAR_ENEMY_INFO_ACK : GameServerPacket
	{
		// Token: 0x06000170 RID: 368 RVA: 0x00003774 File Offset: 0x00001974
		public PROTOCOL_CLAN_WAR_ENEMY_INFO_ACK(MatchModel matchModel_0)
		{
			this.match = matchModel_0;
		}

		// Token: 0x06000171 RID: 369 RVA: 0x0000EB94 File Offset: 0x0000CD94
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

		// Token: 0x0400010E RID: 270
		public readonly MatchModel match;
	}
}
