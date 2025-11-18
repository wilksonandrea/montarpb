namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BASE_USER_TITLE_CHANGE_ACK : GameServerPacket
{
	private readonly int int_0;

	private readonly uint uint_0;

	public PROTOCOL_BASE_USER_TITLE_CHANGE_ACK(uint uint_1, int int_1)
	{
		uint_0 = uint_1;
		int_0 = int_1;
	}

	public override void Write()
	{
		WriteH(2377);
		WriteD(uint_0);
		WriteD(int_0);
	}
}
