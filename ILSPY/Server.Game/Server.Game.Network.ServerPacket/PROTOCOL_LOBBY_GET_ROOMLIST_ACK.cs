namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_LOBBY_GET_ROOMLIST_ACK : GameServerPacket
{
	private readonly int int_0;

	private readonly int int_1;

	private readonly int int_2;

	private readonly int int_3;

	private readonly int int_4;

	private readonly int int_5;

	private readonly byte[] byte_0;

	private readonly byte[] byte_1;

	public PROTOCOL_LOBBY_GET_ROOMLIST_ACK(int int_6, int int_7, int int_8, int int_9, int int_10, int int_11, byte[] byte_2, byte[] byte_3)
	{
		int_3 = int_6;
		int_2 = int_7;
		int_0 = int_8;
		int_1 = int_9;
		byte_0 = byte_2;
		byte_1 = byte_3;
		int_4 = int_10;
		int_5 = int_11;
	}

	public override void Write()
	{
		WriteH(2588);
		WriteD(int_3);
		WriteD(int_0);
		WriteD(int_4);
		WriteB(byte_0);
		WriteD(int_2);
		WriteD(int_1);
		WriteD(int_5);
		WriteB(byte_1);
	}
}
