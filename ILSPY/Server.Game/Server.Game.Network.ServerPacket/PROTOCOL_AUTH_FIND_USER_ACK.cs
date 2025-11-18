namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_AUTH_FIND_USER_ACK : GameServerPacket
{
	private readonly uint uint_0;

	public PROTOCOL_AUTH_FIND_USER_ACK(uint uint_1)
	{
		uint_0 = uint_1;
	}

	public override void Write()
	{
		WriteH(1834);
		WriteH(0);
		WriteD(uint_0);
	}
}
