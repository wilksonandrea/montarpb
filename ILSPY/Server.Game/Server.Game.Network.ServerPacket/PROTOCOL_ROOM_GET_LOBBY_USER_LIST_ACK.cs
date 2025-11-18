namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_ROOM_GET_LOBBY_USER_LIST_ACK : GameServerPacket
{
	private readonly uint uint_0;

	public PROTOCOL_ROOM_GET_LOBBY_USER_LIST_ACK(uint uint_1)
	{
		uint_0 = uint_1;
	}

	public override void Write()
	{
		WriteH(3630);
		WriteD(uint_0);
	}
}
