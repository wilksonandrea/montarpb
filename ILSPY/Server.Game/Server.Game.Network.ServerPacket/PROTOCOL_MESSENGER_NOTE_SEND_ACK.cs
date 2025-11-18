namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_MESSENGER_NOTE_SEND_ACK : GameServerPacket
{
	private readonly uint uint_0;

	public PROTOCOL_MESSENGER_NOTE_SEND_ACK(uint uint_1)
	{
		uint_0 = uint_1;
	}

	public override void Write()
	{
		WriteH(1922);
		WriteD(uint_0);
	}
}
