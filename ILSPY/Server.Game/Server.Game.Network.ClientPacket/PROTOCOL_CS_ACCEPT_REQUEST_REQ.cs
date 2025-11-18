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

public class PROTOCOL_CS_ACCEPT_REQUEST_REQ : GameClientPacket
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
			if (clan.Id > 0 && ((player.ClanAccess >= 1 && player.ClanAccess <= 2) || player.PlayerId == clan.OwnerId))
			{
				List<Account> clanPlayers = ClanManager.GetClanPlayers(clan.Id, -1L, UseCache: true);
				if (clanPlayers.Count >= clan.MaxPlayers)
				{
					int_0 = -1;
					return;
				}
				for (int i = 0; i < list_0.Count; i++)
				{
					Account account = AccountManager.GetAccount(list_0[i], 31);
					if (account == null || clanPlayers.Count >= clan.MaxPlayers || account.ClanId != 0 || DaoManagerSQL.GetRequestClanId(account.PlayerId) <= 0)
					{
						continue;
					}
					using (PROTOCOL_CS_MEMBER_INFO_CHANGE_ACK packet = new PROTOCOL_CS_MEMBER_INFO_CHANGE_ACK(account))
					{
						ClanManager.SendPacket(packet, clanPlayers);
					}
					account.ClanId = player.ClanId;
					account.ClanDate = uint.Parse(DateTimeUtil.Now("yyyyMMdd"));
					account.ClanAccess = 3;
					SendClanInfo.Load(account, null, 3);
					ComDiv.UpdateDB("accounts", "player_id", account.PlayerId, new string[3] { "clan_access", "clan_id", "clan_date" }, account.ClanAccess, account.ClanId, (long)account.ClanDate);
					DaoManagerSQL.DeleteClanInviteDB(player.ClanId, account.PlayerId);
					if (account.IsOnline)
					{
						account.SendPacket(new PROTOCOL_CS_MEMBER_INFO_ACK(clanPlayers), OnlyInServer: false);
						account.Room?.SendPacketToPlayers(new PROTOCOL_ROOM_GET_SLOTONEINFO_ACK(account, clan));
						account.SendPacket(new PROTOCOL_CS_ACCEPT_REQUEST_RESULT_ACK(clan, clanPlayers.Count + 1), OnlyInServer: false);
					}
					if (DaoManagerSQL.GetMessagesCount(account.PlayerId) < 100)
					{
						MessageModel messageModel = method_0(clan, account.PlayerId, Client.PlayerId);
						if (messageModel != null && account.IsOnline)
						{
							account.SendPacket(new PROTOCOL_MESSENGER_NOTE_RECEIVE_ACK(messageModel), OnlyInServer: false);
						}
					}
					int_0++;
					clanPlayers.Add(account);
				}
				clanPlayers = null;
			}
			else
			{
				int_0 = -1;
			}
			Client.SendPacket(new PROTOCOL_CS_ACCEPT_REQUEST_ACK((uint)int_0));
		}
		catch (Exception ex)
		{
			CLogger.Print("PROTOCOL_CS_ACCEPT_REQUEST_RESULT_REQ: " + ex.Message, LoggerType.Error, ex);
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
			ClanNote = NoteMessageClan.InviteAccept
		};
		if (!DaoManagerSQL.CreateMessage(long_0, messageModel))
		{
			return null;
		}
		return messageModel;
	}
}
