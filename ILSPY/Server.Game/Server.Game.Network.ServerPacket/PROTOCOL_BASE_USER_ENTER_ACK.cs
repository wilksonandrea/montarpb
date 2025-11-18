namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BASE_USER_ENTER_ACK : GameServerPacket
{
	private readonly uint uint_0;

	public PROTOCOL_BASE_USER_ENTER_ACK(uint uint_1)
	{
		uint_0 = uint_1;
	}

	public override void Write()
	{
		WriteH(2331);
		WriteD(uint_0);
	}
}
