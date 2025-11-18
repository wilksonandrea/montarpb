using Plugin.Core.Network;
using Server.Auth.Data.Managers;
using Server.Auth.Data.Models;
using Server.Auth.Data.Sync.Update;

namespace Server.Auth.Data.Sync.Client;

public static class ClanSync
{
	public static void Load(SyncClientPacket C)
	{
		long id = C.ReadQ();
		int num = C.ReadC();
		Account account = AccountManager.GetAccount(id, noUseDB: true);
		if (account != null)
		{
			switch (num)
			{
			case 0:
				ClanInfo.ClearList(account);
				break;
			case 1:
			{
				long playerId = C.ReadQ();
				string nickname = C.ReadS(C.ReadC());
				byte[] buffer = C.ReadB(4);
				byte rank = C.ReadC();
				Account account2 = new Account
				{
					PlayerId = playerId,
					Nickname = nickname,
					Rank = rank
				};
				account2.Status.SetData(buffer, playerId);
				ClanInfo.AddMember(account, account2);
				break;
			}
			case 2:
			{
				long playerId = C.ReadQ();
				ClanInfo.RemoveMember(account, playerId);
				break;
			}
			case 3:
			{
				int clanId = C.ReadD();
				int clanAccess = C.ReadC();
				account.ClanId = clanId;
				account.ClanAccess = clanAccess;
				break;
			}
			}
		}
	}
}
