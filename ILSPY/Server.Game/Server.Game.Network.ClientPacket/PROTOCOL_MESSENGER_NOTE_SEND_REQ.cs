using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.SQL;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_MESSENGER_NOTE_SEND_REQ : GameClientPacket
{
	private int int_0;

	private int int_1;

	private string string_0;

	private string string_1;

	private uint uint_0;

	public override void Read()
	{
		int_0 = ReadC();
		int_1 = ReadC();
		string_0 = ReadU(int_0 * 2);
		string_1 = ReadU(int_1 * 2);
	}

	public override void Run()
	{
		try
		{
			Account player = Client.Player;
			if (player == null || player.Nickname.Length == 0 || player.Nickname == string_0)
			{
				return;
			}
			Account account = AccountManager.GetAccount(string_0, 1, 0);
			if (account != null)
			{
				if (DaoManagerSQL.GetMessagesCount(account.PlayerId) >= 100)
				{
					uint_0 = 2147487871u;
				}
				else
				{
					MessageModel messageModel = method_0(player.Nickname, account.PlayerId, Client.PlayerId);
					if (messageModel != null)
					{
						account.SendPacket(new PROTOCOL_MESSENGER_NOTE_RECEIVE_ACK(messageModel), OnlyInServer: false);
					}
				}
			}
			else
			{
				uint_0 = 2147487870u;
			}
			Client.SendPacket(new PROTOCOL_MESSENGER_NOTE_SEND_ACK(uint_0));
		}
		catch (Exception ex)
		{
			CLogger.Print("PROTOCOL_MESSENGER_NOTE_SEND_REQ: " + ex.Message, LoggerType.Error, ex);
		}
	}

	private MessageModel method_0(string string_2, long long_0, long long_1)
	{
		MessageModel messageModel = new MessageModel(15.0)
		{
			SenderName = string_2,
			SenderId = long_1,
			Text = string_1,
			State = NoteMessageState.Unreaded
		};
		if (!DaoManagerSQL.CreateMessage(long_0, messageModel))
		{
			uint_0 = 2147483648u;
			return null;
		}
		return messageModel;
	}
}
