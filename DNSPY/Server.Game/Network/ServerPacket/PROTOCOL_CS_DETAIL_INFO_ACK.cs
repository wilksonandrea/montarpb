using System;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.SQL;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x020000B3 RID: 179
	public class PROTOCOL_CS_DETAIL_INFO_ACK : GameServerPacket
	{
		// Token: 0x060001C3 RID: 451 RVA: 0x00003D2A File Offset: 0x00001F2A
		public PROTOCOL_CS_DETAIL_INFO_ACK(int int_2, ClanModel clanModel_1)
		{
			this.int_0 = int_2;
			this.clanModel_0 = clanModel_1;
			if (clanModel_1 != null)
			{
				this.account_0 = AccountManager.GetAccount(clanModel_1.OwnerId, 31);
				this.int_1 = DaoManagerSQL.GetClanPlayers(clanModel_1.Id);
			}
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x0000FC40 File Offset: 0x0000DE40
		public override void Write()
		{
			base.WriteH(801);
			base.WriteD(this.int_0);
			base.WriteD(this.clanModel_0.Id);
			base.WriteU(this.clanModel_0.Name, 34);
			base.WriteC((byte)this.clanModel_0.Rank);
			base.WriteC((byte)this.int_1);
			base.WriteC((byte)this.clanModel_0.MaxPlayers);
			base.WriteD(this.clanModel_0.CreationDate);
			base.WriteD(this.clanModel_0.Logo);
			base.WriteC((byte)this.clanModel_0.NameColor);
			base.WriteC((byte)this.clanModel_0.Effect);
			base.WriteC((byte)this.clanModel_0.GetClanUnit());
			base.WriteD(this.clanModel_0.Exp);
			base.WriteQ(this.clanModel_0.OwnerId);
			base.WriteC(0);
			base.WriteC(0);
			base.WriteC(0);
			base.WriteC(0);
			base.WriteB(this.method_0(this.account_0));
			base.WriteU(this.clanModel_0.Info, 510);
			base.WriteB(new byte[41]);
			base.WriteC((byte)this.clanModel_0.JoinType);
			base.WriteC((byte)this.clanModel_0.RankLimit);
			base.WriteC((byte)this.clanModel_0.MaxAgeLimit);
			base.WriteC((byte)this.clanModel_0.MinAgeLimit);
			base.WriteC((byte)this.clanModel_0.Authority);
			base.WriteU(this.clanModel_0.News, 510);
			base.WriteD(0);
			base.WriteD(0);
			base.WriteD(0);
			base.WriteD(0);
			base.WriteD(0);
			base.WriteD(0);
			base.WriteD(0);
			base.WriteD(0);
			base.WriteD(0);
			base.WriteD(0);
			base.WriteD(0);
			base.WriteD(0);
			base.WriteD(0);
			base.WriteD(0);
			base.WriteD(0);
			base.WriteD(0);
			base.WriteD(0);
			base.WriteD(0);
			base.WriteD(0);
			base.WriteD(0);
			base.WriteD(0);
			base.WriteD(0);
			base.WriteD(0);
			base.WriteD(0);
			base.WriteD(this.clanModel_0.Matches);
			base.WriteD(this.clanModel_0.MatchWins);
			base.WriteD(this.clanModel_0.MatchLoses);
			base.WriteD(6666);
			base.WriteD(7777);
			base.WriteD(this.clanModel_0.TotalKills);
			base.WriteD(this.clanModel_0.TotalAssists);
			base.WriteD(this.clanModel_0.TotalDeaths);
			base.WriteD(this.clanModel_0.TotalHeadshots);
			base.WriteD(this.clanModel_0.TotalEscapes);
			base.WriteD(8888);
			base.WriteD(0);
			base.WriteD(0);
			base.WriteD(0);
			base.WriteD(0);
			base.WriteD(0);
			base.WriteD(0);
			base.WriteD(0);
			base.WriteD(0);
			base.WriteD(0);
			base.WriteD(0);
			base.WriteD(0);
			base.WriteD(9999);
			base.WriteD(0);
			base.WriteD(0);
			base.WriteD(0);
			base.WriteD(0);
			base.WriteD(0);
			base.WriteD(0);
			base.WriteD(0);
			base.WriteD(0);
			base.WriteD(0);
			base.WriteD(0);
			base.WriteD(0);
			base.WriteD(0);
			base.WriteD(0);
			base.WriteC(0);
			base.WriteC(0);
			base.WriteC(0);
			base.WriteC(0);
			base.WriteC(0);
			base.WriteC(0);
			base.WriteC(0);
			base.WriteQ(this.clanModel_0.BestPlayers.Exp.PlayerId);
			base.WriteQ(this.clanModel_0.BestPlayers.Exp.PlayerId);
			base.WriteQ(this.clanModel_0.BestPlayers.Wins.PlayerId);
			base.WriteQ(this.clanModel_0.BestPlayers.Wins.PlayerId);
			base.WriteQ(this.clanModel_0.BestPlayers.Kills.PlayerId);
			base.WriteQ(this.clanModel_0.BestPlayers.Kills.PlayerId);
			base.WriteQ(this.clanModel_0.BestPlayers.Headshots.PlayerId);
			base.WriteQ(this.clanModel_0.BestPlayers.Headshots.PlayerId);
			base.WriteQ(this.clanModel_0.BestPlayers.Participation.PlayerId);
			base.WriteQ(this.clanModel_0.BestPlayers.Participation.PlayerId);
			base.WriteC(0);
			base.WriteC(0);
			base.WriteQ(this.clanModel_0.BestPlayers.Exp.PlayerId);
			base.WriteQ(this.clanModel_0.BestPlayers.Wins.PlayerId);
			base.WriteQ(this.clanModel_0.BestPlayers.Kills.PlayerId);
			base.WriteQ(this.clanModel_0.BestPlayers.Headshots.PlayerId);
			base.WriteQ(this.clanModel_0.BestPlayers.Participation.PlayerId);
			base.WriteQ(0L);
			base.WriteQ(0L);
			base.WriteQ(0L);
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x000101FC File Offset: 0x0000E3FC
		private byte[] method_0(Account account_1)
		{
			byte[] array;
			using (SyncServerPacket syncServerPacket = new SyncServerPacket())
			{
				if (account_1 != null)
				{
					syncServerPacket.WriteU(account_1.Nickname, 66);
					syncServerPacket.WriteC((byte)account_1.NickColor);
					syncServerPacket.WriteC((byte)account_1.Rank);
				}
				else
				{
					syncServerPacket.WriteU("", 66);
					syncServerPacket.WriteC(0);
					syncServerPacket.WriteC(0);
				}
				array = syncServerPacket.ToArray();
			}
			return array;
		}

		// Token: 0x04000148 RID: 328
		private readonly ClanModel clanModel_0;

		// Token: 0x04000149 RID: 329
		private readonly int int_0;

		// Token: 0x0400014A RID: 330
		private readonly Account account_0;

		// Token: 0x0400014B RID: 331
		private readonly int int_1;
	}
}
