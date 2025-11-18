namespace Server.Game.Data.Sync.Client
{
    using Plugin.Core.Enums;
    using Plugin.Core.Models;
    using Plugin.Core.Network;
    using Server.Game.Data.Managers;
    using Server.Game.Data.Models;
    using Server.Game.Network.ServerPacket;
    using System;

    public class FriendInfo
    {
        public static void Load(SyncClientPacket C)
        {
            int num = C.ReadC();
            int num2 = C.ReadC();
            long id = C.ReadQ();
            Account account = AccountManager.GetAccount(C.ReadQ(), 0x1f);
            if (account != null)
            {
                Account account2 = AccountManager.GetAccount(id, true);
                if (account2 != null)
                {
                    FriendState state = (num2 == 1) ? FriendState.Online : FriendState.Offline;
                    if (num != 0)
                    {
                        account2.SendPacket(new PROTOCOL_CS_MEMBER_INFO_CHANGE_ACK(account, state));
                    }
                    else
                    {
                        int index = -1;
                        FriendModel friend = account2.Friend.GetFriend(account.PlayerId, out index);
                        if ((index != -1) && ((friend != null) && (friend.State == 0)))
                        {
                            account2.SendPacket(new PROTOCOL_AUTH_FRIEND_INFO_CHANGE_ACK(FriendChangeState.Update, friend, state, index));
                        }
                    }
                }
            }
        }
    }
}

