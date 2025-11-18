using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;

namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_MESSENGER_NOTE_RECEIVE_ACK : GameServerPacket
{
	private readonly MessageModel messageModel_0;

	public PROTOCOL_MESSENGER_NOTE_RECEIVE_ACK(MessageModel messageModel_1)
	{
		messageModel_0 = messageModel_1;
	}

	public override void Write()
	{
		WriteH(1931);
		WriteD((uint)messageModel_0.ObjectId);
		WriteQ(messageModel_0.SenderId);
		WriteC((byte)messageModel_0.Type);
		WriteC((byte)messageModel_0.State);
		WriteC((byte)messageModel_0.DaysRemaining);
		WriteD(messageModel_0.ClanId);
		WriteB(NoteClanData(messageModel_0));
	}

	public byte[] NoteClanData(MessageModel Message)
	{
		using SyncServerPacket syncServerPacket = new SyncServerPacket();
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
				syncServerPacket.WriteH((short)((Message.Type != NoteMessageType.Insert) ? ((Message.Type == NoteMessageType.ClanAsk) ? 1 : ((Message.Type == NoteMessageType.Normal || Message.Type == NoteMessageType.Clan) ? 3 : 2)) : 0));
				syncServerPacket.WriteD((int)Message.ClanNote);
				if (Message.ClanNote != NoteMessageClan.Master || Message.ClanNote != NoteMessageClan.Staff || Message.ClanNote != NoteMessageClan.Regular)
				{
					syncServerPacket.WriteH(0);
				}
				syncServerPacket.WriteN(Message.Text, Message.Text.Length + 2, "UTF-16LE");
			}
		}
		return syncServerPacket.ToArray();
	}
}
