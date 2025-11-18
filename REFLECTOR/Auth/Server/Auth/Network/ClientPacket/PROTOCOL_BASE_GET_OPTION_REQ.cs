namespace Server.Auth.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Models;
    using Plugin.Core.Network;
    using Plugin.Core.SQL;
    using Server.Auth.Data.Models;
    using Server.Auth.Network;
    using Server.Auth.Network.ServerPacket;
    using System;
    using System.Collections.Generic;

    public class PROTOCOL_BASE_GET_OPTION_REQ : AuthClientPacket
    {
        private byte[] method_0(int int_0, ref int int_1, List<MessageModel> list_0)
        {
            byte[] buffer;
            using (SyncServerPacket packet = new SyncServerPacket())
            {
                int num = int_0 * 0x19;
                while (true)
                {
                    if (num < list_0.Count)
                    {
                        this.method_1(list_0[num], packet);
                        int num2 = int_1 + 1;
                        int_1 = num2;
                        if (num2 != 0x19)
                        {
                            num++;
                            continue;
                        }
                    }
                    buffer = packet.ToArray();
                    break;
                }
            }
            return buffer;
        }

        private void method_1(MessageModel messageModel_0, SyncServerPacket syncServerPacket_0)
        {
            syncServerPacket_0.WriteD((uint) messageModel_0.ObjectId);
            syncServerPacket_0.WriteQ(messageModel_0.SenderId);
            syncServerPacket_0.WriteC((byte) messageModel_0.Type);
            syncServerPacket_0.WriteC((byte) messageModel_0.State);
            syncServerPacket_0.WriteC((byte) messageModel_0.DaysRemaining);
            syncServerPacket_0.WriteD(messageModel_0.ClanId);
        }

        private byte[] method_2(int int_0, ref int int_1, List<MessageModel> list_0)
        {
            byte[] buffer;
            using (SyncServerPacket packet = new SyncServerPacket())
            {
                int num = int_0 * 0x19;
                while (true)
                {
                    if (num < list_0.Count)
                    {
                        this.method_3(list_0[num], packet);
                        int num2 = int_1 + 1;
                        int_1 = num2;
                        if (num2 != 0x19)
                        {
                            num++;
                            continue;
                        }
                    }
                    buffer = packet.ToArray();
                    break;
                }
            }
            return buffer;
        }

        private void method_3(MessageModel messageModel_0, SyncServerPacket syncServerPacket_0)
        {
            syncServerPacket_0.WriteC((byte) (messageModel_0.SenderName.Length + 1));
            syncServerPacket_0.WriteC(((messageModel_0.Type == NoteMessageType.Insert) || ((messageModel_0.Type == NoteMessageType.ClanAsk) || ((messageModel_0.Type == NoteMessageType.Clan) && (messageModel_0.ClanNote != NoteMessageClan.None)))) ? ((byte) 0) : ((byte) (messageModel_0.Text.Length + 1)));
            syncServerPacket_0.WriteN(messageModel_0.SenderName, messageModel_0.SenderName.Length + 2, "UTF-16LE");
            if ((messageModel_0.Type != NoteMessageType.ClanAsk) && (messageModel_0.Type != NoteMessageType.Clan))
            {
                syncServerPacket_0.WriteN(messageModel_0.Text, messageModel_0.Text.Length + 2, "UTF-16LE");
            }
            else if ((messageModel_0.ClanNote >= NoteMessageClan.JoinAccept) && (messageModel_0.ClanNote <= NoteMessageClan.Secession))
            {
                syncServerPacket_0.WriteH((short) (messageModel_0.Text.Length + 1));
                syncServerPacket_0.WriteH((short) messageModel_0.ClanNote);
                syncServerPacket_0.WriteN(messageModel_0.Text, messageModel_0.Text.Length + 1, "UTF-16LE");
            }
            else if (messageModel_0.ClanNote == NoteMessageClan.None)
            {
                syncServerPacket_0.WriteN(messageModel_0.Text, messageModel_0.Text.Length + 2, "UTF-16LE");
            }
            else
            {
                syncServerPacket_0.WriteH((short) 3);
                syncServerPacket_0.WriteD((int) messageModel_0.ClanNote);
                if ((messageModel_0.ClanNote != NoteMessageClan.Master) || ((messageModel_0.ClanNote != NoteMessageClan.Staff) || (messageModel_0.ClanNote != NoteMessageClan.Regular)))
                {
                    syncServerPacket_0.WriteH((short) 0);
                }
            }
        }

        public override void Read()
        {
        }

        public override void Run()
        {
            try
            {
                Account player = base.Client.Player;
                if (player != null)
                {
                    if (!player.MyConfigsLoaded && (player.Friend.Friends.Count > 0))
                    {
                        base.Client.SendPacket(new PROTOCOL_AUTH_FRIEND_INFO_ACK(player.Friend.Friends));
                    }
                    List<MessageModel> messages = DaoManagerSQL.GetMessages(player.PlayerId);
                    if (messages.Count > 0)
                    {
                        DaoManagerSQL.RecycleMessages(player.PlayerId, messages);
                        if (messages.Count > 0)
                        {
                            int num2 = 0;
                            int num3 = 0;
                            if (0 >= ((int) Math.Ceiling((double) (((double) messages.Count) / 25.0))))
                            {
                                num3 = 0;
                            }
                            base.Client.SendPacket(new PROTOCOL_MESSENGER_NOTE_LIST_ACK(messages.Count, num3++, this.method_0(num3, ref num2, messages), this.method_2(num3, ref num2, messages)));
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                CLogger.Print("PROTOCOL_BASE_GET_OPTION_REQ: " + exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

