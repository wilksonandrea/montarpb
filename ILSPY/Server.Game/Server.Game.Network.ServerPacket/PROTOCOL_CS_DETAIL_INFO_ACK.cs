using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.SQL;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_CS_DETAIL_INFO_ACK : GameServerPacket
{
	private readonly ClanModel clanModel_0;

	private readonly int int_0;

	private readonly Account account_0;

	private readonly int int_1;

	public PROTOCOL_CS_DETAIL_INFO_ACK(int int_2, ClanModel clanModel_1)
	{
		int_0 = int_2;
		clanModel_0 = clanModel_1;
		if (clanModel_1 != null)
		{
			account_0 = AccountManager.GetAccount(clanModel_1.OwnerId, 31);
			int_1 = DaoManagerSQL.GetClanPlayers(clanModel_1.Id);
		}
	}

	public override void Write()
	{
		WriteH(801);
		WriteD(int_0);
		WriteD(clanModel_0.Id);
		WriteU(clanModel_0.Name, 34);
		WriteC((byte)clanModel_0.Rank);
		WriteC((byte)int_1);
		WriteC((byte)clanModel_0.MaxPlayers);
		WriteD(clanModel_0.CreationDate);
		WriteD(clanModel_0.Logo);
		WriteC((byte)clanModel_0.NameColor);
		WriteC((byte)clanModel_0.Effect);
		WriteC((byte)clanModel_0.GetClanUnit());
		WriteD(clanModel_0.Exp);
		WriteQ(clanModel_0.OwnerId);
		WriteC(0);
		WriteC(0);
		WriteC(0);
		WriteC(0);
		WriteB(method_0(account_0));
		WriteU(clanModel_0.Info, 510);
		WriteB(new byte[41]);
		WriteC((byte)clanModel_0.JoinType);
		WriteC((byte)clanModel_0.RankLimit);
		WriteC((byte)clanModel_0.MaxAgeLimit);
		WriteC((byte)clanModel_0.MinAgeLimit);
		WriteC((byte)clanModel_0.Authority);
		WriteU(clanModel_0.News, 510);
		WriteD(0);
		WriteD(0);
		WriteD(0);
		WriteD(0);
		WriteD(0);
		WriteD(0);
		WriteD(0);
		WriteD(0);
		WriteD(0);
		WriteD(0);
		WriteD(0);
		WriteD(0);
		WriteD(0);
		WriteD(0);
		WriteD(0);
		WriteD(0);
		WriteD(0);
		WriteD(0);
		WriteD(0);
		WriteD(0);
		WriteD(0);
		WriteD(0);
		WriteD(0);
		WriteD(0);
		WriteD(clanModel_0.Matches);
		WriteD(clanModel_0.MatchWins);
		WriteD(clanModel_0.MatchLoses);
		WriteD(6666);
		WriteD(7777);
		WriteD(clanModel_0.TotalKills);
		WriteD(clanModel_0.TotalAssists);
		WriteD(clanModel_0.TotalDeaths);
		WriteD(clanModel_0.TotalHeadshots);
		WriteD(clanModel_0.TotalEscapes);
		WriteD(8888);
		WriteD(0);
		WriteD(0);
		WriteD(0);
		WriteD(0);
		WriteD(0);
		WriteD(0);
		WriteD(0);
		WriteD(0);
		WriteD(0);
		WriteD(0);
		WriteD(0);
		WriteD(9999);
		WriteD(0);
		WriteD(0);
		WriteD(0);
		WriteD(0);
		WriteD(0);
		WriteD(0);
		WriteD(0);
		WriteD(0);
		WriteD(0);
		WriteD(0);
		WriteD(0);
		WriteD(0);
		WriteD(0);
		WriteC(0);
		WriteC(0);
		WriteC(0);
		WriteC(0);
		WriteC(0);
		WriteC(0);
		WriteC(0);
		WriteQ(clanModel_0.BestPlayers.Exp.PlayerId);
		WriteQ(clanModel_0.BestPlayers.Exp.PlayerId);
		WriteQ(clanModel_0.BestPlayers.Wins.PlayerId);
		WriteQ(clanModel_0.BestPlayers.Wins.PlayerId);
		WriteQ(clanModel_0.BestPlayers.Kills.PlayerId);
		WriteQ(clanModel_0.BestPlayers.Kills.PlayerId);
		WriteQ(clanModel_0.BestPlayers.Headshots.PlayerId);
		WriteQ(clanModel_0.BestPlayers.Headshots.PlayerId);
		WriteQ(clanModel_0.BestPlayers.Participation.PlayerId);
		WriteQ(clanModel_0.BestPlayers.Participation.PlayerId);
		WriteC(0);
		WriteC(0);
		WriteQ(clanModel_0.BestPlayers.Exp.PlayerId);
		WriteQ(clanModel_0.BestPlayers.Wins.PlayerId);
		WriteQ(clanModel_0.BestPlayers.Kills.PlayerId);
		WriteQ(clanModel_0.BestPlayers.Headshots.PlayerId);
		WriteQ(clanModel_0.BestPlayers.Participation.PlayerId);
		WriteQ(0L);
		WriteQ(0L);
		WriteQ(0L);
	}

	private byte[] method_0(Account account_1)
	{
		using SyncServerPacket syncServerPacket = new SyncServerPacket();
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
		return syncServerPacket.ToArray();
	}
}
