using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.SQL;
using Plugin.Core.Utility;
using Server.Game;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;

namespace Server.Game.Network.ClientPacket
{
	public class PROTOCOL_CS_SECESSION_CLAN_REQ : GameClientPacket
	{
		private uint uint_0;

		public PROTOCOL_CS_SECESSION_CLAN_REQ()
		{
		}

		private MessageModel method_0(ClanModel clanModel_0, Account account_0)
		{
			MessageModel messageModel = new MessageModel(15)
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

		public override void Read()
		{
		}

		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					if (player.ClanId <= 0)
					{
						this.uint_0 = -2147479461;
					}
					else
					{
						ClanModel clan = ClanManager.GetClan(player.ClanId);
						if (clan.Id <= 0 || clan.OwnerId == player.PlayerId)
						{
							this.uint_0 = -2147479458;
						}
						else
						{
							if (ComDiv.UpdateDB("accounts", "player_id", player.PlayerId, new string[] { "clan_id", "clan_access" }, new object[] { 0, 0 }))
							{
								if (!ComDiv.UpdateDB("player_stat_clans", "owner_id", player.PlayerId, new string[] { "clan_matches", "clan_match_wins" }, new object[] { 0, 0 }))
								{
									goto Label1;
								}
								using (PROTOCOL_CS_MEMBER_INFO_DELETE_ACK pROTOCOLCSMEMBERINFODELETEACK = new PROTOCOL_CS_MEMBER_INFO_DELETE_ACK(player.PlayerId))
								{
									ClanManager.SendPacket(pROTOCOLCSMEMBERINFODELETEACK, player.ClanId, player.PlayerId, true, true);
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
								goto Label0;
							}
						Label1:
							this.uint_0 = -2147479445;
						}
					}
				Label0:
					this.Client.SendPacket(new PROTOCOL_CS_SECESSION_CLAN_ACK(this.uint_0));
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("PROTOCOL_CS_SECESSION_CLAN_REQ: ", exception.Message), LoggerType.Error, exception);
			}
		}
	}
}