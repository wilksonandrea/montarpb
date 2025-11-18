using System;
using System.Collections.Generic;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.SQL;
using Server.Auth.Data.Models;
using Server.Auth.Network.ServerPacket;

namespace Server.Auth.Network.ClientPacket;

public class PROTOCOL_BASE_GET_OPTION_REQ : AuthClientPacket
{
	public override void Read()
	{
	}

	public override void Run()
	{
		try
		{
			Account player = Client.Player;
			if (player == null)
			{
				return;
			}
			if (!player.MyConfigsLoaded && player.Friend.Friends.Count > 0)
			{
				Client.SendPacket(new PROTOCOL_AUTH_FRIEND_INFO_ACK(player.Friend.Friends));
			}
			List<MessageModel> messages = DaoManagerSQL.GetMessages(player.PlayerId);
			if (messages.Count <= 0)
			{
				return;
			}
			DaoManagerSQL.RecycleMessages(player.PlayerId, messages);
			if (messages.Count > 0)
			{
				int num = (int)Math.Ceiling((double)messages.Count / 25.0);
				int int_ = 0;
				int int_2 = 0;
				if (0 >= num)
				{
					int_2 = 0;
				}
				byte[] byte_ = method_0(int_2, ref int_, messages);
				byte[] byte_2 = method_2(int_2, ref int_, messages);
				Client.SendPacket(new PROTOCOL_MESSENGER_NOTE_LIST_ACK(messages.Count, int_2++, byte_, byte_2));
			}
		}
		catch (Exception ex)
		{
			CLogger.Print("PROTOCOL_BASE_GET_OPTION_REQ: " + ex.Message, LoggerType.Error, ex);
		}
	}

	private byte[] method_0(int int_0, ref int int_1, List<MessageModel> list_0)
	{
		using SyncServerPacket syncServerPacket = new SyncServerPacket();
		for (int i = int_0 * 25; i < list_0.Count; i++)
		{
			method_1(list_0[i], syncServerPacket);
			if (++int_1 == 25)
			{
				break;
			}
		}
		return syncServerPacket.ToArray();
	}

	private void method_1(MessageModel messageModel_0, SyncServerPacket syncServerPacket_0)
	{
		syncServerPacket_0.WriteD((uint)messageModel_0.ObjectId);
		syncServerPacket_0.WriteQ(messageModel_0.SenderId);
		syncServerPacket_0.WriteC((byte)messageModel_0.Type);
		syncServerPacket_0.WriteC((byte)messageModel_0.State);
		syncServerPacket_0.WriteC((byte)messageModel_0.DaysRemaining);
		syncServerPacket_0.WriteD(messageModel_0.ClanId);
	}

	private byte[] method_2(int int_0, ref int int_1, List<MessageModel> list_0)
	{
		using SyncServerPacket syncServerPacket = new SyncServerPacket();
		for (int i = int_0 * 25; i < list_0.Count; i++)
		{
			method_3(list_0[i], syncServerPacket);
			if (++int_1 == 25)
			{
				break;
			}
		}
		return syncServerPacket.ToArray();
	}

	private void method_3(MessageModel messageModel_0, SyncServerPacket syncServerPacket_0)
	{
		syncServerPacket_0.WriteC((byte)(messageModel_0.SenderName.Length + 1));
		syncServerPacket_0.WriteC((byte)((messageModel_0.Type != NoteMessageType.Insert && messageModel_0.Type != NoteMessageType.ClanAsk && (messageModel_0.Type != NoteMessageType.Clan || messageModel_0.ClanNote == NoteMessageClan.None)) ? ((uint)(messageModel_0.Text.Length + 1)) : 0u));
		syncServerPacket_0.WriteN(messageModel_0.SenderName, messageModel_0.SenderName.Length + 2, "UTF-16LE");
		if (messageModel_0.Type != NoteMessageType.ClanAsk && messageModel_0.Type != NoteMessageType.Clan)
		{
			syncServerPacket_0.WriteN(messageModel_0.Text, messageModel_0.Text.Length + 2, "UTF-16LE");
			return;
		}
		if (messageModel_0.ClanNote >= NoteMessageClan.JoinAccept && messageModel_0.ClanNote <= NoteMessageClan.Secession)
		{
			syncServerPacket_0.WriteH((short)(messageModel_0.Text.Length + 1));
			syncServerPacket_0.WriteH((short)messageModel_0.ClanNote);
			syncServerPacket_0.WriteN(messageModel_0.Text, messageModel_0.Text.Length + 1, "UTF-16LE");
			return;
		}
		if (messageModel_0.ClanNote == NoteMessageClan.None)
		{
			syncServerPacket_0.WriteN(messageModel_0.Text, messageModel_0.Text.Length + 2, "UTF-16LE");
			return;
		}
		syncServerPacket_0.WriteH(3);
		syncServerPacket_0.WriteD((int)messageModel_0.ClanNote);
		if (messageModel_0.ClanNote != NoteMessageClan.Master || messageModel_0.ClanNote != NoteMessageClan.Staff || messageModel_0.ClanNote != NoteMessageClan.Regular)
		{
			syncServerPacket_0.WriteH(0);
		}
	}
}
