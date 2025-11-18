using Plugin.Core.Models;
using Plugin.Core.Network;
using Server.Auth.Data.Managers;
using Server.Auth.Data.Models;
using System;

namespace Server.Auth.Data.Sync.Client
{
	public static class FriendSync
	{
		public static void Load(SyncClientPacket C)
		{
			long ınt64 = C.ReadQ();
			int ınt32 = C.ReadC();
			long ınt641 = C.ReadQ();
			FriendModel friendModel = null;
			if (ınt32 <= 1)
			{
				int ınt321 = C.ReadC();
				bool flag = C.ReadC() == 1;
				friendModel = new FriendModel(ınt641)
				{
					State = ınt321,
					Removed = flag
				};
			}
			if (friendModel == null && ınt32 <= 1)
			{
				return;
			}
			Account account = AccountManager.GetAccount(ınt64, true);
			if (account != null)
			{
				if (ınt32 <= 1)
				{
					friendModel.Info.Nickname = account.Nickname;
					friendModel.Info.Rank = account.Rank;
					friendModel.Info.IsOnline = account.IsOnline;
					friendModel.Info.Status = account.Status;
				}
				if (ınt32 == 0)
				{
					account.Friend.AddFriend(friendModel);
					return;
				}
				if (ınt32 == 1)
				{
					account.Friend.GetFriend(ınt641);
					return;
				}
				if (ınt32 == 2)
				{
					account.Friend.RemoveFriend(ınt641);
				}
			}
		}
	}
}