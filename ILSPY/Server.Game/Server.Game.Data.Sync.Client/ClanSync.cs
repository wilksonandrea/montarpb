using Plugin.Core.Network;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;

namespace Server.Game.Data.Sync.Client;

public static class ClanSync
{
	public static void Load(SyncClientPacket C)
	{
		long id = C.ReadQ();
		int num = C.ReadC();
		Account account = AccountManager.GetAccount(id, noUseDB: true);
		if (account != null && num == 3)
		{
			int clanId = C.ReadD();
			int clanAccess = C.ReadC();
			account.ClanId = clanId;
			account.ClanAccess = clanAccess;
		}
	}
}
