using System;

namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_CLAN_WAR_MATCH_TEAM_COUNT_ACK : GameServerPacket
{
	private readonly int int_0;

	public PROTOCOL_CLAN_WAR_MATCH_TEAM_COUNT_ACK(int int_1)
	{
		int_0 = int_1;
	}

	public override void Write()
	{
		WriteH(6915);
		WriteH((short)int_0);
		WriteC(13);
		WriteH((short)Math.Ceiling((double)int_0 / 13.0));
	}
}
