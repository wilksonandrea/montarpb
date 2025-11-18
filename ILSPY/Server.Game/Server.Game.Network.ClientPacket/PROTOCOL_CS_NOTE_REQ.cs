using System;
using System.Collections.Generic;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.SQL;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_CS_NOTE_REQ : GameClientPacket
{
	private int int_0;

	private string string_0;

	public override void Read()
	{
		int_0 = ReadC();
		string_0 = ReadU(ReadC() * 2);
	}

	public override void Run()
	{
		try
		{
			Account player = Client.Player;
			if (string_0.Length > 120 || player == null)
			{
				return;
			}
			ClanModel clan = ClanManager.GetClan(player.ClanId);
			int num = 0;
			if (clan.Id > 0 && clan.OwnerId == Client.PlayerId)
			{
				List<Account> clanPlayers = ClanManager.GetClanPlayers(clan.Id, Client.PlayerId, UseCache: true);
				for (int i = 0; i < clanPlayers.Count; i++)
				{
					Account account = clanPlayers[i];
					if ((int_0 == 0 || (account.ClanAccess == 2 && int_0 == 1) || (account.ClanAccess == 3 && int_0 == 2)) && DaoManagerSQL.GetMessagesCount(account.PlayerId) < 100)
					{
						num++;
						MessageModel messageModel = method_0(clan, account.PlayerId, Client.PlayerId);
						if (messageModel != null && account.IsOnline)
						{
							account.SendPacket(new PROTOCOL_MESSENGER_NOTE_RECEIVE_ACK(messageModel), OnlyInServer: false);
						}
					}
				}
			}
			Client.SendPacket(new PROTOCOL_CS_NOTE_ACK(num));
			if (num > 0)
			{
				Client.SendPacket(new PROTOCOL_MESSENGER_NOTE_SEND_ACK(0u));
			}
		}
		catch (Exception ex)
		{
			CLogger.Print("PROTOCOL_CS_NOTE_REQ: " + ex.Message, LoggerType.Error, ex);
		}
	}

	private MessageModel method_0(ClanModel clanModel_0, long long_0, long long_1)
	{
		MessageModel messageModel = new MessageModel(15.0)
		{
			SenderName = clanModel_0.Name,
			SenderId = long_1,
			ClanId = clanModel_0.Id,
			Type = NoteMessageType.ClanInfo,
			Text = string_0,
			State = NoteMessageState.Unreaded
		};
		if (!DaoManagerSQL.CreateMessage(long_0, messageModel))
		{
			return null;
		}
		return messageModel;
	}
}
