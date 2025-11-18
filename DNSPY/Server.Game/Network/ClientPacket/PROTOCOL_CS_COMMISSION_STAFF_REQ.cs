using System;
using System.Collections.Generic;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.SQL;
using Plugin.Core.Utility;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Data.Sync.Server;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x02000199 RID: 409
	public class PROTOCOL_CS_COMMISSION_STAFF_REQ : GameClientPacket
	{
		// Token: 0x06000434 RID: 1076 RVA: 0x00020C40 File Offset: 0x0001EE40
		public override void Read()
		{
			int num = (int)base.ReadC();
			for (int i = 0; i < num; i++)
			{
				long num2 = base.ReadQ();
				this.list_0.Add(num2);
			}
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x00020C74 File Offset: 0x0001EE74
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					ClanModel clan = ClanManager.GetClan(player.ClanId);
					if (clan.Id != 0)
					{
						if (player.ClanAccess == 1 || clan.OwnerId == this.Client.PlayerId)
						{
							for (int i = 0; i < this.list_0.Count; i++)
							{
								Account account = AccountManager.GetAccount(this.list_0[i], 31);
								if (account != null && account.ClanId == clan.Id && account.ClanAccess == 3 && ComDiv.UpdateDB("accounts", "clan_access", 2, "player_id", account.PlayerId))
								{
									account.ClanAccess = 2;
									SendClanInfo.Load(account, null, 3);
									if (DaoManagerSQL.GetMessagesCount(account.PlayerId) < 100)
									{
										MessageModel messageModel = this.method_0(clan, account.PlayerId, this.Client.PlayerId);
										if (messageModel != null && account.IsOnline)
										{
											account.SendPacket(new PROTOCOL_MESSENGER_NOTE_RECEIVE_ACK(messageModel), false);
										}
									}
									if (account.IsOnline)
									{
										account.SendPacket(new PROTOCOL_CS_COMMISSION_STAFF_RESULT_ACK(), false);
									}
									this.uint_0 += 1U;
								}
							}
							this.Client.SendPacket(new PROTOCOL_CS_COMMISSION_STAFF_ACK(this.uint_0));
							return;
						}
					}
					this.uint_0 = 2147487833U;
				}
			}
			catch (Exception ex)
			{
				CLogger.Print("PROTOCOL_CS_COMMISSION_STAFF_REQ: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x00020E1C File Offset: 0x0001F01C
		private MessageModel method_0(ClanModel clanModel_0, long long_0, long long_1)
		{
			MessageModel messageModel = new MessageModel(15.0)
			{
				SenderName = clanModel_0.Name,
				SenderId = long_1,
				ClanId = clanModel_0.Id,
				Type = NoteMessageType.Clan,
				State = NoteMessageState.Unreaded,
				ClanNote = NoteMessageClan.Staff
			};
			if (!DaoManagerSQL.CreateMessage(long_0, messageModel))
			{
				return null;
			}
			return messageModel;
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x00005496 File Offset: 0x00003696
		public PROTOCOL_CS_COMMISSION_STAFF_REQ()
		{
		}

		// Token: 0x040002F8 RID: 760
		private List<long> list_0 = new List<long>();

		// Token: 0x040002F9 RID: 761
		private uint uint_0;
	}
}
