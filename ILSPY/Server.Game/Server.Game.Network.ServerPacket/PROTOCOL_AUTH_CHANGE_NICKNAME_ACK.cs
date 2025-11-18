namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_AUTH_CHANGE_NICKNAME_ACK : GameServerPacket
{
	private readonly string string_0;

	public PROTOCOL_AUTH_CHANGE_NICKNAME_ACK(string string_1)
	{
		string_0 = string_1;
	}

	public override void Write()
	{
		WriteH(1836);
		WriteC((byte)string_0.Length);
		WriteU(string_0, string_0.Length * 2);
	}
}
