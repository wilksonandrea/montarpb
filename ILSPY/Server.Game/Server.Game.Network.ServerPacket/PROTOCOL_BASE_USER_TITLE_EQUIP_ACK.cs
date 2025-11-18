namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BASE_USER_TITLE_EQUIP_ACK : GameServerPacket
{
	private readonly uint uint_0;

	private readonly int int_0;

	private readonly int int_1;

	public PROTOCOL_BASE_USER_TITLE_EQUIP_ACK(uint uint_1, int int_2, int int_3)
	{
		uint_0 = uint_1;
		int_0 = int_2;
		int_1 = int_3;
	}

	public override void Write()
	{
		WriteH(2379);
		WriteD(uint_0);
		WriteC((byte)int_0);
		WriteC((byte)int_1);
	}
}
