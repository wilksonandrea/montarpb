using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_AUTH_FIND_USER_REQ : GameClientPacket
{
	private string string_0;

	private uint uint_0;

	public override void Read()
	{
		string_0 = ReadU(34);
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
			Account account = AccountManager.GetAccount(string_0, 1, 286);
			if (account != null && player.Nickname.Length > 0 && player.Nickname != string_0)
			{
				if (player.Nickname != account.Nickname)
				{
					player.FindPlayer = account.Nickname;
				}
			}
			else
			{
				uint_0 = 2147489795u;
			}
			Client.SendPacket(new PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK(uint_0, account, int.MaxValue));
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
		}
	}
}
