using System;
using System.Collections.Generic;
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
	// Token: 0x0200019F RID: 415
	public class PROTOCOL_CS_INVITE_ACCEPT_REQ : GameClientPacket
	{
		// Token: 0x06000449 RID: 1097 RVA: 0x00005518 File Offset: 0x00003718
		public override void Read()
		{
			this.int_0 = base.ReadD();
			this.int_1 = (int)base.ReadC();
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x000216C0 File Offset: 0x0001F8C0
		public override void Run()
		{
			Account player = this.Client.Player;
			if (player == null || player.Nickname.Length == 0)
			{
				return;
			}
			ClanModel clan = ClanManager.GetClan(this.int_0);
			List<Account> clanPlayers = ClanManager.GetClanPlayers(this.int_0, -1L, true);
			if (clan.Id == 0)
			{
				this.Client.SendPacket(new PROTOCOL_CS_INVITE_ACCEPT_ACK(2147487835U));
				return;
			}
			if (player.ClanId > 0)
			{
				this.Client.SendPacket(new PROTOCOL_CS_INVITE_ACCEPT_ACK(2147487832U));
				return;
			}
			if (clan.MaxPlayers <= clanPlayers.Count)
			{
				this.Client.SendPacket(new PROTOCOL_CS_INVITE_ACCEPT_ACK(2147487830U));
				return;
			}
			if (this.int_1 == 0 || this.int_1 == 1)
			{
				try
				{
					uint num = 0U;
					Account account = AccountManager.GetAccount(clan.OwnerId, 31);
					if (account != null)
					{
						if (DaoManagerSQL.GetMessagesCount(clan.OwnerId) < 100)
						{
							MessageModel messageModel = this.method_0(clan, player.Nickname, this.Client.PlayerId);
							if (messageModel != null && account.IsOnline)
							{
								account.SendPacket(new PROTOCOL_MESSENGER_NOTE_RECEIVE_ACK(messageModel), false);
							}
						}
						if (this.int_1 == 1)
						{
							uint num2 = uint.Parse(DateTimeUtil.Now("yyyyMMdd"));
							if (ComDiv.UpdateDB("accounts", "player_id", player.PlayerId, new string[] { "clan_id", "clan_access", "clan_date" }, new object[]
							{
								clan.Id,
								3,
								(long)((ulong)num2)
							}))
							{
								using (PROTOCOL_CS_MEMBER_INFO_INSERT_ACK protocol_CS_MEMBER_INFO_INSERT_ACK = new PROTOCOL_CS_MEMBER_INFO_INSERT_ACK(player))
								{
									ClanManager.SendPacket(protocol_CS_MEMBER_INFO_INSERT_ACK, clanPlayers);
								}
								player.ClanId = clan.Id;
								player.ClanDate = num2;
								player.ClanAccess = 3;
								this.Client.SendPacket(new PROTOCOL_CS_MEMBER_INFO_ACK(clanPlayers));
								RoomModel room = player.Room;
								if (room != null)
								{
									room.SendPacketToPlayers(new PROTOCOL_ROOM_GET_SLOTONEINFO_ACK(player, clan));
								}
								this.Client.SendPacket(new PROTOCOL_CS_ACCEPT_REQUEST_RESULT_ACK(clan, account, clanPlayers.Count + 1));
							}
							else
							{
								num = 2147483648U;
							}
						}
					}
					else
					{
						num = 2147483648U;
					}
					this.Client.SendPacket(new PROTOCOL_MESSENGER_NOTE_SEND_ACK(num));
				}
				catch (Exception ex)
				{
					CLogger.Print(ex.Message, LoggerType.Error, ex);
				}
			}
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x00021950 File Offset: 0x0001FB50
		private MessageModel method_0(ClanModel clanModel_0, string string_0, long long_0)
		{
			MessageModel messageModel = new MessageModel(15.0)
			{
				SenderName = clanModel_0.Name,
				SenderId = long_0,
				ClanId = clanModel_0.Id,
				Type = NoteMessageType.Clan,
				Text = string_0,
				State = NoteMessageState.Unreaded,
				ClanNote = ((this.int_1 == 0) ? NoteMessageClan.JoinDenial : NoteMessageClan.JoinAccept)
			};
			if (!DaoManagerSQL.CreateMessage(clanModel_0.OwnerId, messageModel))
			{
				return null;
			}
			return messageModel;
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_CS_INVITE_ACCEPT_REQ()
		{
		}

		// Token: 0x04000303 RID: 771
		private int int_0;

		// Token: 0x04000304 RID: 772
		private int int_1;
	}
}
