namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_ROOM_REQUEST_MAIN_CHANGE_WHO_ACK : GameServerPacket
{
	private readonly uint uint_0;

	public PROTOCOL_ROOM_REQUEST_MAIN_CHANGE_WHO_ACK(uint uint_1)
	{
		uint_0 = uint_1;
	}

	public PROTOCOL_ROOM_REQUEST_MAIN_CHANGE_WHO_ACK(int int_0)
	{
		uint_0 = (uint)int_0;
	}

	public override void Write()
	{
		WriteH(3616);
		WriteD(uint_0);
	}
}
