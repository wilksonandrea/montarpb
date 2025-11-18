namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_SHOP_PLUS_POINT_ACK : GameServerPacket
{
	private readonly int int_0;

	private readonly int int_1;

	private readonly int int_2;

	public PROTOCOL_SHOP_PLUS_POINT_ACK(int int_3, int int_4, int int_5)
	{
		int_1 = int_3;
		int_0 = int_4;
		int_2 = int_5;
	}

	public override void Write()
	{
		WriteH(1072);
		WriteH(0);
		WriteC((byte)int_2);
		WriteD(int_0);
		WriteD(int_1);
	}
}
