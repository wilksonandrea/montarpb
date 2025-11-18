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
	// Token: 0x0200018A RID: 394
	public class PROTOCOL_CS_ACCEPT_REQUEST_REQ : GameClientPacket
	{
		// Token: 0x06000403 RID: 1027 RVA: 0x0001FAF4 File Offset: 0x0001DCF4
		public override void Read()
		{
			int num = (int)base.ReadC();
			for (int i = 0; i < num; i++)
			{
				long num2 = base.ReadQ();
				this.list_0.Add(num2);
			}
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x0001FB28 File Offset: 0x0001DD28
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					ClanModel clan = ClanManager.GetClan(player.ClanId);
					if (clan.Id > 0 && ((player.ClanAccess >= 1 && player.ClanAccess <= 2) || player.PlayerId == clan.OwnerId))
					{
						List<Account> list = ClanManager.GetClanPlayers(clan.Id, -1L, true);
						if (list.Count >= clan.MaxPlayers)
						{
							this.int_0 = -1;
							return;
						}
						for (int i = 0; i < this.list_0.Count; i++)
						{
							Account account = AccountManager.GetAccount(this.list_0[i], 31);
							if (account != null && list.Count < clan.MaxPlayers && account.ClanId == 0 && DaoManagerSQL.GetRequestClanId(account.PlayerId) > 0)
							{
								using (PROTOCOL_CS_MEMBER_INFO_CHANGE_ACK protocol_CS_MEMBER_INFO_CHANGE_ACK = new PROTOCOL_CS_MEMBER_INFO_CHANGE_ACK(account))
								{
									ClanManager.SendPacket(protocol_CS_MEMBER_INFO_CHANGE_ACK, list);
								}
								account.ClanId = player.ClanId;
								account.ClanDate = uint.Parse(DateTimeUtil.Now("yyyyMMdd"));
								account.ClanAccess = 3;
								SendClanInfo.Load(account, null, 3);
								ComDiv.UpdateDB("accounts", "player_id", account.PlayerId, new string[] { "clan_access", "clan_id", "clan_date" }, new object[]
								{
									account.ClanAccess,
									account.ClanId,
									(long)((ulong)account.ClanDate)
								});
								DaoManagerSQL.DeleteClanInviteDB(player.ClanId, account.PlayerId);
								if (account.IsOnline)
								{
									account.SendPacket(new PROTOCOL_CS_MEMBER_INFO_ACK(list), false);
									RoomModel room = account.Room;
									if (room != null)
									{
										room.SendPacketToPlayers(new PROTOCOL_ROOM_GET_SLOTONEINFO_ACK(account, clan));
									}
									account.SendPacket(new PROTOCOL_CS_ACCEPT_REQUEST_RESULT_ACK(clan, list.Count + 1), false);
								}
								if (DaoManagerSQL.GetMessagesCount(account.PlayerId) < 100)
								{
									MessageModel messageModel = this.method_0(clan, account.PlayerId, this.Client.PlayerId);
									if (messageModel != null && account.IsOnline)
									{
										account.SendPacket(new PROTOCOL_MESSENGER_NOTE_RECEIVE_ACK(messageModel), false);
									}
								}
								this.int_0++;
								list.Add(account);
							}
						}
						list = null;
					}
					else
					{
						this.int_0 = -1;
					}
					this.Client.SendPacket(new PROTOCOL_CS_ACCEPT_REQUEST_ACK((uint)this.int_0));
				}
			}
			catch (Exception ex)
			{
				CLogger.Print("PROTOCOL_CS_ACCEPT_REQUEST_RESULT_REQ: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x0001FE0C File Offset: 0x0001E00C
		private MessageModel method_0(ClanModel clanModel_0, long long_0, long long_1)
		{
			MessageModel messageModel = new MessageModel(15.0)
			{
				SenderName = clanModel_0.Name,
				SenderId = long_1,
				ClanId = clanModel_0.Id,
				Type = NoteMessageType.Clan,
				State = NoteMessageState.Unreaded,
				ClanNote = NoteMessageClan.InviteAccept
			};
			if (!DaoManagerSQL.CreateMessage(long_0, messageModel))
			{
				return null;
			}
			return messageModel;
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x000053A5 File Offset: 0x000035A5
		public PROTOCOL_CS_ACCEPT_REQUEST_REQ()
		{
		}

		// Token: 0x040002E1 RID: 737
		private List<long> list_0 = new List<long>();

		// Token: 0x040002E2 RID: 738
		private int int_0;
	}
}
