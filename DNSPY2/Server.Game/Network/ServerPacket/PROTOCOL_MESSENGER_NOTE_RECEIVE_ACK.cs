using System;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x020000E5 RID: 229
	public class PROTOCOL_MESSENGER_NOTE_RECEIVE_ACK : GameServerPacket
	{
		// Token: 0x06000231 RID: 561 RVA: 0x000044C0 File Offset: 0x000026C0
		public PROTOCOL_MESSENGER_NOTE_RECEIVE_ACK(MessageModel messageModel_1)
		{
			this.messageModel_0 = messageModel_1;
		}

		// Token: 0x06000232 RID: 562 RVA: 0x00011BB4 File Offset: 0x0000FDB4
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

		// Token: 0x06000233 RID: 563 RVA: 0x00011C48 File Offset: 0x0000FE48
		public byte[] NoteClanData(MessageModel Message)
		{
			byte[] array;
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
					if (Message.ClanNote <= NoteMessageClan.Secession)
					{
						syncServerPacket.WriteH((short)(Message.Text.Length + 1));
						syncServerPacket.WriteH((short)Message.ClanNote);
						syncServerPacket.WriteN(Message.Text, Message.Text.Length + 2, "UTF-16LE");
					}
					else
					{
						syncServerPacket.WriteH((Message.Type == NoteMessageType.Insert) ? 0 : ((Message.Type == NoteMessageType.ClanAsk) ? 1 : ((Message.Type == NoteMessageType.Normal || Message.Type == NoteMessageType.Clan) ? 3 : 2)));
						syncServerPacket.WriteD((int)Message.ClanNote);
						if (Message.ClanNote != NoteMessageClan.Master || Message.ClanNote != NoteMessageClan.Staff || Message.ClanNote != NoteMessageClan.Regular)
						{
							syncServerPacket.WriteH(0);
						}
						syncServerPacket.WriteN(Message.Text, Message.Text.Length + 2, "UTF-16LE");
					}
				}
				array = syncServerPacket.ToArray();
			}
			return array;
		}

		// Token: 0x040001A9 RID: 425
		private readonly MessageModel messageModel_0;
	}
}
