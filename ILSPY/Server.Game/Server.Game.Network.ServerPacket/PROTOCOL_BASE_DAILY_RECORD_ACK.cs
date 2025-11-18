using Plugin.Core.Models;

namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BASE_DAILY_RECORD_ACK : GameServerPacket
{
	private readonly StatisticDaily statisticDaily_0;

	private readonly byte byte_0;

	private readonly uint uint_0;

	public PROTOCOL_BASE_DAILY_RECORD_ACK(StatisticDaily statisticDaily_1, byte byte_1, uint uint_1)
	{
		statisticDaily_0 = statisticDaily_1;
		byte_0 = byte_1;
		uint_0 = uint_1;
	}

	public override void Write()
	{
		WriteH(2415);
		WriteH((ushort)statisticDaily_0.Matches);
		WriteH((ushort)statisticDaily_0.MatchWins);
		WriteH((ushort)statisticDaily_0.MatchLoses);
		WriteH((ushort)statisticDaily_0.MatchDraws);
		WriteH((ushort)statisticDaily_0.KillsCount);
		WriteH((ushort)statisticDaily_0.HeadshotsCount);
		WriteH((ushort)statisticDaily_0.DeathsCount);
		WriteD(statisticDaily_0.ExpGained);
		WriteD(statisticDaily_0.PointGained);
		WriteD(statisticDaily_0.Playtime);
		WriteC(byte_0);
		WriteD(uint_0);
	}
}
