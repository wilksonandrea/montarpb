using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.SQL;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_CS_INVITE_REQ : GameClientPacket
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
			if (player == null || player.ClanId == 0 || player.FindPlayer == "" || player.FindPlayer.Length == 0)
			{
				return;
			}
			Account account = AccountManager.GetAccount(player.FindPlayer, 1, 0);
			if (account != null)
			{
				if (account.ClanId == 0 && player.ClanId != 0)
				{
					method_0(account, player.ClanId);
				}
				else
				{
					uint_0 = 2147483648u;
				}
			}
			else
			{
				uint_0 = 2147483648u;
			}
			Client.SendPacket(new PROTOCOL_CS_INVITE_ACK(uint_0));
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
		}
	}

	private void method_0(Account account_0, int int_0)
	{
		if (DaoManagerSQL.GetMessagesCount(account_0.PlayerId) >= 100)
		{
			uint_0 = 2147483648u;
			return;
		}
		MessageModel messageModel = method_1(int_0, account_0.PlayerId, Client.PlayerId);
		if (messageModel != null && account_0.IsOnline)
		{
			account_0.SendPacket(new PROTOCOL_MESSENGER_NOTE_RECEIVE_ACK(messageModel), OnlyInServer: false);
		}
	}

	private MessageModel method_1(int int_0, long long_0, long long_1)
	{
		MessageModel messageModel = new MessageModel(15.0)
		{
			SenderName = ClanManager.GetClan(int_0).Name,
			ClanId = int_0,
			SenderId = long_1,
			Type = NoteMessageType.ClanAsk,
			State = NoteMessageState.Unreaded,
			ClanNote = NoteMessageClan.Invite
		};
		if (!DaoManagerSQL.CreateMessage(long_0, messageModel))
		{
			return null;
		}
		return messageModel;
	}
}
