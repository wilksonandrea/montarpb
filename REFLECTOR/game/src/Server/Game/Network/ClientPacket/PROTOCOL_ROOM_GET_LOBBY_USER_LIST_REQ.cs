namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Server.Game.Data.Managers;
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;

    public class PROTOCOL_ROOM_GET_LOBBY_USER_LIST_REQ : GameClientPacket
    {
        private int int_0;
        private uint uint_0;
        private int int_1;

        public override void Read()
        {
            this.int_0 = base.ReadD();
            this.int_1 = base.ReadD();
        }

        public override void Run()
        {
            try
            {
                Account player = base.Client.Player;
                if (player != null)
                {
                    RoomModel room = player.Room;
                    if ((room != null) && ((this.int_0 > 0) && (this.int_0 <= 8)))
                    {
                        ChannelModel channel = player.GetChannel();
                        if (channel == null)
                        {
                            this.uint_0 = 0x80000000;
                        }
                        else
                        {
                            using (PROTOCOL_SERVER_MESSAGE_INVITED_ACK protocol_server_message_invited_ack = new PROTOCOL_SERVER_MESSAGE_INVITED_ACK(player, room))
                            {
                                byte[] completeBytes = protocol_server_message_invited_ack.GetCompleteBytes("PROTOCOL_ROOM_INVITE_LOBBY_USER_LIST_REQ");
                                for (int i = 0; i < this.int_0; i++)
                                {
                                    Account account2 = AccountManager.GetAccount(channel.GetPlayer(this.int_1).PlayerId, true);
                                    if (account2 != null)
                                    {
                                        account2.SendCompletePacket(completeBytes, protocol_server_message_invited_ack.GetType().Name);
                                    }
                                }
                            }
                        }
                    }
                    base.Client.SendPacket(new PROTOCOL_ROOM_GET_LOBBY_USER_LIST_ACK(this.uint_0));
                }
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

