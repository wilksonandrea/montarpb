namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_ROOM_CHANGE_OBSERVER_SLOT_ACK : GameServerPacket
{
	private readonly int int_0;

	public PROTOCOL_ROOM_CHANGE_OBSERVER_SLOT_ACK(int int_1)
	{
		int_0 = int_1;
	}

	public override void Write()
	{
		WriteH(3650);
		WriteD(int_0);
	}
}
