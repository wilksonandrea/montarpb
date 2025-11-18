namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BASE_SELECT_CHANNEL_ACK : GameServerPacket
{
	private readonly int int_0;

	private readonly ushort ushort_0;

	private readonly uint uint_0;

	public PROTOCOL_BASE_SELECT_CHANNEL_ACK(uint uint_1, int int_1, int int_2)
	{
		uint_0 = uint_1;
		int_0 = int_1;
		ushort_0 = (ushort)int_2;
	}

	public override void Write()
	{
		WriteH(2335);
		WriteD(0);
		WriteD(uint_0);
		if (uint_0 == 0)
		{
			WriteD(int_0);
			WriteH(ushort_0);
		}
	}
}
