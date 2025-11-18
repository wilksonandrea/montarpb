using Plugin.Core.Models;
using Plugin.Core.Network;
using Server.Auth.Data.Managers;
using Server.Auth.Data.Models;

namespace Server.Auth.Data.Sync.Client;

public static class FriendSync
{
	public static void Load(SyncClientPacket C)
	{
		long id = C.ReadQ();
		int num = C.ReadC();
		long num2 = C.ReadQ();
		FriendModel friendModel = null;
		if (num <= 1)
		{
			int state = C.ReadC();
			bool removed = C.ReadC() == 1;
			friendModel = new FriendModel(num2)
			{
				State = state,
				Removed = removed
			};
		}
		if (friendModel == null && num <= 1)
		{
			return;
		}
		Account account = AccountManager.GetAccount(id, noUseDB: true);
		if (account != null)
		{
			if (num <= 1)
			{
				friendModel.Info.Nickname = account.Nickname;
				friendModel.Info.Rank = account.Rank;
				friendModel.Info.IsOnline = account.IsOnline;
				friendModel.Info.Status = account.Status;
			}
			switch (num)
			{
			case 0:
				account.Friend.AddFriend(friendModel);
				break;
			case 1:
				account.Friend.GetFriend(num2);
				break;
			case 2:
				account.Friend.RemoveFriend(num2);
				break;
			}
		}
	}
}
