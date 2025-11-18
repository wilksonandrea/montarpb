using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_CS_ROOM_INVITED_REQ : GameClientPacket
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
			if (player != null && player.ClanId != 0)
			{
				Account account = AccountManager.GetAccount(long_0, 31);
				if (account != null && account.ClanId == player.ClanId)
				{
					account.SendPacket(new PROTOCOL_CS_ROOM_INVITED_RESULT_ACK(Client.PlayerId), OnlyInServer: false);
				}
				player.SendPacket(new PROTOCOL_CS_ROOM_INVITED_ACK(0));
			}
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
		}
	}
}
