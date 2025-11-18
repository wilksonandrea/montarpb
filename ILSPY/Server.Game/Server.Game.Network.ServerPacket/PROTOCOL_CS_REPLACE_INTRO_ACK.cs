using Plugin.Core.Enums;

namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_CS_REPLACE_INTRO_ACK : GameServerPacket
{
	private readonly EventErrorEnum eventErrorEnum_0;

	public PROTOCOL_CS_REPLACE_INTRO_ACK(EventErrorEnum eventErrorEnum_1)
	{
		eventErrorEnum_0 = eventErrorEnum_1;
	}

	public override void Write()
	{
		WriteH(861);
		WriteD((uint)eventErrorEnum_0);
	}
}
