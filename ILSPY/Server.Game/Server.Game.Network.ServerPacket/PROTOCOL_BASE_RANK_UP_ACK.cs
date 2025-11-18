namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BASE_RANK_UP_ACK : GameServerPacket
{
	private readonly int int_0;

	private readonly int int_1;

	public PROTOCOL_BASE_RANK_UP_ACK(int int_2, int int_3)
	{
		int_0 = int_2;
		int_1 = int_3;
	}

	public override void Write()
	{
		WriteH(2343);
		WriteD(int_0);
		WriteD(int_0);
		WriteD(int_1);
	}
}
