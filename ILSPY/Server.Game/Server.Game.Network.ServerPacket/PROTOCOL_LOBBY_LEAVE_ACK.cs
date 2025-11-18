namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_LOBBY_LEAVE_ACK : GameServerPacket
{
	private readonly uint uint_0;

	public PROTOCOL_LOBBY_LEAVE_ACK(uint uint_1)
	{
		uint_0 = uint_1;
	}

	public override void Write()
	{
		WriteH(2562);
		WriteD(uint_0);
	}
}
