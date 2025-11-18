namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_SERVER_MESSAGE_ERROR_ACK : GameServerPacket
{
	private readonly uint uint_0;

	public PROTOCOL_SERVER_MESSAGE_ERROR_ACK(uint uint_1)
	{
		uint_0 = uint_1;
	}

	public override void Write()
	{
		WriteH(3078);
		WriteD(uint_0);
	}
}
