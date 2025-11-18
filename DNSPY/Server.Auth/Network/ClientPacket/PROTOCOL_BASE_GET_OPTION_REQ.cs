using System;
using System.Collections.Generic;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.SQL;
using Server.Auth.Data.Models;
using Server.Auth.Network.ServerPacket;

namespace Server.Auth.Network.ClientPacket
{
	// Token: 0x02000040 RID: 64
	public class PROTOCOL_BASE_GET_OPTION_REQ : AuthClientPacket
	{
		// Token: 0x060000D8 RID: 216 RVA: 0x00002A1B File Offset: 0x00000C1B
		public override void Read()
		{
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x000075B8 File Offset: 0x000057B8
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
							int num = (int)Math.Ceiling((double)messages.Count / 25.0);
							int num2 = 0;
							int num3 = 0;
							if (0 >= num)
							{
								num3 = 0;
							}
							byte[] array = this.method_0(num3, ref num2, messages);
							byte[] array2 = this.method_2(num3, ref num2, messages);
							this.Client.SendPacket(new PROTOCOL_MESSENGER_NOTE_LIST_ACK(messages.Count, num3++, array, array2));
						}
					}
				}
			}
			catch (Exception ex)
			{
				CLogger.Print("PROTOCOL_BASE_GET_OPTION_REQ: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x060000DA RID: 218 RVA: 0x000076C8 File Offset: 0x000058C8
		private byte[] method_0(int int_0, ref int int_1, List<MessageModel> list_0)
		{
			byte[] array;
			using (SyncServerPacket syncServerPacket = new SyncServerPacket())
			{
				for (int i = int_0 * 25; i < list_0.Count; i++)
				{
					this.method_1(list_0[i], syncServerPacket);
					int num = int_1 + 1;
					int_1 = num;
					if (num == 25)
					{
						break;
					}
				}
				array = syncServerPacket.ToArray();
			}
			return array;
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00007730 File Offset: 0x00005930
		private void method_1(MessageModel messageModel_0, SyncServerPacket syncServerPacket_0)
		{
			syncServerPacket_0.WriteD((uint)messageModel_0.ObjectId);
			syncServerPacket_0.WriteQ(messageModel_0.SenderId);
			syncServerPacket_0.WriteC((byte)messageModel_0.Type);
			syncServerPacket_0.WriteC((byte)messageModel_0.State);
			syncServerPacket_0.WriteC((byte)messageModel_0.DaysRemaining);
			syncServerPacket_0.WriteD(messageModel_0.ClanId);
		}

		// Token: 0x060000DC RID: 220 RVA: 0x0000778C File Offset: 0x0000598C
		private byte[] method_2(int int_0, ref int int_1, List<MessageModel> list_0)
		{
			byte[] array;
			using (SyncServerPacket syncServerPacket = new SyncServerPacket())
			{
				for (int i = int_0 * 25; i < list_0.Count; i++)
				{
					this.method_3(list_0[i], syncServerPacket);
					int num = int_1 + 1;
					int_1 = num;
					if (num == 25)
					{
						break;
					}
				}
				array = syncServerPacket.ToArray();
			}
			return array;
		}

		// Token: 0x060000DD RID: 221 RVA: 0x000077F4 File Offset: 0x000059F4
		private void method_3(MessageModel messageModel_0, SyncServerPacket syncServerPacket_0)
		{
			syncServerPacket_0.WriteC((byte)(messageModel_0.SenderName.Length + 1));
			syncServerPacket_0.WriteC((byte)((messageModel_0.Type == NoteMessageType.Insert || messageModel_0.Type == NoteMessageType.ClanAsk || (messageModel_0.Type == NoteMessageType.Clan && messageModel_0.ClanNote != NoteMessageClan.None)) ? 0 : (messageModel_0.Text.Length + 1)));
			syncServerPacket_0.WriteN(messageModel_0.SenderName, messageModel_0.SenderName.Length + 2, "UTF-16LE");
			if (messageModel_0.Type != NoteMessageType.ClanAsk)
			{
				if (messageModel_0.Type != NoteMessageType.Clan)
				{
					syncServerPacket_0.WriteN(messageModel_0.Text, messageModel_0.Text.Length + 2, "UTF-16LE");
					return;
				}
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

		// Token: 0x060000DE RID: 222 RVA: 0x00002A1D File Offset: 0x00000C1D
		public PROTOCOL_BASE_GET_OPTION_REQ()
		{
		}
	}
}
