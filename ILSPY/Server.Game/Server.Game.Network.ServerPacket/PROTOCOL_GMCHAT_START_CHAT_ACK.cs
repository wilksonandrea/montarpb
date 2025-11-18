using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_GMCHAT_START_CHAT_ACK : GameServerPacket
{
	private readonly Account account_0;

	private readonly uint uint_0;

	public PROTOCOL_GMCHAT_START_CHAT_ACK(uint uint_1, Account account_1)
	{
		uint_0 = uint_1;
		account_0 = account_1;
	}

	public override void Write()
	{
		WriteH(6658);
		WriteH(0);
		WriteD(uint_0);
		if (uint_0 == 0)
		{
			WriteC((byte)(account_0.Nickname.Length + 1));
			WriteN(account_0.Nickname, account_0.Nickname.Length + 2, "UTF-16LE");
			WriteQ(account_0.PlayerId);
		}
	}
}
