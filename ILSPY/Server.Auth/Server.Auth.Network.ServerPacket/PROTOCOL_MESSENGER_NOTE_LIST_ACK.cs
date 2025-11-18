namespace Server.Auth.Network.ServerPacket;

public class PROTOCOL_MESSENGER_NOTE_LIST_ACK : AuthServerPacket
{
	private readonly int int_0;

	private readonly int int_1;

	private readonly byte[] byte_0;

	private readonly byte[] byte_1;

	public PROTOCOL_MESSENGER_NOTE_LIST_ACK(int int_2, int int_3, byte[] byte_2, byte[] byte_3)
	{
		int_1 = int_2;
		int_0 = int_3;
		byte_0 = byte_2;
		byte_1 = byte_3;
	}

	public override void Write()
	{
		WriteH(1925);
		WriteC((byte)int_0);
		WriteC((byte)int_1);
		WriteB(byte_0);
		WriteB(byte_1);
	}
}
