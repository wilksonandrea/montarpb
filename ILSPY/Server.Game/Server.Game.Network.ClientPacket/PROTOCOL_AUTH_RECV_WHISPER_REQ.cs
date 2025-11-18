using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_AUTH_RECV_WHISPER_REQ : GameClientPacket
{
	private string string_0;

	private string string_1;

	public override void Read()
	{
		string_0 = ReadU(66);
		string_1 = ReadU(ReadH() * 2);
	}

	public override void Run()
	{
		try
		{
			Account player = Client.Player;
			if (player != null && !(player.Nickname == string_0))
			{
				Account account = AccountManager.GetAccount(string_0, 1, 0);
				if (account != null && !(account.Nickname != string_0) && account.IsOnline)
				{
					account.SendPacket(new PROTOCOL_AUTH_RECV_WHISPER_ACK(player.Nickname, string_1, player.UseChatGM()), OnlyInServer: false);
				}
				else
				{
					Client.SendPacket(new PROTOCOL_AUTH_SEND_WHISPER_ACK(string_0, string_1, 2147483648u));
				}
			}
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
		}
	}
}
