namespace Server.Auth.Network.ServerPacket;

public class PROTOCOL_BASE_MISSION_CARD_INFO_STREAM_ACK : AuthServerPacket
{
	private readonly byte[] byte_0;

	private readonly byte byte_1;

	private readonly short short_0;

	public PROTOCOL_BASE_MISSION_CARD_INFO_STREAM_ACK(byte[] byte_2, short short_1, byte byte_3)
	{
		byte_0 = byte_2;
		short_0 = short_1;
		byte_1 = byte_3;
	}

	public override void Write()
	{
		WriteH(2517);
		WriteH(short_0);
		WriteC(byte_1);
		WriteB(byte_0);
	}
}
