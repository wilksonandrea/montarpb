using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.SQL;
using Plugin.Core.Utility;
using Server.Game;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;
using System.Collections.Generic;

namespace Server.Game.Network.ClientPacket
{
	public class PROTOCOL_CS_INVITE_ACCEPT_REQ : GameClientPacket
	{
		private int int_0;

		private int int_1;

		public PROTOCOL_CS_INVITE_ACCEPT_REQ()
		{
		}

		private MessageModel method_0(ClanModel clanModel_0, string string_0, long long_0)
		{
			MessageModel messageModel = new MessageModel(15)
			{
				SenderName = clanModel_0.Name,
				SenderId = long_0,
				ClanId = clanModel_0.Id,
				Type = NoteMessageType.Clan,
				Text = string_0,
				State = NoteMessageState.Unreaded,
				ClanNote = (this.int_1 == 0 ? NoteMessageClan.JoinDenial : NoteMessageClan.JoinAccept)
			};
			if (!DaoManagerSQL.CreateMessage(clanModel_0.OwnerId, messageModel))
			{
				return null;
			}
			return messageModel;
		}

		public override void Read()
		{
			this.int_0 = base.ReadD();
			this.int_1 = base.ReadC();
		}

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
				this.Client.SendPacket(new PROTOCOL_CS_INVITE_ACCEPT_ACK(-2147479461));
				return;
			}
			if (player.ClanId > 0)
			{
				this.Client.SendPacket(new PROTOCOL_CS_INVITE_ACCEPT_ACK(-2147479464));
				return;
			}
			if (clan.MaxPlayers <= clanPlayers.Count)
			{
				this.Client.SendPacket(new PROTOCOL_CS_INVITE_ACCEPT_ACK(-2147479466));
				return;
			}
			if (this.int_1 == 0 || this.int_1 == 1)
			{
				try
				{
					uint uInt32 = 0;
					Account account = AccountManager.GetAccount(clan.OwnerId, 31);
					if (account == null)
					{
						uInt32 = -2147483648;
					}
					else
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
							uint uInt321 = uint.Parse(DateTimeUtil.Now("yyyyMMdd"));
							if (!ComDiv.UpdateDB("accounts", "player_id", player.PlayerId, new string[] { "clan_id", "clan_access", "clan_date" }, new object[] { clan.Id, 3, (long)((ulong)uInt321) }))
							{
								uInt32 = -2147483648;
							}
							else
							{
								using (PROTOCOL_CS_MEMBER_INFO_INSERT_ACK pROTOCOLCSMEMBERINFOINSERTACK = new PROTOCOL_CS_MEMBER_INFO_INSERT_ACK(player))
								{
									ClanManager.SendPacket(pROTOCOLCSMEMBERINFOINSERTACK, clanPlayers);
								}
								player.ClanId = clan.Id;
								player.ClanDate = uInt321;
								player.ClanAccess = 3;
								this.Client.SendPacket(new PROTOCOL_CS_MEMBER_INFO_ACK(clanPlayers));
								RoomModel room = player.Room;
								if (room != null)
								{
									room.SendPacketToPlayers(new PROTOCOL_ROOM_GET_SLOTONEINFO_ACK(player, clan));
								}
								this.Client.SendPacket(new PROTOCOL_CS_ACCEPT_REQUEST_RESULT_ACK(clan, account, clanPlayers.Count + 1));
							}
						}
					}
					this.Client.SendPacket(new PROTOCOL_MESSENGER_NOTE_SEND_ACK(uInt32));
				}
				catch (Exception exception1)
				{
					Exception exception = exception1;
					CLogger.Print(exception.Message, LoggerType.Error, exception);
				}
			}
		}
	}
}