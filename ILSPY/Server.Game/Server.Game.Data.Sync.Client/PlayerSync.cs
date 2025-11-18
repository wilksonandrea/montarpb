using Plugin.Core.Network;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;

namespace Server.Game.Data.Sync.Client;

public static class PlayerSync
{
	public static void Load(SyncClientPacket C)
	{
		long id = C.ReadQ();
		int num = C.ReadC();
		int rank = C.ReadC();
		int gold = C.ReadD();
		int cash = C.ReadD();
		int tags = C.ReadD();
		Account account = AccountManager.GetAccount(id, noUseDB: true);
		if (account != null && num == 0)
		{
			account.Rank = rank;
			account.Gold = gold;
			account.Cash = cash;
			account.Tags = tags;
		}
	}
}
