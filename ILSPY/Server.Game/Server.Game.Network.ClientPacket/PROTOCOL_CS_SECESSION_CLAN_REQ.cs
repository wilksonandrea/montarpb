using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.SQL;
using Plugin.Core.Utility;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_CS_SECESSION_CLAN_REQ : GameClientPacket
{
	private uint uint_0;

	public override void Read()
	{
	}

	public override void Run()
	{
		try
		{
			Account player = Client.Player;
			if (player == null)
			{
				return;
			}
			if (player.ClanId > 0)
			{
				ClanModel clan = ClanManager.GetClan(player.ClanId);
				if (clan.Id > 0 && clan.OwnerId != player.PlayerId)
				{
					if (ComDiv.UpdateDB("accounts", "player_id", player.PlayerId, new string[2] { "clan_id", "clan_access" }, 0, 0) && ComDiv.UpdateDB("player_stat_clans", "owner_id", player.PlayerId, new string[2] { "clan_matches", "clan_match_wins" }, 0, 0))
					{
						using (PROTOCOL_CS_MEMBER_INFO_DELETE_ACK packet = new PROTOCOL_CS_MEMBER_INFO_DELETE_ACK(player.PlayerId))
						{
							ClanManager.SendPacket(packet, player.ClanId, player.PlayerId, UseCache: true, IsOnline: true);
						}
						long ownerId = clan.OwnerId;
						if (DaoManagerSQL.GetMessagesCount(ownerId) < 100)
						{
							MessageModel messageModel = method_0(clan, player);
							if (messageModel != null)
							{
								Account account = AccountManager.GetAccount(ownerId, 31);
								if (account != null && account.IsOnline)
								{
									account.SendPacket(new PROTOCOL_MESSENGER_NOTE_RECEIVE_ACK(messageModel), OnlyInServer: false);
								}
							}
						}
						player.ClanId = 0;
						player.ClanAccess = 0;
					}
					else
					{
						uint_0 = 2147487851u;
					}
				}
				else
				{
					uint_0 = 2147487838u;
				}
			}
			else
			{
				uint_0 = 2147487835u;
			}
			Client.SendPacket(new PROTOCOL_CS_SECESSION_CLAN_ACK(uint_0));
		}
		catch (Exception ex)
		{
			CLogger.Print("PROTOCOL_CS_SECESSION_CLAN_REQ: " + ex.Message, LoggerType.Error, ex);
		}
	}

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
}
