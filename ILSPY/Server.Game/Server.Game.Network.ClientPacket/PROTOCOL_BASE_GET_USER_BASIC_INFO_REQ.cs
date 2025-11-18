using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_BASE_GET_USER_BASIC_INFO_REQ : GameClientPacket
{
	private uint uint_0;

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
			if (player == null)
			{
				return;
			}
			Account accountDB = AccountManager.GetAccountDB(long_0, 2, 31);
			if (accountDB != null && player.Nickname.Length > 0 && player.PlayerId != long_0)
			{
				if (player.Nickname != accountDB.Nickname)
				{
					player.FindPlayer = accountDB.Nickname;
				}
				Client.SendPacket(new PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK(uint_0, accountDB, int.MaxValue));
			}
			else
			{
				uint_0 = 2147489795u;
			}
			Client.SendPacket(new PROTOCOL_BASE_GET_USER_BASIC_INFO_ACK(uint_0));
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
		}
	}
}
