using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_CLAN_WAR_CREATE_TEAM_ACK : GameServerPacket
{
	private readonly uint uint_0;

	private readonly MatchModel matchModel_0;

	public PROTOCOL_CLAN_WAR_CREATE_TEAM_ACK(uint uint_1, MatchModel matchModel_1 = null)
	{
		uint_0 = uint_1;
		matchModel_0 = matchModel_1;
	}

	public override void Write()
	{
		WriteH(6919);
		WriteD(uint_0);
		if (uint_0 == 0)
		{
			WriteH((short)matchModel_0.MatchId);
			WriteH((short)matchModel_0.GetServerInfo());
			WriteH((short)matchModel_0.GetServerInfo());
			WriteC((byte)matchModel_0.State);
			WriteC((byte)matchModel_0.FriendId);
			WriteC((byte)matchModel_0.Training);
			WriteC((byte)matchModel_0.GetCountPlayers());
			WriteD(matchModel_0.Leader);
			WriteC(0);
		}
	}
}
