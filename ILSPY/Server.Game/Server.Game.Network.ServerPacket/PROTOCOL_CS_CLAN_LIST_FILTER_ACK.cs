namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_CS_CLAN_LIST_FILTER_ACK : GameServerPacket
{
	private readonly byte byte_0;

	private readonly int int_0;

	private readonly byte[] byte_1;

	public PROTOCOL_CS_CLAN_LIST_FILTER_ACK(byte byte_2, int int_1, byte[] byte_3)
	{
		byte_0 = byte_2;
		int_0 = int_1;
		byte_1 = byte_3;
	}

	public override void Write()
	{
		WriteH(998);
		WriteH(0);
		WriteC(0);
		WriteC(byte_0);
		WriteH((ushort)int_0);
		WriteD(int_0);
		WriteB(byte_1);
	}
}
