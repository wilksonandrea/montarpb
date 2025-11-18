namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_CS_MEMBER_LIST_ACK : GameServerPacket
{
	private readonly uint uint_0;

	private readonly byte[] byte_0;

	private readonly byte byte_1;

	private readonly byte byte_2;

	public PROTOCOL_CS_MEMBER_LIST_ACK(uint uint_1, byte byte_3, byte byte_4, byte[] byte_5)
	{
		uint_0 = uint_1;
		byte_1 = byte_3;
		byte_2 = byte_4;
		byte_0 = byte_5;
	}

	public override void Write()
	{
		WriteH(805);
		WriteD(uint_0);
		if (uint_0 == 0)
		{
			WriteC(byte_1);
			WriteC(byte_2);
			WriteB(byte_0);
		}
	}
}
