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

    public class PROTOCOL_AUTH_FRIEND_ACCEPT_REQ : GameClientPacket
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
                    if ((friend == null) || (friend.State <= 0))
                    {
                        this.uint_0 = 0x80000000;
                    }
                    else
                    {
                        Account account2 = AccountManager.GetAccount(friend.PlayerId, 0x11f);
                        if (account2 == null)
                        {
                            this.uint_0 = 0x80000000;
                        }
                        else
                        {
                            if (friend.Info == null)
                            {
                                friend.SetModel(account2.PlayerId, account2.Rank, account2.NickColor, account2.Nickname, account2.IsOnline, account2.Status);
                            }
                            else
                            {
                                friend.Info.SetInfo(account2.Rank, account2.NickColor, account2.Nickname, account2.IsOnline, account2.Status);
                            }
                            friend.State = 0;
                            DaoManagerSQL.UpdatePlayerFriendState(player.PlayerId, friend);
                            base.Client.SendPacket(new PROTOCOL_AUTH_FRIEND_INFO_CHANGE_ACK(FriendChangeState.Accept, null, 0, this.int_0));
                            base.Client.SendPacket(new PROTOCOL_AUTH_FRIEND_INFO_CHANGE_ACK(FriendChangeState.Update, friend, this.int_0));
                            int index = -1;
                            FriendModel model2 = account2.Friend.GetFriend(player.PlayerId, out index);
                            if ((model2 != null) && (model2.State > 0))
                            {
                                if (model2.Info == null)
                                {
                                    model2.SetModel(player.PlayerId, player.Rank, player.NickColor, player.Nickname, player.IsOnline, player.Status);
                                }
                                else
                                {
                                    model2.Info.SetInfo(player.Rank, player.NickColor, player.Nickname, player.IsOnline, player.Status);
                                }
                                model2.State = 0;
                                DaoManagerSQL.UpdatePlayerFriendState(account2.PlayerId, model2);
                                SendFriendInfo.Load(account2, model2, 1);
                                account2.SendPacket(new PROTOCOL_AUTH_FRIEND_INFO_CHANGE_ACK(FriendChangeState.Update, model2, index), false);
                            }
                        }
                    }
                    if (this.uint_0 > 0)
                    {
                        base.Client.SendPacket(new PROTOCOL_AUTH_FRIEND_ACCEPT_ACK(this.uint_0));
                    }
                }
            }
            catch (Exception exception)
            {
                CLogger.Print("PROTOCOL_AUTH_FRIEND_ACCEPT_REQ: " + exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

