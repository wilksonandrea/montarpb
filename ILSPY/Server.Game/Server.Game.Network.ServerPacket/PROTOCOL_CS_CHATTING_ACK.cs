using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_CS_CHATTING_ACK : GameServerPacket
{
	private readonly string string_0;

	private readonly Account account_0;

	private readonly int int_0;

	private readonly int int_1;

	public PROTOCOL_CS_CHATTING_ACK(string string_1, Account account_1)
	{
		string_0 = string_1;
		account_0 = account_1;
	}

	public PROTOCOL_CS_CHATTING_ACK(int int_2, int int_3)
	{
		int_0 = int_2;
		int_1 = int_3;
	}

	public override void Write()
	{
		WriteH(855);
		if (int_0 == 0)
		{
			WriteC((byte)(account_0.Nickname.Length + 1));
			WriteN(account_0.Nickname, account_0.Nickname.Length + 2, "UTF-16LE");
			WriteC(account_0.UseChatGM() ? ((byte)1) : ((byte)0));
			WriteC((byte)account_0.NickColor);
			WriteC((byte)(string_0.Length + 1));
			WriteN(string_0, string_0.Length + 2, "UTF-16LE");
		}
		else
		{
			WriteD(int_1);
		}
	}
}
