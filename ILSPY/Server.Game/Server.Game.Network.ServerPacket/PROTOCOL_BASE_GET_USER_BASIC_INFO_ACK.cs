namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BASE_GET_USER_BASIC_INFO_ACK : GameServerPacket
{
	private readonly uint uint_0;

	public PROTOCOL_BASE_GET_USER_BASIC_INFO_ACK(uint uint_1)
	{
		uint_0 = uint_1;
	}

	public override void Write()
	{
		WriteH(2427);
		WriteH(0);
		WriteD(uint_0);
	}
}
