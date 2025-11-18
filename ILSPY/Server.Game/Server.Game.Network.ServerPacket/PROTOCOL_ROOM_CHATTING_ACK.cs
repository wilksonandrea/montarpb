namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_ROOM_CHATTING_ACK : GameServerPacket
{
	private readonly string string_0;

	private readonly int int_0;

	private readonly int int_1;

	private readonly bool bool_0;

	public PROTOCOL_ROOM_CHATTING_ACK(int int_2, int int_3, bool bool_1, string string_1)
	{
		int_0 = int_2;
		int_1 = int_3;
		bool_0 = bool_1;
		string_0 = string_1;
	}

	public override void Write()
	{
		WriteH(3606);
		WriteH((short)int_0);
		WriteD(int_1);
		WriteC(bool_0 ? ((byte)1) : ((byte)0));
		WriteD(string_0.Length + 1);
		WriteN(string_0, string_0.Length + 2, "UTF-16LE");
	}
}
