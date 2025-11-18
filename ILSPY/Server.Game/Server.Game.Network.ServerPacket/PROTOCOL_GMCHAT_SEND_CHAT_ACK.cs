using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_GMCHAT_SEND_CHAT_ACK : GameServerPacket
{
	private readonly Account account_0;

	private readonly string string_0;

	private readonly string string_1;

	private readonly string string_2;

	public PROTOCOL_GMCHAT_SEND_CHAT_ACK(string string_3, string string_4, string string_5, Account account_1)
	{
		string_0 = string_3;
		string_2 = string_4;
		string_1 = string_5;
		account_0 = account_1;
	}

	public override void Write()
	{
		WriteH(6660);
		WriteH(0);
		WriteD(0);
		WriteH((byte)string_2.Length);
		WriteU(string_2, string_2.Length * 2);
		WriteC((byte)string_1.Length);
		WriteU(string_1, string_1.Length * 2);
	}
}
