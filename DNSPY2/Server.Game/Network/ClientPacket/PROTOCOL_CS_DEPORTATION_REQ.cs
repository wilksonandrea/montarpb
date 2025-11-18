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
	// Token: 0x0200019D RID: 413
	public class PROTOCOL_CS_DEPORTATION_REQ : GameClientPacket
	{
		// Token: 0x06000442 RID: 1090 RVA: 0x00021304 File Offset: 0x0001F504
		public override void Read()
		{
			int num = (int)base.ReadC();
			for (int i = 0; i < num; i++)
			{
				long num2 = base.ReadQ();
				this.list_0.Add(num2);
			}
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x00021338 File Offset: 0x0001F538
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
						if ((player.ClanAccess >= 1 && player.ClanAccess <= 2) || clan.OwnerId == this.Client.PlayerId)
						{
							List<Account> clanPlayers = ClanManager.GetClanPlayers(clan.Id, -1L, true);
							for (int i = 0; i < this.list_0.Count; i++)
							{
								Account account = AccountManager.GetAccount(this.list_0[i], 31);
								if (account == null || account.ClanId != clan.Id || account.Match != null || !ComDiv.UpdateDB("accounts", "player_id", account.PlayerId, new string[] { "clan_id", "clan_access" }, new object[] { 0, 0 }) || !ComDiv.UpdateDB("player_stat_clans", "owner_id", account.PlayerId, new string[] { "clan_matches", "clan_match_wins" }, new object[] { 0, 0 }))
								{
									this.uint_0 = 2147487833U;
									IL_222:
									this.Client.SendPacket(new PROTOCOL_CS_DEPORTATION_ACK(this.uint_0));
									return;
								}
								using (PROTOCOL_CS_MEMBER_INFO_DELETE_ACK protocol_CS_MEMBER_INFO_DELETE_ACK = new PROTOCOL_CS_MEMBER_INFO_DELETE_ACK(account.PlayerId))
								{
									ClanManager.SendPacket(protocol_CS_MEMBER_INFO_DELETE_ACK, clanPlayers, account.PlayerId);
								}
								account.ClanId = 0;
								account.ClanAccess = 0;
								SendClanInfo.Load(account, null, 0);
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
									account.SendPacket(new PROTOCOL_CS_DEPORTATION_RESULT_ACK(), false);
								}
								this.uint_0 += 1U;
								clanPlayers.Remove(account);
							}
							goto IL_222;
						}
					}
					this.uint_0 = 2147487833U;
				}
			}
			catch (Exception ex)
			{
				CLogger.Print("PROTOCOL_CS_DEPORTATION_REQ: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x000215E0 File Offset: 0x0001F7E0
		private MessageModel method_0(ClanModel clanModel_0, long long_0, long long_1)
		{
			MessageModel messageModel = new MessageModel(15.0)
			{
				SenderName = clanModel_0.Name,
				SenderId = long_1,
				ClanId = clanModel_0.Id,
				Type = NoteMessageType.Clan,
				State = NoteMessageState.Unreaded,
				ClanNote = NoteMessageClan.Deportation
			};
			if (!DaoManagerSQL.CreateMessage(long_0, messageModel))
			{
				return null;
			}
			return messageModel;
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x000054EB File Offset: 0x000036EB
		public PROTOCOL_CS_DEPORTATION_REQ()
		{
		}

		// Token: 0x040002FF RID: 767
		private List<long> list_0 = new List<long>();

		// Token: 0x04000300 RID: 768
		private uint uint_0;
	}
}
