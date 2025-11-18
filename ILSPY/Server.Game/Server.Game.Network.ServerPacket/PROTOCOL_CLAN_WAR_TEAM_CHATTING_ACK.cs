namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_CLAN_WAR_TEAM_CHATTING_ACK : GameServerPacket
{
	private readonly int int_0;

	private readonly int int_1;

	private readonly string string_0;

	private readonly string string_1;

	public PROTOCOL_CLAN_WAR_TEAM_CHATTING_ACK(string string_2, string string_3)
	{
		string_1 = string_2;
		string_0 = string_3;
	}

	public PROTOCOL_CLAN_WAR_TEAM_CHATTING_ACK(int int_2, int int_3)
	{
		int_0 = int_2;
		int_1 = int_3;
	}

	public override void Write()
	{
		WriteH(6929);
		WriteC((byte)int_0);
		if (int_0 == 0)
		{
			WriteC((byte)(string_1.Length + 1));
			WriteN(string_1, string_1.Length + 2, "UTF-16LE");
			WriteC((byte)(string_0.Length + 1));
			WriteN(string_0, string_0.Length + 2, "UTF-16LE");
		}
		else
		{
			WriteD(int_1);
		}
	}
}
