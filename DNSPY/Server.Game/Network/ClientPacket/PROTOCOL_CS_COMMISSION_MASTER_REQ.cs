using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.SQL;
using Plugin.Core.Utility;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x02000197 RID: 407
	public class PROTOCOL_CS_COMMISSION_MASTER_REQ : GameClientPacket
	{
		// Token: 0x0600042C RID: 1068 RVA: 0x00005475 File Offset: 0x00003675
		public override void Read()
		{
			this.long_0 = base.ReadQ();
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x000207A4 File Offset: 0x0001E9A4
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					if (player.ClanAccess == 1)
					{
						Account account = AccountManager.GetAccount(this.long_0, 31);
						int clanId = player.ClanId;
						if (account != null)
						{
							if (account.ClanId == clanId)
							{
								if (account.Rank <= 10)
								{
									this.uint_0 = 2147487928U;
									goto IL_199;
								}
								ClanModel clan = ClanManager.GetClan(clanId);
								if (clan.Id <= 0 || clan.OwnerId != this.Client.PlayerId || account.ClanAccess != 2 || !ComDiv.UpdateDB("system_clan", "owner_id", this.long_0, "id", clanId) || !ComDiv.UpdateDB("accounts", "clan_access", 1, "player_id", this.long_0) || !ComDiv.UpdateDB("accounts", "clan_access", 2, "player_id", player.PlayerId))
								{
									this.uint_0 = 2147487744U;
									goto IL_199;
								}
								account.ClanAccess = 1;
								player.ClanAccess = 2;
								clan.OwnerId = this.long_0;
								if (DaoManagerSQL.GetMessagesCount(account.PlayerId) < 100)
								{
									MessageModel messageModel = this.method_0(clan, account.PlayerId, player.PlayerId);
									if (messageModel != null && account.IsOnline)
									{
										account.SendPacket(new PROTOCOL_MESSENGER_NOTE_RECEIVE_ACK(messageModel), false);
									}
								}
								if (account.IsOnline)
								{
									account.SendPacket(new PROTOCOL_CS_COMMISSION_MASTER_RESULT_ACK(), false);
									goto IL_199;
								}
								goto IL_199;
							}
						}
						this.uint_0 = 2147483648U;
						IL_199:
						this.Client.SendPacket(new PROTOCOL_CS_COMMISSION_MASTER_ACK(this.uint_0));
					}
				}
			}
			catch (Exception ex)
			{
				CLogger.Print("PROTOCOL_CS_COMMISSION_MASTER_REQ: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x000209A0 File Offset: 0x0001EBA0
		private MessageModel method_0(ClanModel clanModel_0, long long_1, long long_2)
		{
			MessageModel messageModel = new MessageModel(15.0)
			{
				SenderName = clanModel_0.Name,
				SenderId = long_2,
				ClanId = clanModel_0.Id,
				Type = NoteMessageType.Clan,
				State = NoteMessageState.Unreaded,
				ClanNote = NoteMessageClan.Master
			};
			if (!DaoManagerSQL.CreateMessage(long_1, messageModel))
			{
				return null;
			}
			return messageModel;
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_CS_COMMISSION_MASTER_REQ()
		{
		}

		// Token: 0x040002F4 RID: 756
		private long long_0;

		// Token: 0x040002F5 RID: 757
		private uint uint_0;
	}
}
