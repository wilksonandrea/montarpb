using System;

namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_CS_CLAN_MATCH_RESULT_CONTEXT_ACK : GameServerPacket
{
	private readonly int int_0;

	public PROTOCOL_CS_CLAN_MATCH_RESULT_CONTEXT_ACK(int int_1)
	{
		int_0 = int_1;
	}

	public override void Write()
	{
		WriteH(1955);
		WriteC((byte)int_0);
		WriteC(13);
		WriteC((byte)Math.Ceiling((double)int_0 / 13.0));
	}
}
