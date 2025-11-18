namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_ROOM_GET_COLOR_NICK_ACK : GameServerPacket
{
	private readonly int int_0;

	private readonly int int_1;

	public PROTOCOL_ROOM_GET_COLOR_NICK_ACK(int int_2, int int_3)
	{
		int_0 = int_2;
		int_1 = int_3;
	}

	public override void Write()
	{
		WriteH(3628);
		WriteD(int_0);
		WriteC((byte)int_1);
	}
}
