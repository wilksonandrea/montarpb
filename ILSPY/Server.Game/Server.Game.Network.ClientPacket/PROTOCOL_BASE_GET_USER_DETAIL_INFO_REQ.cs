using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_BASE_GET_USER_DETAIL_INFO_REQ : GameClientPacket
{
	private int int_0;

	public override void Read()
	{
		int_0 = ReadD();
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
			PlayerSession player2 = player.GetChannel().GetPlayer(int_0);
			if (player2 == null)
			{
				return;
			}
			Account account = AccountManager.GetAccount(player2.PlayerId, noUseDB: true);
			if (account != null)
			{
				if (player.Nickname != account.Nickname)
				{
					player.FindPlayer = account.Nickname;
				}
				Client.SendPacket(new PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK(0u, account, int.MaxValue));
			}
		}
		catch (Exception ex)
		{
			CLogger.Print("PROTOCOL_LOBBY_USER_STATISTICS_REQ: " + ex.Message, LoggerType.Error, ex);
		}
	}
}
