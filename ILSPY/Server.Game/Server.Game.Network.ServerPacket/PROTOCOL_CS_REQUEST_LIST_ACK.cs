namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_CS_REQUEST_LIST_ACK : GameServerPacket
{
	private readonly int int_0;

	private readonly int int_1;

	private readonly int int_2;

	private readonly byte[] byte_0;

	public PROTOCOL_CS_REQUEST_LIST_ACK(int int_3, int int_4, int int_5, byte[] byte_1)
	{
		int_0 = int_3;
		int_2 = int_4;
		int_1 = int_5;
		byte_0 = byte_1;
	}

	public PROTOCOL_CS_REQUEST_LIST_ACK(int int_3)
	{
		int_0 = int_3;
	}

	public override void Write()
	{
		WriteH(819);
		WriteD(int_0);
		if (int_0 >= 0)
		{
			WriteC((byte)int_1);
			WriteC((byte)int_2);
			WriteB(byte_0);
		}
	}
}
