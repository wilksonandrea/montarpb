using System;
using System.Collections.Generic;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.SQL;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x020001A4 RID: 420
	public class PROTOCOL_CS_NOTE_REQ : GameClientPacket
	{
		// Token: 0x0600045C RID: 1116 RVA: 0x00005562 File Offset: 0x00003762
		public override void Read()
		{
			this.int_0 = (int)base.ReadC();
			this.string_0 = base.ReadU((int)(base.ReadC() * 2));
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x00021EE0 File Offset: 0x000200E0
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (this.string_0.Length <= 120 && player != null)
				{
					ClanModel clan = ClanManager.GetClan(player.ClanId);
					int num = 0;
					if (clan.Id > 0 && clan.OwnerId == this.Client.PlayerId)
					{
						List<Account> clanPlayers = ClanManager.GetClanPlayers(clan.Id, this.Client.PlayerId, true);
						for (int i = 0; i < clanPlayers.Count; i++)
						{
							Account account = clanPlayers[i];
							if ((this.int_0 == 0 || (account.ClanAccess == 2 && this.int_0 == 1) || (account.ClanAccess == 3 && this.int_0 == 2)) && DaoManagerSQL.GetMessagesCount(account.PlayerId) < 100)
							{
								num++;
								MessageModel messageModel = this.method_0(clan, account.PlayerId, this.Client.PlayerId);
								if (messageModel != null && account.IsOnline)
								{
									account.SendPacket(new PROTOCOL_MESSENGER_NOTE_RECEIVE_ACK(messageModel), false);
								}
							}
						}
					}
					this.Client.SendPacket(new PROTOCOL_CS_NOTE_ACK(num));
					if (num > 0)
					{
						this.Client.SendPacket(new PROTOCOL_MESSENGER_NOTE_SEND_ACK(0U));
					}
				}
			}
			catch (Exception ex)
			{
				CLogger.Print("PROTOCOL_CS_NOTE_REQ: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x0002205C File Offset: 0x0002025C
		private MessageModel method_0(ClanModel clanModel_0, long long_0, long long_1)
		{
			MessageModel messageModel = new MessageModel(15.0)
			{
				SenderName = clanModel_0.Name,
				SenderId = long_1,
				ClanId = clanModel_0.Id,
				Type = NoteMessageType.ClanInfo,
				Text = this.string_0,
				State = NoteMessageState.Unreaded
			};
			if (!DaoManagerSQL.CreateMessage(long_0, messageModel))
			{
				return null;
			}
			return messageModel;
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_CS_NOTE_REQ()
		{
		}

		// Token: 0x0400030A RID: 778
		private int int_0;

		// Token: 0x0400030B RID: 779
		private string string_0;
	}
}
