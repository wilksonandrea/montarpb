namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_AUTH_SEND_WHISPER_ACK : GameServerPacket
{
	private readonly string string_0;

	private readonly string string_1;

	private readonly uint uint_0;

	private readonly int int_0;

	private readonly int int_1;

	public PROTOCOL_AUTH_SEND_WHISPER_ACK(string string_2, string string_3, uint uint_1)
	{
		string_0 = string_2;
		string_1 = string_3;
		uint_0 = uint_1;
	}

	public override void Write()
	{
		WriteH(1827);
		WriteD(uint_0);
		WriteC((byte)int_0);
		WriteU(string_0, 66);
		if (uint_0 == 0)
		{
			WriteH((ushort)(string_1.Length + 1));
			WriteN(string_1, string_1.Length + 2, "UTF-16LE");
		}
	}
}
