using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Server.Game.Network;
using System;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_MESSENGER_NOTE_RECEIVE_ACK : GameServerPacket
	{
		private readonly MessageModel messageModel_0;

		public PROTOCOL_MESSENGER_NOTE_RECEIVE_ACK(MessageModel messageModel_1)
		{
			this.messageModel_0 = messageModel_1;
		}

		public byte[] NoteClanData(MessageModel Message)
		{
			byte[] array;
			object obj;
			using (SyncServerPacket syncServerPacket = new SyncServerPacket())
			{
				if (Message.Type == NoteMessageType.Normal || Message.Type == NoteMessageType.ClanInfo)
				{
					syncServerPacket.WriteC((byte)(Message.SenderName.Length + 2));
					syncServerPacket.WriteC((byte)(Message.Text.Length + 2));
					syncServerPacket.WriteN(Message.SenderName, Message.SenderName.Length + 2, "UTF-16LE");
					if (Message.ClanNote == NoteMessageClan.None)
					{
						syncServerPacket.WriteH((short)Message.ClanNote);
						syncServerPacket.WriteN(Message.Text, Message.Text.Length + 2, "UTF-16LE");
					}
				}
				if (Message.Type == NoteMessageType.ClanAsk || Message.Type == NoteMessageType.Clan || Message.Type == NoteMessageType.Insert)
				{
					syncServerPacket.WriteC((byte)(Message.SenderName.Length + 1));
					syncServerPacket.WriteC(0);
					syncServerPacket.WriteN(Message.SenderName, Message.SenderName.Length + 2, "UTF-16LE");
					if (Message.ClanNote > NoteMessageClan.Secession)
					{
						SyncServerPacket syncServerPacket1 = syncServerPacket;
						if (Message.Type == NoteMessageType.Insert)
						{
							obj = null;
						}
						else if (Message.Type == NoteMessageType.ClanAsk)
						{
							obj = 1;
						}
						else
						{
							obj = (Message.Type == NoteMessageType.Normal || Message.Type == NoteMessageType.Clan ? 3 : 2);
						}
						syncServerPacket1.WriteH((short)obj);
						syncServerPacket.WriteD((int)Message.ClanNote);
						if (Message.ClanNote != NoteMessageClan.Master || Message.ClanNote != NoteMessageClan.Staff || Message.ClanNote != NoteMessageClan.Regular)
						{
							syncServerPacket.WriteH(0);
						}
						syncServerPacket.WriteN(Message.Text, Message.Text.Length + 2, "UTF-16LE");
					}
					else
					{
						syncServerPacket.WriteH((short)(Message.Text.Length + 1));
						syncServerPacket.WriteH((short)Message.ClanNote);
						syncServerPacket.WriteN(Message.Text, Message.Text.Length + 2, "UTF-16LE");
					}
				}
				array = syncServerPacket.ToArray();
			}
			return array;
		}

		public override void Write()
		{
			base.WriteH(1931);
			base.WriteD((uint)this.messageModel_0.ObjectId);
			base.WriteQ(this.messageModel_0.SenderId);
			base.WriteC((byte)this.messageModel_0.Type);
			base.WriteC((byte)this.messageModel_0.State);
			base.WriteC((byte)this.messageModel_0.DaysRemaining);
			base.WriteD(this.messageModel_0.ClanId);
			base.WriteB(this.NoteClanData(this.messageModel_0));
		}
	}
}