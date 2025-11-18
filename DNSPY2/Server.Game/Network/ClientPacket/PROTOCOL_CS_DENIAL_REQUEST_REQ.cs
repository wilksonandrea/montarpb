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
	// Token: 0x0200019C RID: 412
	public class PROTOCOL_CS_DENIAL_REQUEST_REQ : GameClientPacket
	{
		// Token: 0x0600043E RID: 1086 RVA: 0x00021150 File Offset: 0x0001F350
		public override void Read()
		{
			int num = (int)base.ReadC();
			for (int i = 0; i < num; i++)
			{
				long num2 = base.ReadQ();
				this.list_0.Add(num2);
			}
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x00021184 File Offset: 0x0001F384
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					ClanModel clan = ClanManager.GetClan(player.ClanId);
					if (clan.Id > 0 && ((player.ClanAccess >= 1 && player.ClanAccess <= 2) || clan.OwnerId == player.PlayerId))
					{
						for (int i = 0; i < this.list_0.Count; i++)
						{
							long num = this.list_0[i];
							if (DaoManagerSQL.DeleteClanInviteDB(clan.Id, num))
							{
								if (DaoManagerSQL.GetMessagesCount(num) < 100)
								{
									MessageModel messageModel = this.method_0(clan, num, player.PlayerId);
									if (messageModel != null)
									{
										Account account = AccountManager.GetAccount(num, 31);
										if (account != null && account.IsOnline)
										{
											account.SendPacket(new PROTOCOL_MESSENGER_NOTE_RECEIVE_ACK(messageModel), false);
										}
									}
								}
								this.int_0++;
							}
						}
					}
					this.Client.SendPacket(new PROTOCOL_CS_DENIAL_REQUEST_ACK(this.int_0));
				}
			}
			catch (Exception ex)
			{
				CLogger.Print("PROTOCOL_CS_DENIAL_REQUEST_REQ: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x000212A8 File Offset: 0x0001F4A8
		private MessageModel method_0(ClanModel clanModel_0, long long_0, long long_1)
		{
			MessageModel messageModel = new MessageModel(15.0)
			{
				SenderName = clanModel_0.Name,
				SenderId = long_1,
				ClanId = clanModel_0.Id,
				Type = NoteMessageType.Clan,
				State = NoteMessageState.Unreaded,
				ClanNote = NoteMessageClan.InviteDenial
			};
			if (!DaoManagerSQL.CreateMessage(long_0, messageModel))
			{
				return null;
			}
			return messageModel;
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x000054D8 File Offset: 0x000036D8
		public PROTOCOL_CS_DENIAL_REQUEST_REQ()
		{
		}

		// Token: 0x040002FD RID: 765
		private List<long> list_0 = new List<long>();

		// Token: 0x040002FE RID: 766
		private int int_0;
	}
}
