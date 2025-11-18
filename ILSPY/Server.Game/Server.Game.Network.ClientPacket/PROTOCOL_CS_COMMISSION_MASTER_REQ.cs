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

public class PROTOCOL_CS_COMMISSION_MASTER_REQ : GameClientPacket
{
	private long long_0;

	private uint uint_0;

	public override void Read()
	{
		long_0 = ReadQ();
	}

	public override void Run()
	{
		try
		{
			Account player = Client.Player;
			if (player == null || player.ClanAccess != 1)
			{
				return;
			}
			Account account = AccountManager.GetAccount(long_0, 31);
			int clanId = player.ClanId;
			if (account != null && account.ClanId == clanId)
			{
				if (account.Rank > 10)
				{
					ClanModel clan = ClanManager.GetClan(clanId);
					if (clan.Id > 0 && clan.OwnerId == Client.PlayerId && account.ClanAccess == 2 && ComDiv.UpdateDB("system_clan", "owner_id", long_0, "id", clanId) && ComDiv.UpdateDB("accounts", "clan_access", 1, "player_id", long_0) && ComDiv.UpdateDB("accounts", "clan_access", 2, "player_id", player.PlayerId))
					{
						account.ClanAccess = 1;
						player.ClanAccess = 2;
						clan.OwnerId = long_0;
						if (DaoManagerSQL.GetMessagesCount(account.PlayerId) < 100)
						{
							MessageModel messageModel = method_0(clan, account.PlayerId, player.PlayerId);
							if (messageModel != null && account.IsOnline)
							{
								account.SendPacket(new PROTOCOL_MESSENGER_NOTE_RECEIVE_ACK(messageModel), OnlyInServer: false);
							}
						}
						if (account.IsOnline)
						{
							account.SendPacket(new PROTOCOL_CS_COMMISSION_MASTER_RESULT_ACK(), OnlyInServer: false);
						}
					}
					else
					{
						uint_0 = 2147487744u;
					}
				}
				else
				{
					uint_0 = 2147487928u;
				}
			}
			else
			{
				uint_0 = 2147483648u;
			}
			Client.SendPacket(new PROTOCOL_CS_COMMISSION_MASTER_ACK(uint_0));
		}
		catch (Exception ex)
		{
			CLogger.Print("PROTOCOL_CS_COMMISSION_MASTER_REQ: " + ex.Message, LoggerType.Error, ex);
		}
	}

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
}
