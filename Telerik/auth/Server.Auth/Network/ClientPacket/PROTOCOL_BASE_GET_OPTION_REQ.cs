using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.SQL;
using Server.Auth;
using Server.Auth.Data.Models;
using Server.Auth.Network;
using Server.Auth.Network.ServerPacket;
using System;
using System.Collections.Generic;

namespace Server.Auth.Network.ClientPacket
{
	public class PROTOCOL_BASE_GET_OPTION_REQ : AuthClientPacket
	{
		public PROTOCOL_BASE_GET_OPTION_REQ()
		{
		}

		private byte[] method_0(int int_0, ref int int_1, List<MessageModel> list_0)
		{
			byte[] array;
			using (SyncServerPacket syncServerPacket = new SyncServerPacket())
			{
				for (int i = int_0 * 25; i < list_0.Count; i++)
				{
					this.method_1(list_0[i], syncServerPacket);
					int int1 = int_1 + 1;
					int_1 = int1;
					if (int1 == 25)
					{
						break;
					}
				}
				array = syncServerPacket.ToArray();
			}
			return array;
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
			byte[] array;
			using (SyncServerPacket syncServerPacket = new SyncServerPacket())
			{
				for (int i = int_0 * 25; i < list_0.Count; i++)
				{
					this.method_3(list_0[i], syncServerPacket);
					int int1 = int_1 + 1;
					int_1 = int1;
					if (int1 == 25)
					{
						break;
					}
				}
				array = syncServerPacket.ToArray();
			}
			return array;
		}

		private void method_3(MessageModel messageModel_0, SyncServerPacket syncServerPacket_0)
		{
			object length;
			syncServerPacket_0.WriteC((byte)(messageModel_0.SenderName.Length + 1));
			SyncServerPacket syncServerPacket0 = syncServerPacket_0;
			if (messageModel_0.Type == NoteMessageType.Insert || messageModel_0.Type == NoteMessageType.ClanAsk || messageModel_0.Type == NoteMessageType.Clan && messageModel_0.ClanNote != NoteMessageClan.None)
			{
				length = null;
			}
			else
			{
				length = messageModel_0.Text.Length + 1;
			}
			syncServerPacket0.WriteC((byte)length);
			syncServerPacket_0.WriteN(messageModel_0.SenderName, messageModel_0.SenderName.Length + 2, "UTF-16LE");
			if (messageModel_0.Type != NoteMessageType.ClanAsk)
			{
				if (messageModel_0.Type == NoteMessageType.Clan)
				{
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
					if (messageModel_0.ClanNote == NoteMessageClan.Master && messageModel_0.ClanNote == NoteMessageClan.Staff)
					{
						if (messageModel_0.ClanNote == NoteMessageClan.Regular)
						{
							return;
						}
					}
					syncServerPacket_0.WriteH(0);
					return;
				}
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
			if (messageModel_0.ClanNote == NoteMessageClan.Master && messageModel_0.ClanNote == NoteMessageClan.Staff)
			{
				if (messageModel_0.ClanNote == NoteMessageClan.Regular)
				{
					return;
				}
			}
			syncServerPacket_0.WriteH(0);
		}

		public override void Read()
		{
		}

		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					if (!player.MyConfigsLoaded && player.Friend.Friends.Count > 0)
					{
						this.Client.SendPacket(new PROTOCOL_AUTH_FRIEND_INFO_ACK(player.Friend.Friends));
					}
					List<MessageModel> messages = DaoManagerSQL.GetMessages(player.PlayerId);
					if (messages.Count > 0)
					{
						DaoManagerSQL.RecycleMessages(player.PlayerId, messages);
						if (messages.Count > 0)
						{
							int ınt32 = (int)Math.Ceiling((double)messages.Count / 25);
							int ınt321 = 0;
							int ınt322 = 0;
							if (0 >= ınt32)
							{
								ınt322 = 0;
							}
							byte[] numArray = this.method_0(ınt322, ref ınt321, messages);
							byte[] numArray1 = this.method_2(ınt322, ref ınt321, messages);
							int ınt323 = ınt322;
							ınt322 = ınt323 + 1;
							this.Client.SendPacket(new PROTOCOL_MESSENGER_NOTE_LIST_ACK(messages.Count, ınt323, numArray, numArray1));
						}
					}
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("PROTOCOL_BASE_GET_OPTION_REQ: ", exception.Message), LoggerType.Error, exception);
			}
		}
	}
}