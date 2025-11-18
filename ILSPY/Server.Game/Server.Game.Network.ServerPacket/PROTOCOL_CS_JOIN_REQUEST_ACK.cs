namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_CS_JOIN_REQUEST_ACK : GameServerPacket
{
	private readonly int int_0;

	private readonly uint uint_0;

	public PROTOCOL_CS_JOIN_REQUEST_ACK(uint uint_1, int int_1)
	{
		uint_0 = uint_1;
		int_0 = int_1;
	}

	public override void Write()
	{
		WriteH(813);
		WriteD(uint_0);
		if (uint_0 == 0)
		{
			WriteD(int_0);
		}
	}
}
