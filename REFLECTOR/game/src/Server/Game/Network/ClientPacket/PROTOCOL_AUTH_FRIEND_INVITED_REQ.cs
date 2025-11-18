namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Models;
    using Server.Game.Data.Managers;
    using Server.Game.Data.Models;
    using Server.Game.Data.Utils;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;

    public class PROTOCOL_AUTH_FRIEND_INVITED_REQ : GameClientPacket
    {
        private string string_0;

        public override void Read()
        {
            this.string_0 = base.ReadU(0x42);
        }

        public override void Run()
        {
            try
            {
                Account player = base.Client.Player;
                if (player != null)
                {
                    if ((player.Nickname.Length == 0) || (player.Nickname == this.string_0))
                    {
                        base.Client.SendPacket(new PROTOCOL_AUTH_FRIEND_INVITED_ACK(0x80001037));
                    }
                    else if (player.Friend.Friends.Count >= 50)
                    {
                        base.Client.SendPacket(new PROTOCOL_AUTH_FRIEND_INVITED_ACK(0x80001038));
                    }
                    else
                    {
                        Account owner = AccountManager.GetAccount(this.string_0, 1, 0x11f);
                        if (owner == null)
                        {
                            base.Client.SendPacket(new PROTOCOL_AUTH_FRIEND_INVITED_ACK(0x80001042));
                        }
                        else if (player.Friend.GetFriendIdx(owner.PlayerId) != -1)
                        {
                            base.Client.SendPacket(new PROTOCOL_AUTH_FRIEND_INVITED_ACK(0x80001041));
                        }
                        else if (owner.Friend.Friends.Count >= 50)
                        {
                            base.Client.SendPacket(new PROTOCOL_AUTH_FRIEND_INVITED_ACK(0x80001038));
                        }
                        else
                        {
                            int num = AllUtils.AddFriend(owner, player, 2);
                            if ((AllUtils.AddFriend(player, owner, (int) (num != 1)) == -1) || (num == -1))
                            {
                                base.Client.SendPacket(new PROTOCOL_AUTH_FRIEND_INVITED_ACK(0x80001039));
                            }
                            else
                            {
                                int num2;
                                int num3;
                                FriendModel friend = owner.Friend.GetFriend(player.PlayerId, out num2);
                                if (friend != null)
                                {
                                    owner.SendPacket(new PROTOCOL_AUTH_FRIEND_INFO_CHANGE_ACK((num == 0) ? FriendChangeState.Insert : FriendChangeState.Update, friend, num2), false);
                                }
                                FriendModel model2 = player.Friend.GetFriend(owner.PlayerId, out num3);
                                if (model2 != null)
                                {
                                    base.Client.SendPacket(new PROTOCOL_AUTH_FRIEND_INFO_CHANGE_ACK(FriendChangeState.Insert, model2, num3));
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                CLogger.Print("PROTOCOL_AUTH_FRIEND_INVITED_REQ: " + exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

