using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_CLAN_WAR_JOIN_ROOM_ACK : GameServerPacket
{
	private readonly MatchModel matchModel_0;

	private readonly int int_0;

	private readonly int int_1;

	public PROTOCOL_CLAN_WAR_JOIN_ROOM_ACK(MatchModel matchModel_1, int int_2, int int_3)
	{
		matchModel_0 = matchModel_1;
		int_0 = int_2;
		int_1 = int_3;
	}

	public override void Write()
	{
		WriteH(1566);
		WriteD(int_0);
		WriteH((ushort)int_1);
		WriteH((ushort)matchModel_0.GetServerInfo());
	}
}
