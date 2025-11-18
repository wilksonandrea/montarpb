using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_CS_PAGE_CHATTING_ACK : GameServerPacket
{
	private readonly string string_0;

	private readonly string string_1;

	private readonly int int_0;

	private readonly int int_1;

	private readonly int int_2;

	private readonly bool bool_0;

	public PROTOCOL_CS_PAGE_CHATTING_ACK(Account account_0, string string_2)
	{
		string_0 = account_0.Nickname;
		string_1 = string_2;
		bool_0 = account_0.UseChatGM();
		int_2 = account_0.NickColor;
	}

	public PROTOCOL_CS_PAGE_CHATTING_ACK(int int_3, int int_4)
	{
		int_0 = int_3;
		int_1 = int_4;
	}

	public override void Write()
	{
		WriteH(887);
		WriteC((byte)int_0);
		if (int_0 == 0)
		{
			WriteC((byte)(string_0.Length + 1));
			WriteN(string_0, string_0.Length + 2, "UTF-16LE");
			WriteC(bool_0 ? ((byte)1) : ((byte)0));
			WriteC((byte)int_2);
			WriteC((byte)(string_1.Length + 1));
			WriteN(string_1, string_1.Length + 2, "UTF-16LE");
		}
		else
		{
			WriteD(int_1);
		}
	}
}
