namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_AUTH_RECV_WHISPER_ACK : GameServerPacket
{
	private readonly string string_0;

	private readonly string string_1;

	private readonly bool bool_0;

	public PROTOCOL_AUTH_RECV_WHISPER_ACK(string string_2, string string_3, bool bool_1)
	{
		string_0 = string_2;
		string_1 = string_3;
		bool_0 = bool_1;
	}

	public override void Write()
	{
		WriteH(1830);
		WriteU(string_0, 66);
		WriteC(bool_0 ? ((byte)1) : ((byte)0));
		WriteC(0);
		WriteH((ushort)(string_1.Length + 1));
		WriteN(string_1, string_1.Length + 2, "UTF-16LE");
		WriteC(0);
	}
}
