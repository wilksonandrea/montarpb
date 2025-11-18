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

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_CS_COMMISSION_STAFF_REQ : GameClientPacket
{
	private List<long> list_0 = new List<long>();

	private uint uint_0;

	public override void Read()
	{
		int num = ReadC();
		for (int i = 0; i < num; i++)
		{
			long item = ReadQ();
			list_0.Add(item);
		}
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
			ClanModel clan = ClanManager.GetClan(player.ClanId);
			if (clan.Id != 0 && (player.ClanAccess == 1 || clan.OwnerId == Client.PlayerId))
			{
				for (int i = 0; i < list_0.Count; i++)
				{
					Account account = AccountManager.GetAccount(list_0[i], 31);
					if (account == null || account.ClanId != clan.Id || account.ClanAccess != 3 || !ComDiv.UpdateDB("accounts", "clan_access", 2, "player_id", account.PlayerId))
					{
						continue;
					}
					account.ClanAccess = 2;
					SendClanInfo.Load(account, null, 3);
					if (DaoManagerSQL.GetMessagesCount(account.PlayerId) < 100)
					{
						MessageModel messageModel = method_0(clan, account.PlayerId, Client.PlayerId);
						if (messageModel != null && account.IsOnline)
						{
							account.SendPacket(new PROTOCOL_MESSENGER_NOTE_RECEIVE_ACK(messageModel), OnlyInServer: false);
						}
					}
					if (account.IsOnline)
					{
						account.SendPacket(new PROTOCOL_CS_COMMISSION_STAFF_RESULT_ACK(), OnlyInServer: false);
					}
					uint_0++;
				}
				Client.SendPacket(new PROTOCOL_CS_COMMISSION_STAFF_ACK(uint_0));
			}
			else
			{
				uint_0 = 2147487833u;
			}
		}
		catch (Exception ex)
		{
			CLogger.Print("PROTOCOL_CS_COMMISSION_STAFF_REQ: " + ex.Message, LoggerType.Error, ex);
		}
	}

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
}
