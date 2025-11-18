using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.SQL;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_CS_REQUEST_INFO_REQ : GameClientPacket
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
				Client.SendPacket(new PROTOCOL_CS_REQUEST_INFO_ACK(long_0, DaoManagerSQL.GetRequestClanInviteText(player.ClanId, long_0)));
			}
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
		}
	}
}
