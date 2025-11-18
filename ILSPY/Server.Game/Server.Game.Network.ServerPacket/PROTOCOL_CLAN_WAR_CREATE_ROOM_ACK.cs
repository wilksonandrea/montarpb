using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_CLAN_WAR_CREATE_ROOM_ACK : GameServerPacket
{
	public readonly MatchModel match;

	public PROTOCOL_CLAN_WAR_CREATE_ROOM_ACK(MatchModel matchModel_0)
	{
		match = matchModel_0;
	}

	public override void Write()
	{
		WriteH(1564);
		WriteH((short)match.MatchId);
		WriteD(match.GetServerInfo());
		WriteH((short)match.GetServerInfo());
		WriteC(10);
	}
}
