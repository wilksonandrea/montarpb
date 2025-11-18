using Server.Game.Data.Managers;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_GM_LOG_LOBBY_ACK : GameServerPacket
{
	private readonly Account account_0;

	private readonly uint uint_0;

	public PROTOCOL_GM_LOG_LOBBY_ACK(uint uint_1, long long_0)
	{
		uint_0 = uint_1;
		account_0 = AccountManager.GetAccount(long_0, noUseDB: true);
	}

	public override void Write()
	{
		WriteH(2685);
		WriteD(uint_0);
		WriteQ(account_0.PlayerId);
	}
}
