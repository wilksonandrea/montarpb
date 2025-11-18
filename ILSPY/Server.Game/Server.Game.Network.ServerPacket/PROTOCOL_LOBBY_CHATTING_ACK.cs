using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_LOBBY_CHATTING_ACK : GameServerPacket
{
	private readonly string string_0;

	private readonly string string_1;

	private readonly int int_0;

	private readonly int int_1;

	private readonly bool bool_0;

	public PROTOCOL_LOBBY_CHATTING_ACK(Account account_0, string string_2, bool bool_1 = false)
	{
		if (!bool_1)
		{
			int_1 = account_0.NickColor;
			bool_0 = account_0.UseChatGM();
		}
		else
		{
			bool_0 = true;
		}
		string_0 = account_0.Nickname;
		int_0 = account_0.GetSessionId();
		string_1 = string_2;
	}

	public PROTOCOL_LOBBY_CHATTING_ACK(string string_2, int int_2, int int_3, bool bool_1, string string_3)
	{
		string_0 = string_2;
		int_0 = int_2;
		int_1 = int_3;
		bool_0 = bool_1;
		string_1 = string_3;
	}

	public override void Write()
	{
		WriteH(2571);
		WriteD(int_0);
		WriteC((byte)(string_0.Length + 1));
		WriteN(string_0, string_0.Length + 2, "UTF-16LE");
		WriteC((byte)int_1);
		WriteC(bool_0 ? ((byte)1) : ((byte)0));
		WriteH((ushort)(string_1.Length + 1));
		WriteN(string_1, string_1.Length + 2, "UTF-16LE");
	}
}
