namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Models;
    using Server.Game.Data.Managers;
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;

    public class PROTOCOL_AUTH_FRIEND_INVITED_REQUEST_REQ : GameClientPacket
    {
        private int int_0;

        private Account method_0(Account account_0)
        {
            FriendModel friend = account_0.Friend.GetFriend(this.int_0);
            return ((friend != null) ? AccountManager.GetAccount(friend.PlayerId, 0x11f) : null);
        }

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
                    Account account2 = this.method_0(player);
                    if (account2 == null)
                    {
                        base.Client.SendPacket(new PROTOCOL_AUTH_FRIEND_INVITED_ACK(0x8000103d));
                    }
                    else if ((account2.Status.ServerId == 0xff) || (account2.Status.ServerId == 0))
                    {
                        base.Client.SendPacket(new PROTOCOL_AUTH_FRIEND_INVITED_ACK(0x80003002));
                    }
                    else if (account2.MatchSlot >= 0)
                    {
                        base.Client.SendPacket(new PROTOCOL_AUTH_FRIEND_INVITED_ACK(0x80003003));
                    }
                    else
                    {
                        int friendIdx = account2.Friend.GetFriendIdx(player.PlayerId);
                        if (friendIdx == -1)
                        {
                            base.Client.SendPacket(new PROTOCOL_AUTH_FRIEND_INVITED_ACK(0x8000103e));
                        }
                        else if (account2.IsOnline)
                        {
                            account2.SendPacket(new PROTOCOL_AUTH_FRIEND_INVITED_REQUEST_ACK(friendIdx), false);
                        }
                        else
                        {
                            base.Client.SendPacket(new PROTOCOL_AUTH_FRIEND_INVITED_ACK(0x8000103f));
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                CLogger.Print("PROTOCOL_AUTH_FRIEND_INVITED_REQUEST_REQ: " + exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

