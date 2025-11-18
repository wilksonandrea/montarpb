using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.SQL;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network;
using System;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_CS_DETAIL_INFO_ACK : GameServerPacket
	{
		private readonly ClanModel clanModel_0;

		private readonly int int_0;

		private readonly Account account_0;

		private readonly int int_1;

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

		private byte[] method_0(Account account_1)
		{
			byte[] array;
			using (SyncServerPacket syncServerPacket = new SyncServerPacket())
			{
				if (account_1 == null)
				{
					syncServerPacket.WriteU("", 66);
					syncServerPacket.WriteC(0);
					syncServerPacket.WriteC(0);
				}
				else
				{
					syncServerPacket.WriteU(account_1.Nickname, 66);
					syncServerPacket.WriteC((byte)account_1.NickColor);
					syncServerPacket.WriteC((byte)account_1.Rank);
				}
				array = syncServerPacket.ToArray();
			}
			return array;
		}

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
	}
}