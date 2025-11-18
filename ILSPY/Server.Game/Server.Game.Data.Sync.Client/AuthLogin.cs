using Plugin.Core.Network;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Data.Sync.Client;

public class AuthLogin
{
	public static void Load(SyncClientPacket C)
	{
		Account account = AccountManager.GetAccount(C.ReadQ(), noUseDB: true);
		if (account != null)
		{
			account.SendPacket(new PROTOCOL_AUTH_ACCOUNT_KICK_ACK(1));
			account.SendPacket(new PROTOCOL_SERVER_MESSAGE_ERROR_ACK(2147487744u));
			account.Close(1000);
		}
	}
}
