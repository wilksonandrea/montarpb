namespace Server.Game.Network.ServerPacket
{
    using Plugin.Core.Enums;
    using Plugin.Core.Models;
    using Plugin.Core.Network;
    using Server.Game.Network;
    using System;

    public class PROTOCOL_MESSENGER_NOTE_RECEIVE_ACK : GameServerPacket
    {
        private readonly MessageModel messageModel_0;

        public PROTOCOL_MESSENGER_NOTE_RECEIVE_ACK(MessageModel messageModel_1)
        {
            this.messageModel_0 = messageModel_1;
        }

        public byte[] NoteClanData(MessageModel Message)
        {
            using (SyncServerPacket packet = new SyncServerPacket())
            {
                if ((Message.Type == NoteMessageType.Normal) || (Message.Type == NoteMessageType.ClanInfo))
                {
                    packet.WriteC((byte) (Message.SenderName.Length + 2));
                    packet.WriteC((byte) (Message.Text.Length + 2));
                    packet.WriteN(Message.SenderName, Message.SenderName.Length + 2, "UTF-16LE");
                    if (Message.ClanNote == NoteMessageClan.None)
                    {
                        packet.WriteH((short) Message.ClanNote);
                        packet.WriteN(Message.Text, Message.Text.Length + 2, "UTF-16LE");
                    }
                }
                if ((Message.Type == NoteMessageType.ClanAsk) || ((Message.Type == NoteMessageType.Clan) || (Message.Type == NoteMessageType.Insert)))
                {
                    packet.WriteC((byte) (Message.SenderName.Length + 1));
                    packet.WriteC(0);
                    packet.WriteN(Message.SenderName, Message.SenderName.Length + 2, "UTF-16LE");
                    if (Message.ClanNote <= NoteMessageClan.Secession)
                    {
                        packet.WriteH((short) (Message.Text.Length + 1));
                        packet.WriteH((short) Message.ClanNote);
                        packet.WriteN(Message.Text, Message.Text.Length + 2, "UTF-16LE");
                    }
                    else
                    {
                        packet.WriteH((Message.Type == NoteMessageType.Insert) ? ((short) 0) : ((Message.Type == NoteMessageType.ClanAsk) ? ((short) 1) : (((Message.Type == NoteMessageType.Normal) || (Message.Type == NoteMessageType.Clan)) ? ((short) 3) : ((short) 2))));
                        packet.WriteD((int) Message.ClanNote);
                        if ((Message.ClanNote != NoteMessageClan.Master) || ((Message.ClanNote != NoteMessageClan.Staff) || (Message.ClanNote != NoteMessageClan.Regular)))
                        {
                            packet.WriteH((short) 0);
                        }
                        packet.WriteN(Message.Text, Message.Text.Length + 2, "UTF-16LE");
                    }
                }
                return packet.ToArray();
            }
        }

        public override void Write()
        {
            base.WriteH((short) 0x78b);
            base.WriteD((uint) this.messageModel_0.ObjectId);
            base.WriteQ(this.messageModel_0.SenderId);
            base.WriteC((byte) this.messageModel_0.Type);
            base.WriteC((byte) this.messageModel_0.State);
            base.WriteC((byte) this.messageModel_0.DaysRemaining);
            base.WriteD(this.messageModel_0.ClanId);
            base.WriteB(this.NoteClanData(this.messageModel_0));
        }
    }
}

