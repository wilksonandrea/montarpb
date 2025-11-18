namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Models;
    using Plugin.Core.SQL;
    using Server.Game.Data.Managers;
    using Server.Game.Data.Models;
    using Server.Game.Data.Sync.Server;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;

    public class PROTOCOL_AUTH_FRIEND_DELETE_REQ : GameClientPacket
    {
        private int int_0;
        private uint uint_0;

        public override void Read()
        {
            this.int_0 = base.ReadC();
        }

        public override void Run()
        {
            try
            {
                Account player = base.Client.Player;
                if (player != null)
                {
                    FriendModel friend = player.Friend.GetFriend(this.int_0);
                    if (friend == null)
                    {
                        this.uint_0 = 0x80000000;
                    }
                    else
                    {
                        DaoManagerSQL.DeletePlayerFriend(friend.PlayerId, player.PlayerId);
                        Account account2 = AccountManager.GetAccount(friend.PlayerId, 0x11f);
                        if (account2 != null)
                        {
                            int index = -1;
                            FriendModel model2 = account2.Friend.GetFriend(player.PlayerId, out index);
                            if (model2 != null)
                            {
                                model2.Removed = true;
                                DaoManagerSQL.UpdatePlayerFriendBlock(account2.PlayerId, model2);
                                SendFriendInfo.Load(account2, model2, 2);
                                account2.SendPacket(new PROTOCOL_AUTH_FRIEND_INFO_CHANGE_ACK(FriendChangeState.Update, model2, index), false);
                            }
                        }
                        player.Friend.RemoveFriend(friend);
                        base.Client.SendPacket(new PROTOCOL_AUTH_FRIEND_INFO_CHANGE_ACK(FriendChangeState.Delete, null, 0, this.int_0));
                    }
                    base.Client.SendPacket(new PROTOCOL_AUTH_FRIEND_DELETE_ACK(this.uint_0));
                    base.Client.SendPacket(new PROTOCOL_AUTH_FRIEND_INFO_ACK(player.Friend.Friends));
                }
            }
            catch (Exception exception)
            {
                CLogger.Print("PROTOCOL_AUTH_FRIEND_DELETE_REQ: " + exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

