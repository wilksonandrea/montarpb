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

public class PROTOCOL_CS_DENIAL_REQUEST_REQ : GameClientPacket
{
	private List<long> list_0 = new List<long>();

	private int int_0;

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
			if (clan.Id > 0 && ((player.ClanAccess >= 1 && player.ClanAccess <= 2) || clan.OwnerId == player.PlayerId))
			{
				for (int i = 0; i < list_0.Count; i++)
				{
					long num = list_0[i];
					if (!DaoManagerSQL.DeleteClanInviteDB(clan.Id, num))
					{
						continue;
					}
					if (DaoManagerSQL.GetMessagesCount(num) < 100)
					{
						MessageModel messageModel = method_0(clan, num, player.PlayerId);
						if (messageModel != null)
						{
							Account account = AccountManager.GetAccount(num, 31);
							if (account != null && account.IsOnline)
							{
								account.SendPacket(new PROTOCOL_MESSENGER_NOTE_RECEIVE_ACK(messageModel), OnlyInServer: false);
							}
						}
					}
					int_0++;
				}
			}
			Client.SendPacket(new PROTOCOL_CS_DENIAL_REQUEST_ACK(int_0));
		}
		catch (Exception ex)
		{
			CLogger.Print("PROTOCOL_CS_DENIAL_REQUEST_REQ: " + ex.Message, LoggerType.Error, ex);
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
			ClanNote = NoteMessageClan.InviteDenial
		};
		if (!DaoManagerSQL.CreateMessage(long_0, messageModel))
		{
			return null;
		}
		return messageModel;
	}
}
