using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.SQL;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x020001A0 RID: 416
	public class PROTOCOL_CS_INVITE_REQ : GameClientPacket
	{
		// Token: 0x0600044D RID: 1101 RVA: 0x00004D24 File Offset: 0x00002F24
		public override void Read()
		{
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x000219C4 File Offset: 0x0001FBC4
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null && player.ClanId != 0)
				{
					if (!(player.FindPlayer == "") && player.FindPlayer.Length != 0)
					{
						Account account = AccountManager.GetAccount(player.FindPlayer, 1, 0);
						if (account != null)
						{
							if (account.ClanId == 0 && player.ClanId != 0)
							{
								this.method_0(account, player.ClanId);
							}
							else
							{
								this.uint_0 = 2147483648U;
							}
						}
						else
						{
							this.uint_0 = 2147483648U;
						}
						this.Client.SendPacket(new PROTOCOL_CS_INVITE_ACK(this.uint_0));
					}
				}
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x00021A94 File Offset: 0x0001FC94
		private void method_0(Account account_0, int int_0)
		{
			if (DaoManagerSQL.GetMessagesCount(account_0.PlayerId) >= 100)
			{
				this.uint_0 = 2147483648U;
				return;
			}
			MessageModel messageModel = this.method_1(int_0, account_0.PlayerId, this.Client.PlayerId);
			if (messageModel != null && account_0.IsOnline)
			{
				account_0.SendPacket(new PROTOCOL_MESSENGER_NOTE_RECEIVE_ACK(messageModel), false);
			}
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x00021AF0 File Offset: 0x0001FCF0
		private MessageModel method_1(int int_0, long long_0, long long_1)
		{
			MessageModel messageModel = new MessageModel(15.0)
			{
				SenderName = ClanManager.GetClan(int_0).Name,
				ClanId = int_0,
				SenderId = long_1,
				Type = NoteMessageType.ClanAsk,
				State = NoteMessageState.Unreaded,
				ClanNote = NoteMessageClan.Invite
			};
			if (!DaoManagerSQL.CreateMessage(long_0, messageModel))
			{
				return null;
			}
			return messageModel;
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_CS_INVITE_REQ()
		{
		}

		// Token: 0x04000305 RID: 773
		private uint uint_0;
	}
}
