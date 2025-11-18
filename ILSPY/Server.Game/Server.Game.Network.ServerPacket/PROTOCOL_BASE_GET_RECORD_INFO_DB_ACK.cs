using Plugin.Core.Models;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BASE_GET_RECORD_INFO_DB_ACK : GameServerPacket
{
	private readonly PlayerStatistic playerStatistic_0;

	public PROTOCOL_BASE_GET_RECORD_INFO_DB_ACK(Account account_0)
	{
		playerStatistic_0 = account_0.Statistic;
	}

	public override void Write()
	{
		WriteH(2351);
		WriteD(playerStatistic_0.Season.Matches);
		WriteD(playerStatistic_0.Season.MatchWins);
		WriteD(playerStatistic_0.Season.MatchLoses);
		WriteD(playerStatistic_0.Season.MatchDraws);
		WriteD(playerStatistic_0.Season.KillsCount);
		WriteD(playerStatistic_0.Season.HeadshotsCount);
		WriteD(playerStatistic_0.Season.DeathsCount);
		WriteD(playerStatistic_0.Season.TotalMatchesCount);
		WriteD(playerStatistic_0.Season.TotalKillsCount);
		WriteD(playerStatistic_0.Season.EscapesCount);
		WriteD(playerStatistic_0.Season.AssistsCount);
		WriteD(playerStatistic_0.Season.MvpCount);
		WriteD(playerStatistic_0.Basic.Matches);
		WriteD(playerStatistic_0.Basic.MatchWins);
		WriteD(playerStatistic_0.Basic.MatchLoses);
		WriteD(playerStatistic_0.Basic.MatchDraws);
		WriteD(playerStatistic_0.Basic.KillsCount);
		WriteD(playerStatistic_0.Basic.HeadshotsCount);
		WriteD(playerStatistic_0.Basic.DeathsCount);
		WriteD(playerStatistic_0.Basic.TotalMatchesCount);
		WriteD(playerStatistic_0.Basic.TotalKillsCount);
		WriteD(playerStatistic_0.Basic.EscapesCount);
		WriteD(playerStatistic_0.Basic.AssistsCount);
		WriteD(playerStatistic_0.Basic.MvpCount);
	}
}
