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

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_CS_INVITE_ACCEPT_REQ : GameClientPacket
{
	private int int_0;

	private int int_1;

	public override void Read()
	{
		int_0 = ReadD();
		int_1 = ReadC();
	}

	public override void Run()
	{
		Account player = Client.Player;
		if (player == null || player.Nickname.Length == 0)
		{
			return;
		}
		ClanModel clan = ClanManager.GetClan(int_0);
		List<Account> clanPlayers = ClanManager.GetClanPlayers(int_0, -1L, UseCache: true);
		if (clan.Id == 0)
		{
			Client.SendPacket(new PROTOCOL_CS_INVITE_ACCEPT_ACK(2147487835u));
		}
		else if (player.ClanId > 0)
		{
			Client.SendPacket(new PROTOCOL_CS_INVITE_ACCEPT_ACK(2147487832u));
		}
		else if (clan.MaxPlayers <= clanPlayers.Count)
		{
			Client.SendPacket(new PROTOCOL_CS_INVITE_ACCEPT_ACK(2147487830u));
		}
		else
		{
			if (int_1 != 0 && int_1 != 1)
			{
				return;
			}
			try
			{
				uint uint_ = 0u;
				Account account = AccountManager.GetAccount(clan.OwnerId, 31);
				if (account != null)
				{
					if (DaoManagerSQL.GetMessagesCount(clan.OwnerId) < 100)
					{
						MessageModel messageModel = method_0(clan, player.Nickname, Client.PlayerId);
						if (messageModel != null && account.IsOnline)
						{
							account.SendPacket(new PROTOCOL_MESSENGER_NOTE_RECEIVE_ACK(messageModel), OnlyInServer: false);
						}
					}
					if (int_1 == 1)
					{
						uint num = uint.Parse(DateTimeUtil.Now("yyyyMMdd"));
						if (ComDiv.UpdateDB("accounts", "player_id", player.PlayerId, new string[3] { "clan_id", "clan_access", "clan_date" }, clan.Id, 3, (long)num))
						{
							using (PROTOCOL_CS_MEMBER_INFO_INSERT_ACK packet = new PROTOCOL_CS_MEMBER_INFO_INSERT_ACK(player))
							{
								ClanManager.SendPacket(packet, clanPlayers);
							}
							player.ClanId = clan.Id;
							player.ClanDate = num;
							player.ClanAccess = 3;
							Client.SendPacket(new PROTOCOL_CS_MEMBER_INFO_ACK(clanPlayers));
							player.Room?.SendPacketToPlayers(new PROTOCOL_ROOM_GET_SLOTONEINFO_ACK(player, clan));
							Client.SendPacket(new PROTOCOL_CS_ACCEPT_REQUEST_RESULT_ACK(clan, account, clanPlayers.Count + 1));
						}
						else
						{
							uint_ = 2147483648u;
						}
					}
				}
				else
				{
					uint_ = 2147483648u;
				}
				Client.SendPacket(new PROTOCOL_MESSENGER_NOTE_SEND_ACK(uint_));
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
		}
	}

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
			ClanNote = ((int_1 == 0) ? NoteMessageClan.JoinDenial : NoteMessageClan.JoinAccept)
		};
		if (!DaoManagerSQL.CreateMessage(clanModel_0.OwnerId, messageModel))
		{
			return null;
		}
		return messageModel;
	}
}
