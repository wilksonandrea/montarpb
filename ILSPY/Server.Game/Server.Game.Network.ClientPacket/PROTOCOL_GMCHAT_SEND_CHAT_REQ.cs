using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_GMCHAT_SEND_CHAT_REQ : GameClientPacket
{
	private long long_0;

	private string string_0;

	private string string_1;

	private string string_2;

	public override void Read()
	{
		string_0 = ReadU(ReadC() * 2);
		string_2 = ReadU(ReadH() * 2);
		string_1 = ReadU(ReadC() * 2);
		long_0 = ReadQ();
	}

	public override void Run()
	{
		try
		{
			Account player = Client.Player;
			if (player != null)
			{
				Account account = AccountManager.GetAccount(string_0, 1, 31);
				if (account != null && player.Nickname != account.Nickname)
				{
					account.SendPacket(new PROTOCOL_GMCHAT_SEND_CHAT_ACK(string_0, string_2, string_1, account));
				}
			}
		}
		catch (Exception ex)
		{
			CLogger.Print(GetType().Name + ": " + ex.Message, LoggerType.Error, ex);
		}
	}
}
