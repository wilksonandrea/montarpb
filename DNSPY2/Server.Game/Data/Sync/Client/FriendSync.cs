using System;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;

namespace Server.Game.Data.Sync.Client
{
	// Token: 0x020001F4 RID: 500
	public class FriendSync
	{
		// Token: 0x060005DA RID: 1498 RVA: 0x0002F9C8 File Offset: 0x0002DBC8
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

		// Token: 0x060005DB RID: 1499 RVA: 0x000025DF File Offset: 0x000007DF
		public FriendSync()
		{
		}
	}
}
