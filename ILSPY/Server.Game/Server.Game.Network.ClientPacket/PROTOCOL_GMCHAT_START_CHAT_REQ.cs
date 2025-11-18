using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_GMCHAT_START_CHAT_REQ : GameClientPacket
{
	private string string_0;

	private int int_0;

	private byte byte_0;

	public override void Read()
	{
		string_0 = ReadU(ReadC() * 2);
		int_0 = ReadD();
		byte_0 = ReadC();
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
					Client.SendPacket(new PROTOCOL_GMCHAT_START_CHAT_ACK(0u, account));
				}
				else
				{
					Client.SendPacket(new PROTOCOL_GMCHAT_START_CHAT_ACK(2147483648u, null));
				}
			}
		}
		catch (Exception ex)
		{
			CLogger.Print("PROTOCOL_GMCHAT_START_CHAT_REQ: " + ex.Message, LoggerType.Error, ex);
		}
	}
}
