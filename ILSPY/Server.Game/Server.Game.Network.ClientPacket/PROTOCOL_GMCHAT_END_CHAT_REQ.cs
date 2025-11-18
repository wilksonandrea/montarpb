using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_GMCHAT_END_CHAT_REQ : GameClientPacket
{
	private long long_0;

	public override void Read()
	{
		long_0 = ReadQ();
	}

	public override void Run()
	{
		try
		{
			if (Client.Player != null)
			{
				Account account = AccountManager.GetAccount(long_0, 31);
				if (account != null)
				{
					Client.SendPacket(new PROTOCOL_GMCHAT_END_CHAT_ACK(0u, account));
				}
				else
				{
					Client.SendPacket(new PROTOCOL_GMCHAT_END_CHAT_ACK(2147483648u, null));
				}
			}
		}
		catch (Exception ex)
		{
			CLogger.Print("PROTOCOL_GMCHAT_START_CHAT_REQ: " + ex.Message, LoggerType.Error, ex);
		}
	}
}
