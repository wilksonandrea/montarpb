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
	// Token: 0x020001AD RID: 429
	public class PROTOCOL_CS_SECESSION_CLAN_REQ : GameClientPacket
	{
		// Token: 0x06000479 RID: 1145 RVA: 0x00004D24 File Offset: 0x00002F24
		public override void Read()
		{
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x000227D8 File Offset: 0x000209D8
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					if (player.ClanId > 0)
					{
						ClanModel clan = ClanManager.GetClan(player.ClanId);
						if (clan.Id > 0 && clan.OwnerId != player.PlayerId)
						{
							if (ComDiv.UpdateDB("accounts", "player_id", player.PlayerId, new string[] { "clan_id", "clan_access" }, new object[] { 0, 0 }) && ComDiv.UpdateDB("player_stat_clans", "owner_id", player.PlayerId, new string[] { "clan_matches", "clan_match_wins" }, new object[] { 0, 0 }))
							{
								using (PROTOCOL_CS_MEMBER_INFO_DELETE_ACK protocol_CS_MEMBER_INFO_DELETE_ACK = new PROTOCOL_CS_MEMBER_INFO_DELETE_ACK(player.PlayerId))
								{
									ClanManager.SendPacket(protocol_CS_MEMBER_INFO_DELETE_ACK, player.ClanId, player.PlayerId, true, true);
								}
								long ownerId = clan.OwnerId;
								if (DaoManagerSQL.GetMessagesCount(ownerId) < 100)
								{
									MessageModel messageModel = this.method_0(clan, player);
									if (messageModel != null)
									{
										Account account = AccountManager.GetAccount(ownerId, 31);
										if (account != null && account.IsOnline)
										{
											account.SendPacket(new PROTOCOL_MESSENGER_NOTE_RECEIVE_ACK(messageModel), false);
										}
									}
								}
								player.ClanId = 0;
								player.ClanAccess = 0;
							}
							else
							{
								this.uint_0 = 2147487851U;
							}
						}
						else
						{
							this.uint_0 = 2147487838U;
						}
					}
					else
					{
						this.uint_0 = 2147487835U;
					}
					this.Client.SendPacket(new PROTOCOL_CS_SECESSION_CLAN_ACK(this.uint_0));
				}
			}
			catch (Exception ex)
			{
				CLogger.Print("PROTOCOL_CS_SECESSION_CLAN_REQ: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x000229D8 File Offset: 0x00020BD8
		private MessageModel method_0(ClanModel clanModel_0, Account account_0)
		{
			MessageModel messageModel = new MessageModel(15.0)
			{
				SenderName = clanModel_0.Name,
				SenderId = account_0.PlayerId,
				ClanId = clanModel_0.Id,
				Type = NoteMessageType.Clan,
				Text = account_0.Nickname,
				State = NoteMessageState.Unreaded,
				ClanNote = NoteMessageClan.Secession
			};
			if (!DaoManagerSQL.CreateMessage(clanModel_0.OwnerId, messageModel))
			{
				return null;
			}
			return messageModel;
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_CS_SECESSION_CLAN_REQ()
		{
		}

		// Token: 0x0400031B RID: 795
		private uint uint_0;
	}
}
