namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_LOBBY_ENTER_ACK : GameServerPacket
{
	private readonly uint uint_0;

	public PROTOCOL_LOBBY_ENTER_ACK(uint uint_1)
	{
		uint_0 = uint_1;
	}

	public override void Write()
	{
		WriteH(2584);
		WriteH(0);
		WriteD(uint_0);
		WriteC(0);
		WriteQ(0L);
	}
}
