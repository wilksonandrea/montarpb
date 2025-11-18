namespace Server.Auth.Data.Sync.Client
{
    using Plugin.Core.Models;
    using Plugin.Core.Network;
    using Server.Auth.Data.Managers;
    using Server.Auth.Data.Models;
    using System;

    public static class FriendSync
    {
        public static void Load(SyncClientPacket C)
        {
            long id = C.ReadQ();
            int num2 = C.ReadC();
            long num3 = C.ReadQ();
            FriendModel friend = null;
            if (num2 <= 1)
            {
                int num4 = C.ReadC();
                bool flag = C.ReadC() == 1;
                FriendModel model1 = new FriendModel(num3);
                model1.State = num4;
                model1.Removed = flag;
                friend = model1;
            }
            if ((friend != null) || (num2 > 1))
            {
                Account account = AccountManager.GetAccount(id, true);
                if (account != null)
                {
                    if (num2 <= 1)
                    {
                        friend.Info.Nickname = account.Nickname;
                        friend.Info.Rank = account.Rank;
                        friend.Info.IsOnline = account.IsOnline;
                        friend.Info.Status = account.Status;
                    }
                    if (num2 == 0)
                    {
                        account.Friend.AddFriend(friend);
                    }
                    else if (num2 == 1)
                    {
                        account.Friend.GetFriend(num3);
                    }
                    else if (num2 == 2)
                    {
                        account.Friend.RemoveFriend(num3);
                    }
                }
            }
        }
    }
}

