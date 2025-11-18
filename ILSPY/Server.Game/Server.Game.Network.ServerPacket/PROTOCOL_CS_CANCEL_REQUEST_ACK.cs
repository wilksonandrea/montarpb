namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_CS_CANCEL_REQUEST_ACK : GameServerPacket
{
	private readonly uint uint_0;

	public PROTOCOL_CS_CANCEL_REQUEST_ACK(uint uint_1)
	{
		uint_0 = uint_1;
	}

	public override void Write()
	{
		WriteH(815);
		WriteD(uint_0);
	}
}
