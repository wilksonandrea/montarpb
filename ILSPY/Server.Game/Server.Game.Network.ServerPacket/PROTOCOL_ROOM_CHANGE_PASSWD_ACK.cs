namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_ROOM_CHANGE_PASSWD_ACK : GameServerPacket
{
	private readonly string string_0;

	public PROTOCOL_ROOM_CHANGE_PASSWD_ACK(string string_1)
	{
		string_0 = string_1;
	}

	public override void Write()
	{
		WriteH(3603);
		WriteS(string_0, 4);
	}
}
