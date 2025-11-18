using Plugin.Core.Enums;

namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_SERVER_MESSAGE_KICK_BATTLE_PLAYER_ACK : GameServerPacket
{
	private readonly EventErrorEnum eventErrorEnum_0;

	public PROTOCOL_SERVER_MESSAGE_KICK_BATTLE_PLAYER_ACK(EventErrorEnum eventErrorEnum_1)
	{
		eventErrorEnum_0 = eventErrorEnum_1;
	}

	public override void Write()
	{
		WriteH(3076);
		WriteD((uint)eventErrorEnum_0);
	}
}
