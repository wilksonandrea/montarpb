namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK : GameServerPacket
{
	private readonly string string_0;

	public PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK(string string_1)
	{
		string_0 = string_1;
	}

	public override void Write()
	{
		WriteH(3079);
		WriteH(0);
		WriteD(0);
		WriteH((ushort)string_0.Length);
		WriteN(string_0, string_0.Length, "UTF-16LE");
		WriteD(2);
	}
}
