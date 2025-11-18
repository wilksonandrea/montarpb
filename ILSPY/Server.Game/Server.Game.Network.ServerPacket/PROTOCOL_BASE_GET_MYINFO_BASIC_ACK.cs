using Plugin.Core.Models;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BASE_GET_MYINFO_BASIC_ACK : GameServerPacket
{
	private readonly Account account_0;

	private readonly ClanModel clanModel_0;

	public PROTOCOL_BASE_GET_MYINFO_BASIC_ACK(Account account_1)
	{
		account_0 = account_1;
		if (account_1 != null)
		{
			clanModel_0 = ClanManager.GetClan(account_1.ClanId);
		}
	}

	public override void Write()
	{
		WriteH(2371);
		WriteU(account_0.Nickname, 66);
		WriteD(account_0.GetRank());
		WriteD(account_0.GetRank());
		WriteD(account_0.Gold);
		WriteD(account_0.Exp);
		WriteD(0);
		WriteC(0);
		WriteQ(0L);
		WriteC((byte)account_0.AuthLevel());
		WriteC(0);
		WriteD(account_0.Tags);
		WriteD(account_0.Cash);
		WriteD(clanModel_0.Id);
		WriteD(account_0.ClanAccess);
		WriteQ(account_0.StatusId());
		WriteC((byte)account_0.CafePC);
		WriteC((byte)account_0.Country);
		WriteU(clanModel_0.Name, 34);
		WriteC((byte)clanModel_0.Rank);
		WriteC((byte)clanModel_0.GetClanUnit());
		WriteD(clanModel_0.Logo);
		WriteC((byte)clanModel_0.NameColor);
		WriteC((byte)clanModel_0.Effect);
		WriteD(account_0.Statistic.Season.Matches);
		WriteD(account_0.Statistic.Season.MatchWins);
		WriteD(account_0.Statistic.Season.MatchLoses);
		WriteD(account_0.Statistic.Season.MatchDraws);
		WriteD(account_0.Statistic.Season.KillsCount);
		WriteD(account_0.Statistic.Season.HeadshotsCount);
		WriteD(account_0.Statistic.Season.DeathsCount);
		WriteD(account_0.Statistic.Season.TotalMatchesCount);
		WriteD(account_0.Statistic.Season.TotalKillsCount);
		WriteD(account_0.Statistic.Season.EscapesCount);
		WriteD(account_0.Statistic.Season.AssistsCount);
		WriteD(account_0.Statistic.Season.MvpCount);
		WriteD(account_0.Statistic.Basic.Matches);
		WriteD(account_0.Statistic.Basic.MatchWins);
		WriteD(account_0.Statistic.Basic.MatchLoses);
		WriteD(account_0.Statistic.Basic.MatchDraws);
		WriteD(account_0.Statistic.Basic.KillsCount);
		WriteD(account_0.Statistic.Basic.HeadshotsCount);
		WriteD(account_0.Statistic.Basic.DeathsCount);
		WriteD(account_0.Statistic.Basic.TotalMatchesCount);
		WriteD(account_0.Statistic.Basic.TotalKillsCount);
		WriteD(account_0.Statistic.Basic.EscapesCount);
		WriteD(account_0.Statistic.Basic.AssistsCount);
		WriteD(account_0.Statistic.Basic.MvpCount);
	}
}
