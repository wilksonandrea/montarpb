using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_BASE_GET_RECORD_INFO_DB_REQ : GameClientPacket
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
			Account player = Client.Player;
			if (player != null)
			{
				Account account = AccountManager.GetAccount(long_0, 31);
				if (account != null && player.PlayerId != account.PlayerId)
				{
					Client.SendPacket(new PROTOCOL_BASE_GET_RECORD_INFO_DB_ACK(account));
				}
			}
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
		}
	}
}
