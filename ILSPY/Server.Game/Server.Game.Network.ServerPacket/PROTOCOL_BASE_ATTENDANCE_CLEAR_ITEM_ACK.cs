using Plugin.Core.Enums;

namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BASE_ATTENDANCE_CLEAR_ITEM_ACK : GameServerPacket
{
	private readonly EventErrorEnum eventErrorEnum_0;

	public PROTOCOL_BASE_ATTENDANCE_CLEAR_ITEM_ACK(EventErrorEnum eventErrorEnum_1)
	{
		eventErrorEnum_0 = eventErrorEnum_1;
	}

	public override void Write()
	{
		WriteH(2339);
		WriteD((uint)eventErrorEnum_0);
	}
}
