using System;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Server.Auth.Data.Managers;
using Server.Auth.Data.Models;

namespace Server.Auth.Data.Sync.Client
{
	// Token: 0x02000056 RID: 86
	public static class FriendSync
	{
		// Token: 0x0600013A RID: 314 RVA: 0x0000B800 File Offset: 0x00009A00
		public static void Load(SyncClientPacket C)
		{
			long num = C.ReadQ();
			int num2 = (int)C.ReadC();
			long num3 = C.ReadQ();
			FriendModel friendModel = null;
			if (num2 <= 1)
			{
				int num4 = (int)C.ReadC();
				bool flag = C.ReadC() == 1;
				friendModel = new FriendModel(num3)
				{
					State = num4,
					Removed = flag
				};
			}
			if (friendModel == null && num2 <= 1)
			{
				return;
			}
			Account account = AccountManager.GetAccount(num, true);
			if (account != null)
			{
				if (num2 <= 1)
				{
					friendModel.Info.Nickname = account.Nickname;
					friendModel.Info.Rank = account.Rank;
					friendModel.Info.IsOnline = account.IsOnline;
					friendModel.Info.Status = account.Status;
				}
				if (num2 == 0)
				{
					account.Friend.AddFriend(friendModel);
					return;
				}
				if (num2 == 1)
				{
					account.Friend.GetFriend(num3);
					return;
				}
				if (num2 == 2)
				{
					account.Friend.RemoveFriend(num3);
				}
			}
		}
	}
}
