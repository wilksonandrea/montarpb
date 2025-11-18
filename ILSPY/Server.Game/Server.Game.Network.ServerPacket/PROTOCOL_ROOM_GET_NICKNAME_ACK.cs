namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_ROOM_GET_NICKNAME_ACK : GameServerPacket
{
	private readonly int int_0;

	private readonly string string_0;

	public PROTOCOL_ROOM_GET_NICKNAME_ACK(int int_1, string string_1)
	{
		int_0 = int_1;
		string_0 = string_1;
	}

	public override void Write()
	{
		WriteH(3599);
		WriteD(int_0);
		WriteU(string_0, 66);
	}
}
