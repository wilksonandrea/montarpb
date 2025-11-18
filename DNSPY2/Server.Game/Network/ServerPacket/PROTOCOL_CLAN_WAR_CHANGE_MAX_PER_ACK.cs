using System;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x02000089 RID: 137
	public class PROTOCOL_CLAN_WAR_CHANGE_MAX_PER_ACK : GameServerPacket
	{
		// Token: 0x0600016A RID: 362 RVA: 0x00003739 File Offset: 0x00001939
		public PROTOCOL_CLAN_WAR_CHANGE_MAX_PER_ACK(MatchModel matchModel_0, Account account_0)
		{
			this.match = matchModel_0;
			this.Player = account_0;
		}

		// Token: 0x0600016B RID: 363 RVA: 0x0000E8CC File Offset: 0x0000CACC
		public override void Write()
		{
			base.WriteH(6927);
			base.WriteH((short)this.match.MatchId);
			base.WriteH((ushort)this.match.GetServerInfo());
			base.WriteH((ushort)this.match.GetServerInfo());
			base.WriteC((byte)this.match.State);
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
			if (this.Player != null)
			{
				base.WriteC((byte)this.Player.Rank);
				base.WriteS(this.Player.Nickname, 33);
				base.WriteQ(this.Player.PlayerId);
				base.WriteC((byte)this.match.Slots[this.match.Leader].State);
				return;
			}
			base.WriteB(new byte[43]);
		}

		// Token: 0x04000109 RID: 265
		public readonly MatchModel match;

		// Token: 0x0400010A RID: 266
		public readonly Account Player;
	}
}
