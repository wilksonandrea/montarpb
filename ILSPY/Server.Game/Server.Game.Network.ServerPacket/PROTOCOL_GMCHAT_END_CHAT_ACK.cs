using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_GMCHAT_END_CHAT_ACK : GameServerPacket
{
	private readonly Account account_0;

	private readonly uint uint_0;

	public PROTOCOL_GMCHAT_END_CHAT_ACK(uint uint_1, Account account_1)
	{
		uint_0 = uint_1;
		account_0 = account_1;
	}

	public override void Write()
	{
		WriteH(6662);
		WriteH(0);
		WriteD(uint_0);
	}
}
